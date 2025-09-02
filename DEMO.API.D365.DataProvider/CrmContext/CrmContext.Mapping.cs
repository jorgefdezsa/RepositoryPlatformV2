namespace DEMO.API.D365.DataProvider.CrmContext
{
    using AutoMapper;
    using AutoMapper.Configuration.Annotations;
    using DEMO.API.D365.DataProvider.Attributes;
    using DEMO.API.D365.DataProvider.Collection;
    using Microsoft.Xrm.Sdk;
    using System.Collections;
    using System.Reflection;

    public partial class CrmContext : ICrmContext
    {
        private IMapper GetBusinessToEntityMapper<T>() where T : ITrackedBusinessObject, new()
        {
            var type = typeof(T);
            return businessToEntityMapperCache.GetOrAdd(type,
                (t) =>
                {
                    return new MapperConfiguration(cfg => cfg.CreateMap<T, Entity>().ConvertUsing<EntityTypeConverter<T>>())
                           .CreateMapper();
                });

        }
        private static Tuple<PropertyInfo, string, D365_AtributeBase>[] GetOrCreatePropertyList<T>()
        {
            return typePropertiesCache.GetOrAdd(typeof(T), (t) => CreatePropertyCache(t));
        }
        internal class BusinessToTypeConverter<T> : AutoMapper.ITypeConverter<Entity, T> where T : ITrackedBusinessObject, new()
        {
            public BusinessToTypeConverter()
            {

            }
            public T Convert(Entity entity, T value, ResolutionContext context)
            {
                value = value ?? new T();
                value.CrmId = entity.Id;
                ICrmContext _context = context.Items["ICrmContext"] as ICrmContext;
                Tuple<PropertyInfo, TrackedForeingKeyAttributeBase>[] relations = GetOrCreateOneToNRelations<T>();
                foreach (var r in relations)
                {
                    var trackedcol = r.Item2.CreateCollection(_context, r.Item1, entity.Id, r.Item1.GetValue(value) as ICollection);
                    r.Item1.SetValue(value, trackedcol);
                }
                Tuple<PropertyInfo, string, D365_AtributeBase>[] props = GetOrCreatePropertyList<T>();

                foreach (var property in props)
                {
                    if (entity.Attributes.TryGetValue(property.Item2.ToLowerInvariant(), out var val))
                    {
                        var obj = ConvertFieldEntityToBusiness(val);
                        if (property.Item1 != null && property.Item3 != null
                            && property.Item3 is D365_PickListAtribute && IsNullableEnum(property.Item1.PropertyType, out var underlyingType) && obj != null)
                        {
                            MethodInfo castMethod = this.GetType().GetMethod("Cast", BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(underlyingType);
                            object castedNullableEnum = castMethod.Invoke(this, new object[] { obj });
                            property.Item1.SetValue(value, castedNullableEnum);
                        }
                        else
                            property.Item1.SetValue(value, obj ?? null); //En cuelqueir otro caso, el caso general
                    }
                }
                return value;
            }

            private T Cast<T>(object o)
            {
                return (T)o;
            }
        }

        private static bool IsNullableEnum(Type t, out Type underlyingType)
        {
            underlyingType = Nullable.GetUnderlyingType(t);
            return (underlyingType != null) && underlyingType.IsEnum;
        }

        private static object ConvertFieldBusinessToEntity(D365_AtributeBase attributeType, object businessAttribute)
        {
            if (businessAttribute != null)
            {
                switch (attributeType)
                {
                    case D365_LookupAttribute lk:
                        if (businessAttribute is Guid guid)
                            return new EntityReference(lk.GetReferenceLogicalEntityName(), guid);
                        else
                            return businessAttribute;
                    case D365_MoneyAtribute:
                        if (businessAttribute is decimal dd)
                            return new Money(dd);
                        else
                            return businessAttribute;
                    case D365_PickListAtribute p:
                        if (businessAttribute is Enum || businessAttribute is int)
                            return new OptionSetValue((int)businessAttribute);
                        else
                            return businessAttribute;
                    default:
                        return businessAttribute;
                }
            }
            else
                return businessAttribute;


        }

        private static object ConvertFieldEntityToBusiness(object entityAttribute)
        {
            if (entityAttribute != null)
            {
                if (entityAttribute is AliasedValue aliased)
                    return ConvertFieldEntityToBusiness(aliased.Value);

                switch (entityAttribute)
                {
                    case EntityReference er:
                        return er.Id;
                    case OptionSetValue op:
                        return op.Value;
                    case Money mn:
                        return mn.Value;
                    default:
                        return entityAttribute;
                }

            }
            else
                return entityAttribute;
        }

        private static Tuple<PropertyInfo, TrackedForeingKeyAttributeBase>[] GetOrCreateOneToNRelations<T>()
        {
            return onetonrelations.GetOrAdd(typeof(T), (t) => CreateRelationsCache(t));
        }

        private static Tuple<PropertyInfo, TrackedForeingKeyAttributeBase>[] GetOrCreateOneToNRelations(Type x)
        {
            return onetonrelations.GetOrAdd(x, (t) => CreateRelationsCache(t));
        }


        private static Tuple<PropertyInfo, TrackedForeingKeyAttributeBase>[] CreateRelationsCache(Type t)
        {
            return

                      t.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                          .Select(x => new Tuple<PropertyInfo, Attribute>(x, x.GetCustomAttributes().FirstOrDefault(n => n is TrackedForeingKeyAttributeBase)))
                          .Where(x => x.Item2 != null)
                          .Select(x => new Tuple<PropertyInfo, TrackedForeingKeyAttributeBase>(x.Item1, (x.Item2 as TrackedForeingKeyAttributeBase)))
                          .ToArray();

        }

        private static Tuple<PropertyInfo, string, D365_AtributeBase>[] CreatePropertyCache(Type t)
        {
            return

                            t.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                .Select(x => new Tuple<PropertyInfo, SourceMemberAttribute?>(x, x.GetCustomAttribute<SourceMemberAttribute>()))
                                .Select(x =>
                                {

                                    if (x.Item1.Name == nameof(ITrackedBusinessObject.CrmId)
                                    || x.Item1.GetCustomAttributes().Any(x => x is IgnoreAttribute || x is TrackedForeingKeyAttributeBase))
                                        return new Tuple<PropertyInfo, string, D365_AtributeBase>(x.Item1, "", null);

                                    var attributeD365 = (D365_AtributeBase?)x.Item1.GetCustomAttributes().FirstOrDefault(n => n is D365_AtributeBase);

                                    return new Tuple<PropertyInfo, string, D365_AtributeBase>(x.Item1, x.Item2?.Name ?? x.Item1.Name, attributeD365);
                                })
                                .Where(x => !String.IsNullOrWhiteSpace(x.Item2)).ToArray();
        }


        internal class EntityTypeConverter<T> : AutoMapper.ITypeConverter<T, Entity> where T : ITrackedBusinessObject
        {

            public Entity Convert(T value, Entity entity, ResolutionContext context)
            {
                if (value.CrmId != null)
                    entity = entity ?? new Entity(T.LogicalName, value.CrmId.Value);
                else
                    entity = entity ?? new Entity(T.LogicalName);

                var props = GetOrCreatePropertyList<T>();
                foreach (var property in props)
                {
                    var newval = ConvertFieldBusinessToEntity(property.Item3, property.Item1.GetValue(value));

                    if (entity.Attributes.ContainsKey(property.Item2.ToLowerInvariant()))

                        entity.Attributes[property.Item2.ToLowerInvariant()] = newval;

                    else
                        entity.Attributes.Add(property.Item2.ToLowerInvariant(), newval);
                }
                return entity;
            }

        }

        private IMapper GetEntityToBusinessMapper<T>() where T : ITrackedBusinessObject, new()
        {
            var type = typeof(T);
            return entityToBusinessMapperCache.GetOrAdd(type,
                (t) =>
                {
                    return new MapperConfiguration(cfg => cfg.CreateMap<Entity, T>().ConvertUsing(new BusinessToTypeConverter<T>())).CreateMapper();
                });

        }

    }
}
