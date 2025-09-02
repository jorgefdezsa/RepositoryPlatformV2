namespace DEMO.API.Functions.Validators
{
    using DEMO.API.Functions.Models;
    using FluentValidation;
    using static DEMO.API.Core.Domain.Metadata.Customer.AccountMetadata;
    using static DEMO.API.Resources.ProxyMessages;

    public class UpsertCustomerValidator : AbstractValidator<UpsertCustomerModel>
    {
        public UpsertCustomerValidator()
        {
            //CustomerId
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage(string.Format(GetMessageKey(MessagesConstants.FIELD_MANDATORY), "CustomerId"));
            RuleFor(x => x.CustomerId).MaximumLength(100).WithMessage(string.Format(GetMessageKey(MessagesConstants.MAXIMUN_STRING_LENGTH), "CustomerId", "100"));

            //LegalEntity
            RuleFor(x => x.LegalEntityId).NotEmpty().WithMessage(string.Format(GetMessageKey(MessagesConstants.FIELD_MANDATORY), "LegalEntity"));

            //FiscalId
            RuleFor(x => x.FiscalId).NotEmpty().WithMessage(string.Format(GetMessageKey(MessagesConstants.FIELD_MANDATORY), "FiscalId"));
            RuleFor(x => x.FiscalId).MaximumLength(100).WithMessage(string.Format(GetMessageKey(MessagesConstants.MAXIMUN_STRING_LENGTH), "FiscalId", "100"));

            //Name
            RuleFor(x => x.Name).NotEmpty().WithMessage(string.Format(GetMessageKey(MessagesConstants.FIELD_MANDATORY), "Name"));
            RuleFor(x => x.Name).MaximumLength(160).WithMessage(string.Format(GetMessageKey(MessagesConstants.MAXIMUN_STRING_LENGTH), "Name", "160"));

            //Address
            RuleFor(x => x.Address).NotEmpty().WithMessage(string.Format(GetMessageKey(MessagesConstants.FIELD_MANDATORY), "Address"));
            RuleFor(x => x.Address).MaximumLength(250).WithMessage(string.Format(GetMessageKey(MessagesConstants.MAXIMUN_STRING_LENGTH), "Address", "250"));

            //City
            RuleFor(x => x.City).NotEmpty().WithMessage(string.Format(GetMessageKey(MessagesConstants.FIELD_MANDATORY), "City"));
            RuleFor(x => x.City).MaximumLength(80).WithMessage(string.Format(GetMessageKey(MessagesConstants.MAXIMUN_STRING_LENGTH), "City", "80"));

            //State or Province
            RuleFor(x => x.State_Province).NotEmpty().WithMessage(string.Format(GetMessageKey(MessagesConstants.FIELD_MANDATORY), "State_Province"));
            RuleFor(x => x.State_Province).MaximumLength(50).WithMessage(string.Format(GetMessageKey(MessagesConstants.MAXIMUN_STRING_LENGTH), "State_Province", "50"));

            //Country
            RuleFor(x => x.Country).NotEmpty().WithMessage(string.Format(GetMessageKey(MessagesConstants.FIELD_MANDATORY), "Country"));
            RuleFor(x => x.Country).MaximumLength(50).WithMessage(string.Format(GetMessageKey(MessagesConstants.MAXIMUN_STRING_LENGTH), "Country", "50"));

            //Código Postal
            RuleFor(x => x.PostalCode).NotEmpty().WithMessage(string.Format(GetMessageKey(MessagesConstants.FIELD_MANDATORY), "PostalCode"));
            RuleFor(x => x.PostalCode).MaximumLength(20).WithMessage(string.Format(GetMessageKey(MessagesConstants.MAXIMUN_STRING_LENGTH), "PostalCode", "20"));

            //Integration Time Stamp
            RuleFor(x => x.IntegrationTimeStamp).NotEmpty().WithMessage(string.Format(GetMessageKey(MessagesConstants.FIELD_MANDATORY), "IntegrationTimeStamp"));

            //Customer Type Code
            RuleFor(x => x.RelationshipType).Must(RelationshipType => RelationshipType.Equals((int)RelationshipType_OptionSet.Competitor) ||
                                                    RelationshipType.Equals((int)RelationshipType_OptionSet.Consultant) ||
                                                    RelationshipType.Equals((int)RelationshipType_OptionSet.Customer) ||
                                                    RelationshipType.Equals((int)RelationshipType_OptionSet.Influencer) ||
                                                    RelationshipType.Equals((int)RelationshipType_OptionSet.Investor) ||
                                                    RelationshipType.Equals((int)RelationshipType_OptionSet.Partner) ||
                                                    RelationshipType.Equals((int)RelationshipType_OptionSet.Press) ||
                                                    RelationshipType.Equals((int)RelationshipType_OptionSet.Prospect) ||
                                                    RelationshipType.Equals((int)RelationshipType_OptionSet.Reseller) ||
                                                    RelationshipType.Equals((int)RelationshipType_OptionSet.Supplier) ||
                                                    RelationshipType.Equals((int)RelationshipType_OptionSet.Vendor) ||
                                                    RelationshipType.Equals((int)RelationshipType_OptionSet.Other) ||
                                                    RelationshipType.Equals(null)
                    ).WithMessage(string.Format(GetMessageKey(MessagesConstants.INCORRECT_VALUES), "RelationshipType"));

        }
    }
}
