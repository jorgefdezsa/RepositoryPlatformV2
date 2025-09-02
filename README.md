# DEMO Repository Platform

Plataforma de integración basada en Azure Functions (Worker aislado .NET 9) para operaciones de negocio (ej. Upsert de clientes) sobre distintos orígenes/destinos (Dynamics 365 / SQL) con capacidades de resiliencia (reintentos en Service Bus), validación, mapeo y desacoplamiento por capas.

---
## 🔍 Objetivos
- Exponer funciones HTTP y basadas en mensajería para orquestar operaciones de dominio.
- Implementar una arquitectura limpia / DDD orientada a mantenibilidad y extensibilidad.
- Asegurar resiliencia ante fallos controlados vía reintentos programados en Azure Service Bus.
- Centralizar validación, mapeo, manejo de errores y presentación de respuestas homogéneas.

---
## 🏗️ Arquitectura (visión alta)
```
[Cliente/Caller]
     |  HTTP POST /UpsertCustomer
     v
[Azure Function (Isolated)] --(Validation + Mapping)--> [Servicio de Dominio]
     |                                                /          \
     |                                     [Repositorios SQL]   [DataProvider D365]
     |                                                          (CDS / Dataverse)
     |-- on Controlled Error --> [Retry Manager] -> [Service Bus Topic/Queue upsertcustomer]
                                      ^                    |
                                      | (Trigger Retry) <--+
```

### Capas / Proyectos
| Proyecto | Rol principal |
|----------|---------------|
| `DEMO.API.Functions` | Funciones Azure (triggers HTTP, Service Bus), Middlewares, Presenters, Validación y orquestación. |
| `DEMO.API.Core` | Entidades de dominio, excepciones personalizadas, lógica transversal mínima. |
| `DEMO.API.D365.DataProvider` | Acceso a Dynamics 365 / Dataverse (contexto, conexión, atributos, providers). |
| `DEMO.API.D365.Data` | Modelos/DTOs y repositorios/abstracciones específicas de D365. |
| `DEMO.API.D365.Services` | Servicios de dominio (casos de uso: Customer, etc.). |
| `DEMO.API.SQL` | Repositorios y acceso a SQL (EF Core DbContext y repos). |
| `DEMO.API.SQL.Integrations` | Modelos y repositorios de integraciones SQL auxiliares. |
| `DEMO.API.Functions.Helpers.Types` | Infraestructura auxiliar: Service Bus, Retry Manager, Parsers/Serializers, extensiones. |
| `DEMO.API.Resources` | Recursos `.resx` multilenguaje (mensajes, internacionalización). |
| `DEMO-REPOSITORY.ServiceDefaults` | Extensiones comunes y configuración base compartida. |

### Principales librerías / tecnologías
- .NET 9 / C# (Azure Functions Isolated Worker v4)
- Azure Service Bus (mensajería y reintentos diferidos)
- Entity Framework Core 7 (SQL Server)
- AutoMapper (mapeo DTO <-> Modelos)
- FluentValidation (validación declarativa)
- OpenAPI (documentación automática de endpoints)
- Inyección de dependencias nativa (`Microsoft.Extensions.*`)
- Middleware personalizado para logging y manejo de excepciones

---
## ⚙️ Flujo de ejemplo: `UpsertCustomer`
1. HTTP Trigger `Function("UpsertCustomer")` recibe `UpsertCustomerModel` (JSON).
2. `GetJsonBody<TModel, TValidator>()` deserializa + valida (FluentValidation). Si hay errores -> `BadRequestException`.
3. AutoMapper convierte modelo -> `UpsertCustomerRequest` (DTO de servicio).
4. `ICustomerService.Handle()` ejecuta la lógica de dominio: coordinación entre repositorios D365/SQL.
5. El resultado se materializa vía un `IApiPresenter<UpsertCustomerResponse>` que encapsula formato de respuesta.
6. Si se produce una excepción controlada que requiere reintento, `IRetryManager.SendMessage()` publica el payload en Service Bus (`upsertcustomer`).
7. La función `RetryUpsertCustomer` (ServiceBusTrigger) consume el mensaje y re-ejecuta el flujo aplicando política de reintentos lineal (o la configurada).

### Manejo de errores
- Excepciones de validación -> 400 (Bad Request) con lista de mensajes.
- Concurrency / conflictos de dominio -> 409 (Conflict) (según implementación interna del servicio).
- Errores no controlados -> 500 (Internal Server Error) capturados por `ExceptionLoggingMiddleware` (log + respuesta estandarizada).

### Reintentos y Service Bus
El `RetryManager` gestiona:
- Envío inicial a la cola / tópico con metadatos (timestamp, retry count).
- Programación (Scheduled Enqueue Time) basada en estrategia (e.g. Lineal, exponencial si se implementa).
- Re-publicación incrementando cabeceras personalizadas y manteniendo trazabilidad.

---
## 🧩 Inyección de dependencias (Program.cs resumido)
- Registro de AutoMapper (assemblies escaneados + perfiles explícitos).
- Registro de clientes de Azure Service Bus (cliente y administración) con política de reintentos.
- Registro de DbContext EF Core (`SqldbIberoamericaFspoDevContext`) con cadena `SQLConnectionString`.
- Encadenamiento de métodos de extensión: `.AddCDSDataProviderDependencies()`, `.AddRepositoriesDependencies()`, `.AddServicesDependencies()`, `.AddHelpersDependencies()`, `.AddSqlDependencies()`, `.AddPresentersInputsDependencies()`.

---
## 🛡️ Principios de diseño
- Separación de responsabilidades: Cada proyecto foca un aspecto (dominio, infraestructura, aplicación, presentación).
- Abstracción de acceso a datos: Servicios de dominio no dependen de detalles concretos de acceso (repositorios / providers detrás de interfaces).
- Resiliencia y consistencia eventual mediante reintentos asíncronos.
- Internacionalización: Mensajes en `DEMO.API.Resources` (neutral + culturas `es-ES`, `pt-PT`).
- Extensibilidad: Nuevas funciones siguen un pipeline consistente (validación -> mapeo -> servicio -> presenter -> respuesta / retry).

---
## 🚀 Puesta en marcha local
### Prerrequisitos
- .NET 9 SDK
- Azure Functions Core Tools v4
- Azure Service Bus Namespace (cadena conexión) (local o en Azure)
- SQL Server (local o Azure) con base y esquemas esperados

### Archivos de configuración
Crear `DEMO.API.Functions/local.settings.json` (no se versiona) con algo similar:
```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true", // si se requiere
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "ServiceBusConnection": "Endpoint=sb://<namespace>.servicebus.windows.net/;SharedAccessKeyName=...;SharedAccessKey=...",
    "SQLConnectionString": "Server=localhost;Database=DemoDb;User Id=...;Password=...;TrustServerCertificate=True;"
  }
}
```
(No incluir secretos reales en control de versiones.)

### Compilar
```powershell
dotnet restore
dotnet build
```

### Ejecutar Functions
```powershell
cd .\DEMO.API.Functions
func start
```
OpenAPI/Swagger se expondrá (según configuración de la extensión) típicamente en: `http://localhost:7071/api/swagger/ui`.

### Invocar UpsertCustomer (ejemplo)
```bash
POST http://localhost:7071/api/UpsertCustomer
Content-Type: application/json

{
  "customerId": "12345",
  "name": "Cliente Demo",
  "email": "demo@example.com"
}
```
(Estructura exacta dependerá del modelo `UpsertCustomerModel`.)

---
## ➕ Añadir una nueva Function (guía rápida)
1. Modelo + Validator: Crear `MyOperationModel` y `MyOperationValidator` (FluentValidation).
2. DTO y Perfil AutoMapper: Crear request/response DTOs en capa de servicios / data y perfil de mapeo.
3. Servicio de dominio: Agregar método en servicio o nuevo servicio implementando interfaz adecuada.
4. Presenter: Implementar `IApiPresenter<T>` si el contrato de respuesta difiere.
5. Function: Crear clase con `[Function("MyOperation")]` + triggers (HTTP / ServiceBus).
6. Registrar dependencias (si nuevas) en métodos de extensión DI.
7. Actualizar OpenAPI con atributos (`OpenApiOperation`, `OpenApiResponseWithBody`).
8. Probar local + escenarios de error / retry.

---
## 📦 Packaging & Deploy (resumen conceptual)
- Publicación Azure Functions aisladas: `dotnet publish -c Release` y despliegue mediante Azure DevOps / GitHub Actions / Azure Functions Deploy.
- Asegurar configuración de App Settings en el Function App (ServiceBusConnection, SQLConnectionString, etc.).
- (Opcional) Habilitar Application Insights para telemetría (paquete y connection string).

---
## 🧪 Testing (recomendado)
Aunque no se muestran proyectos de test en este repo, se sugiere:
- Tests de Validación: Validar reglas FluentValidation.
- Tests de Mapeo: Assert AutoMapper configuration (`MapperConfiguration.AssertConfigurationIsValid()`).
- Tests de Servicios: Mock de repositorios / providers.
- Tests de Functions: Uso de `FunctionContext` fake + serialización de requests.

---
## 📊 Logging & Observabilidad
- Consola (local) vía `AddConsole()`.
- Middleware central para registro de excepciones.
- Recomendado: habilitar Application Insights (telemetría, traces, dependencias, métricas custom).

---
## 🌐 Internacionalización
Los mensajes se resuelven desde `DEMO.API.Resources` permitiendo entregar errores / textos en distintos idiomas (ES, PT). Anclar cultura vía configuraciones o encabezados (a implementar si procede).

---
## 🔁 Estrategias de Reintento
Implementadas vía Service Bus + `IRetryManager`:
- Modo Lineal (intervalos uniformes configurables).
- Extensible para Exponencial, Backoff con jitter, etc. Añadiendo lógica en `RetryManager`.

---
## 🧩 Extensiones y Middlewares Clave
- `ExceptionLoggingMiddleware` captura errores no controlados y emite respuesta normalizada.
- Extensiones de `FunctionContext` para almacenar metadata: timestamps, retry count, payload original.

---
## 🗺️ Roadmap sugerido
- Añadir capa de Tests automatizados.
- Implementar política de reintentos exponencial con jitter.
- Incorporar métricas custom (latencia por caso de uso, tasa de reintentos exitosos).
- Seguridad: Autenticación (JWT / AAD) en endpoints HTTP (actualmente `AuthorizationLevel.Anonymous`).
- Observabilidad avanzada: OpenTelemetry + traces distribuidos.

---
## 🛡️ Seguridad
- Evitar exponer Function Keys con permisos amplios si se activa auth nivel Function.
- Gestionar secretos en Azure Key Vault (referencias en App Settings) para cadenas de conexión.
- Limitar políticas SAS de Service Bus a mínimos privilegios.

---
## 📂 Estructura (resumen)
```
DEMO-REPOSITORY.sln
└── DEMO.API.Functions
    ├── Program.cs
    ├── UpsertCustomer.cs
    ├── Middlewares/
    ├── Validators/
    ├── Presenters/
    └── ...
└── DEMO.API.Core
└── DEMO.API.D365.* (DataProvider, Data, Services)
└── DEMO.API.SQL.* (principal, Integrations)
└── DEMO.API.Functions.Helpers.Types
└── DEMO.API.Resources
└── DEMO-REPOSITORY.ServiceDefaults
```

---
## ❓ FAQ Rápido
**¿Dónde agrego un nuevo servicio de dominio?** En `DEMO.API.D365.Services` (o crear nueva carpeta según bounded context) exponiendo interfaz + implementación.

**¿Cómo manejo nuevos mensajes de retry?** Añadir estrategia en `RetryManager` y ajustar enumeraciones / configuración.

**¿Cómo amplio OpenAPI?** Añadir atributos `OpenApi*` en la Function; regenerado en runtime.

---
## 📄 Licencia
Definir licencia (MIT / Propietaria) según política del repositorio.

---
## 🤝 Contribuciones
1. Crear rama feature/*.
2. PR con descripción técnica + capturas (si aplica).
3. Revisiones: Estándares de naming, separación de capas, no exponer secretos.

---
## ✅ Resumen
Este README cubre la arquitectura, flujo principal, configuración, extensibilidad y mejores prácticas recomendadas para evolucionar la plataforma.
