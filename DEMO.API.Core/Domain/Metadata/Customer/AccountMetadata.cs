// *********************************************************************
// Created by : Latebound Constants Generator 1.2025.8.1 for XrmToolBox
// Tool Author: Jonas Rapp https://jonasr.app/
// GitHub     : https://github.com/rappen/LCG-UDG/
// Source Org : https://orge687a35c.crm4.dynamics.com
// Filename   : C:\Users\jorge\Downloads\Account.cs
// Created    : 2025-09-01 22:55:02
// *********************************************************************
namespace DEMO.API.Core.Domain.Metadata.Customer
{
    /// <summary>OwnershipType: UserOwned, IntroducedVersion: 5.0.0.0</summary>
    public static class AccountMetadata
    {
        public const string EntityName = "account";
        public const string EntityCollectionName = "accounts";

        #region Attributes

        /// <summary>Type: Uniqueidentifier, RequiredLevel: SystemRequired</summary>
        public const string PrimaryKey = "accountid";
        /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 160, Format: Text</summary>
        public const string PrimaryName = "name";
        /// <summary>Type: Uniqueidentifier, RequiredLevel: None</summary>
        public const string DeprecatedProcessStage = "stageid";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 1250, Format: Text</summary>
        public const string DeprecatedTraversedPath = "traversedpath";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 20, Format: Text</summary>
        public const string AccountNumber = "accountnumber";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Account Rating, OptionSetType: Picklist, DefaultFormValue: 1</summary>
        public const string AccountRating = "accountratingcode";
        /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 100, Format: Text</summary>
        public const string Address = "jfs_address";
        /// <summary>Type: Money, RequiredLevel: None, MinValue: 0, MaxValue: 100000000000000, Precision: 2</summary>
        public const string Aging30 = "aging30";
        /// <summary>Type: Money, RequiredLevel: None, MinValue: 0, MaxValue: 100000000000000, Precision: 2</summary>
        public const string Aging60 = "aging60";
        /// <summary>Type: Money, RequiredLevel: None, MinValue: 0, MaxValue: 100000000000000, Precision: 2</summary>
        public const string Aging90 = "aging90";
        /// <summary>Type: Money, RequiredLevel: None, MinValue: 0, MaxValue: 100000000000000, Precision: 2</summary>
        public const string AnnualRevenue = "revenue";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Business Type, OptionSetType: Picklist, DefaultFormValue: 1</summary>
        public const string BusinessType = "businesstypecode";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Category, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Category = "accountcategorycode";
        /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 100, Format: Text</summary>
        public const string City = "jfs_city";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Classification, OptionSetType: Picklist, DefaultFormValue: 1</summary>
        public const string Classification = "accountclassificationcode";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: jfs_country</summary>
        public const string Country = "jfs_country";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string CreatedByIPAddress = "adx_createdbyipaddress";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string CreatedByUserName = "adx_createdbyusername";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string CreditHold = "creditonhold";
        /// <summary>Type: Money, RequiredLevel: None, MinValue: 0, MaxValue: 100000000000000, Precision: 2</summary>
        public const string CreditLimit = "creditlimit";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: transactioncurrency</summary>
        public const string Currency = "transactioncurrencyid";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Customer Size, OptionSetType: Picklist, DefaultFormValue: 1</summary>
        public const string CustomerSize = "customersizecode";
        /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 100, Format: Text</summary>
        public const string CustomerERPID = "jfs_customererpid";
        /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
        public const string Description = "description";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string DonotallowBulkEmails = "donotbulkemail";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string DonotallowBulkMails = "donotbulkpostalmail";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string DonotallowEmails = "donotemail";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string DonotallowFaxes = "donotfax";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string DonotallowMails = "donotpostalmail";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string DonotallowPhoneCalls = "donotphone";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Email</summary>
        public const string Email = "emailaddress1";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Email</summary>
        public const string EmailAddress2 = "emailaddress2";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Email</summary>
        public const string EmailAddress3 = "emailaddress3";
        /// <summary>Type: Uniqueidentifier, RequiredLevel: None</summary>
        public const string EntityImageId = "entityimageid";
        /// <summary>Type: Decimal, RequiredLevel: None, MinValue: 0.000000000001, MaxValue: 100000000000, Precision: 12</summary>
        public const string ExchangeRate = "exchangerate";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string Fax = "fax";
        /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 100, Format: Text</summary>
        public const string FiscalID = "jfs_fiscalid";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: True</summary>
        public const string FollowEmailActivity = "followemail";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 200, Format: Url</summary>
        public const string FTPSite = "ftpsiteurl";
        /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 100, Format: Text</summary>
        public const string IdErp = "jfs_iderp";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Industry, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Industry = "industrycode";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string IntegrationTimeStamp = "jfs_integrationtimestamp";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string isprivate = "isprivate";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
        public const string LastDateIncludedinCampaign = "lastusedincampaign";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string LastOnHoldTime = "lastonholdtime";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: sla</summary>
        public const string LastSLAapplied = "slainvokedid";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: jfs_legalentity</summary>
        public const string LegalEntity = "jfs_legalentity";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string MainPhone = "telephone1";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: account</summary>
        public const string ManagingPartner = "msa_managingpartnerid";
        /// <summary>Type: Money, RequiredLevel: None, MinValue: 0, MaxValue: 100000000000000, Precision: 2</summary>
        public const string MarketCapitalization = "marketcap";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string MarketingOnly = "marketingonly";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: account</summary>
        public const string MasterID = "masterid";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Merged = "merged";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string ModifiedByIPAddress = "adx_modifiedbyipaddress";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string ModifiedByUserName = "adx_modifiedbyusername";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: 0, MaxValue: 1000000000</summary>
        public const string NumberofEmployees = "numberofemployees";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
        public const string OnHoldTimeMinutes = "onholdtime";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string OtherPhone = "telephone2";
        /// <summary>Type: Owner, RequiredLevel: SystemRequired, Targets: systemuser,team</summary>
        public const string Owner = "ownerid";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Ownership, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Ownership = "ownershipcode";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: account</summary>
        public const string ParentAccount = "parentaccountid";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string ParticipatesinWorkflow = "participatesinworkflow";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Payment Terms, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string PaymentTerms = "paymenttermscode";
        /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 100, Format: Text</summary>
        public const string PostalCode = "jfs_postalcode";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Preferred Day, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string PreferredDay = "preferredappointmentdaycode";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Preferred Method of Contact, OptionSetType: Picklist, DefaultFormValue: 1</summary>
        public const string PreferredMethodofContact = "preferredcontactmethodcode";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Preferred Time, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string PreferredTime = "preferredappointmenttimecode";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: systemuser</summary>
        public const string PreferredUser = "preferredsystemuserid";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: contact</summary>
        public const string PrimaryContact = "primarycontactid";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 200, Format: Text</summary>
        public const string PrimarySatoriID = "primarysatoriid";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 128, Format: Text</summary>
        public const string PrimaryTwitterID = "primarytwitterid";
        /// <summary>Type: Uniqueidentifier, RequiredLevel: None</summary>
        public const string Process = "processid";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Relationship Type, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string RelationshipType = "customertypecode";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string SendMarketingMaterials = "donotsendmm";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: 0, MaxValue: 1000000000</summary>
        public const string SharesOutstanding = "sharesoutstanding";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Shipping Method, OptionSetType: Picklist, DefaultFormValue: 1</summary>
        public const string ShippingMethod = "shippingmethodcode";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 20, Format: Text</summary>
        public const string SICCode = "sic";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: sla</summary>
        public const string SLA = "slaid";
        /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 100, Format: Text</summary>
        public const string State_Province = "jfs_state_province";
        /// <summary>Type: State, RequiredLevel: SystemRequired, DisplayName: Status, OptionSetType: State</summary>
        public const string Status = "statecode";
        /// <summary>Type: Status, RequiredLevel: None, DisplayName: Status Reason, OptionSetType: Status</summary>
        public const string StatusReason = "statuscode";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 20, Format: Text</summary>
        public const string StockExchange = "stockexchange";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string Telephone3 = "telephone3";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Territory Code, OptionSetType: Picklist, DefaultFormValue: 1</summary>
        public const string TerritoryCode = "territorycode";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 10, Format: TickerSymbol</summary>
        public const string TickerSymbol = "tickersymbol";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 1250, Format: Text</summary>
        public const string TimeSpentbyme = "timespentbymeonemailandmeetings";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 200, Format: Url</summary>
        public const string Website = "websiteurl";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 160, Format: PhoneticGuide</summary>
        public const string YomiAccountName = "yominame";

        #endregion Attributes

        #region OptionSets

        public enum AccountRating_OptionSet
        {
            DefaultValue = 1
        }
        public enum BusinessType_OptionSet
        {
            DefaultValue = 1
        }
        public enum Category_OptionSet
        {
            PreferredCustomer = 1,
            Standard = 2
        }
        public enum Classification_OptionSet
        {
            DefaultValue = 1
        }
        public enum CustomerSize_OptionSet
        {
            DefaultValue = 1
        }
        public enum Industry_OptionSet
        {
            Accounting = 1,
            AgricultureandNon_petrolNaturalResourceExtraction = 2,
            BroadcastingPrintingandPublishing = 3,
            Brokers = 4,
            BuildingSupplyRetail = 5,
            BusinessServices = 6,
            Consulting = 7,
            ConsumerServices = 8,
            DesignDirectionandCreativeManagement = 9,
            DistributorsDispatchersandProcessors = 10,
            DoctorsOfficesandClinics = 11,
            DurableManufacturing = 12,
            EatingandDrinkingPlaces = 13,
            EntertainmentRetail = 14,
            EquipmentRentalandLeasing = 15,
            Financial = 16,
            FoodandTobaccoProcessing = 17,
            InboundCapitalIntensiveProcessing = 18,
            InboundRepairandServices = 19,
            Insurance = 20,
            LegalServices = 21,
            Non_DurableMerchandiseRetail = 22,
            OutboundConsumerService = 23,
            PetrochemicalExtractionandDistribution = 24,
            ServiceRetail = 25,
            SIGAffiliations = 26,
            SocialServices = 27,
            SpecialOutboundTradeContractors = 28,
            SpecialtyRealty = 29,
            Transportation = 30,
            UtilityCreationandDistribution = 31,
            VehicleRetail = 32,
            Wholesale = 33
        }
        public enum Ownership_OptionSet
        {
            Public = 1,
            Private = 2,
            Subsidiary = 3,
            Other = 4
        }
        public enum PaymentTerms_OptionSet
        {
            Net30 = 1,
            _210Net30 = 2,
            Net45 = 3,
            Net60 = 4
        }
        public enum PreferredDay_OptionSet
        {
            Sunday = 0,
            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6
        }
        public enum PreferredMethodofContact_OptionSet
        {
            Any = 1,
            Email = 2,
            Phone = 3,
            Fax = 4,
            Mail = 5
        }
        public enum PreferredTime_OptionSet
        {
            Morning = 1,
            Afternoon = 2,
            Evening = 3
        }
        public enum RelationshipType_OptionSet
        {
            Competitor = 1,
            Consultant = 2,
            Customer = 3,
            Investor = 4,
            Partner = 5,
            Influencer = 6,
            Press = 7,
            Prospect = 8,
            Reseller = 9,
            Supplier = 10,
            Vendor = 11,
            Other = 12
        }
        public enum ShippingMethod_OptionSet
        {
            DefaultValue = 1
        }
        public enum Status_OptionSet
        {
            Active = 0,
            Inactive = 1
        }
        public enum StatusReason_OptionSet
        {
            Active = 1,
            Inactive = 2
        }
        public enum TerritoryCode_OptionSet
        {
            DefaultValue = 1
        }

        #endregion OptionSets
    }
}
