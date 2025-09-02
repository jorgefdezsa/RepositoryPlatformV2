// *********************************************************************
// Created by : Latebound Constants Generator 1.2025.8.1 for XrmToolBox
// Tool Author: Jonas Rapp https://jonasr.app/
// GitHub     : https://github.com/rappen/LCG-UDG/
// Source Org : https://orge687a35c.crm4.dynamics.com
// Filename   : C:\Users\jorge\Downloads\Contact.cs
// Created    : 2025-09-01 19:32:03
// *********************************************************************

namespace DEMO.API.Core.Domain.Metadata.Contact
{
    /// <summary>OwnershipType: UserOwned, IntroducedVersion: 5.0.0.0</summary>
    public static class ContactMetadata
    {
        public const string EntityName = "contact";
        public const string EntityCollectionName = "contacts";

        #region Attributes

        /// <summary>Type: Uniqueidentifier, RequiredLevel: SystemRequired</summary>
        public const string PrimaryKey = "contactid";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 160, Format: Text</summary>
        public const string PrimaryName = "fullname";
        /// <summary>Type: Uniqueidentifier, RequiredLevel: None</summary>
        public const string DeprecatedProcessStage = "stageid";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 1250, Format: Text</summary>
        public const string DeprecatedTraversedPath = "traversedpath";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
        public const string AccessFailedCount = "adx_identity_accessfailedcount";
        /// <summary>Type: Money, RequiredLevel: None, MinValue: 0, MaxValue: 100000000000000, Precision: 2</summary>
        public const string Aging30 = "aging30";
        /// <summary>Type: Money, RequiredLevel: None, MinValue: 0, MaxValue: 100000000000000, Precision: 2</summary>
        public const string Aging60 = "aging60";
        /// <summary>Type: Money, RequiredLevel: None, MinValue: 0, MaxValue: 100000000000000, Precision: 2</summary>
        public const string Aging90 = "aging90";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: DateOnly</summary>
        public const string Anniversary = "anniversary";
        /// <summary>Type: Money, RequiredLevel: None, MinValue: 0, MaxValue: 100000000000000, Precision: 2</summary>
        public const string AnnualIncome = "annualincome";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string Assistant = "assistantname";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string AssistantPhone = "assistantphone";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Auto_created = "isautocreate";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string BackOfficeCustomer = "isbackofficecustomer";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: DateOnly</summary>
        public const string Birthday = "birthdate";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string BusinessPhone = "telephone1";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string BusinessPhone2 = "business2";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string CallbackNumber = "callback";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 255, Format: Text</summary>
        public const string ChildrensNames = "childrensnames";
        /// <summary>Type: Customer, RequiredLevel: None, Targets: account,contact</summary>
        public const string CompanyName = "parentcustomerid";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string CompanyPhone = "company";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string ConfirmRemovePassword = "adx_confirmremovepassword";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string CreatedByIPAddress = "adx_createdbyipaddress";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string CreatedByUsername = "adx_createdbyusername";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string CreditHold = "creditonhold";
        /// <summary>Type: Money, RequiredLevel: None, MinValue: 0, MaxValue: 100000000000000, Precision: 2</summary>
        public const string CreditLimit = "creditlimit";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: transactioncurrency</summary>
        public const string Currency = "transactioncurrencyid";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Customer Size, OptionSetType: Picklist, DefaultFormValue: 1</summary>
        public const string CustomerSize = "customersizecode";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string Department = "department";
        /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
        public const string Description = "description";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string DisableWebTracking = "msdyn_disablewebtracking";
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
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Education, OptionSetType: Picklist, DefaultFormValue: 1</summary>
        public const string Education = "educationcode";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Email</summary>
        public const string Email = "emailaddress1";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Email</summary>
        public const string EmailAddress2 = "emailaddress2";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Email</summary>
        public const string EmailAddress3 = "emailaddress3";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string EmailConfirmed = "adx_identity_emailaddress1confirmed";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string Employee = "employeeid";
        /// <summary>Type: Uniqueidentifier, RequiredLevel: None</summary>
        public const string EntityImageId = "entityimageid";
        /// <summary>Type: Decimal, RequiredLevel: None, MinValue: 0.000000000001, MaxValue: 100000000000, Precision: 12</summary>
        public const string ExchangeRate = "exchangerate";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string ExternalUserIdentifier = "externaluseridentifier";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string Fax = "fax";
        /// <summary>Type: String, RequiredLevel: Recommended, MaxLength: 50, Format: Text</summary>
        public const string FirstName = "firstname";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: True</summary>
        public const string FollowEmailActivity = "followemail";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 200, Format: Url</summary>
        public const string FTPSite = "ftpsiteurl";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Gender, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Gender = "gendercode";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string Government = "governmentid";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Has Children, OptionSetType: Picklist, DefaultFormValue: 1</summary>
        public const string HasChildren = "haschildrencode";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string HomePhone = "telephone2";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string HomePhone2 = "home2";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string IsMinor = "msdyn_isminor";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string IsMinorwithParentalConsent = "msdyn_isminorwithparentalconsent";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string isprivate = "isprivate";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string JobTitle = "jobtitle";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
        public const string LastDateIncludedinCampaign = "lastusedincampaign";
        /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 50, Format: Text</summary>
        public const string LastName = "lastname";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string LastOnHoldTime = "lastonholdtime";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: sla</summary>
        public const string LastSLAapplied = "slainvokedid";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string LastSuccessfulLogin = "adx_identity_lastsuccessfullogin";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Lead Source, OptionSetType: Picklist, DefaultFormValue: 1</summary>
        public const string LeadSource = "leadsourcecode";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string LocalLoginDisabled = "adx_identity_locallogindisabled";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string LockoutEnabled = "adx_identity_lockoutenabled";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string LockoutEndDate = "adx_identity_lockoutenddate";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string LoginEnabled = "adx_identity_logonenabled";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string Manager = "managername";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string ManagerPhone = "managerphone";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: account</summary>
        public const string ManagingPartner = "msa_managingpartnerid";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Marital Status, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string MaritalStatus = "familystatuscode";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string MarketingOnly = "marketingonly";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: contact</summary>
        public const string MasterID = "masterid";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Merged = "merged";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string MiddleName = "middlename";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string MobilePhone = "mobilephone";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string MobilePhoneConfirmed = "adx_identity_mobilephoneconfirmed";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string ModifiedByIPAddress = "adx_modifiedbyipaddress";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string ModifiedByUsername = "adx_modifiedbyusername";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string NewPasswordInput = "adx_identity_newpassword";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string Nickname = "nickname";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: 0, MaxValue: 1000000000</summary>
        public const string NoofChildren = "numberofchildren";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
        public const string OnHoldTimeMinutes = "onholdtime";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 250, Format: Text</summary>
        public const string OrganizationName = "adx_organizationname";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string Pager = "pager";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string ParticipatesinWorkflow = "participatesinworkflow";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 128, Format: Text</summary>
        public const string PasswordHash = "adx_identity_passwordhash";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Payment Terms, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string PaymentTerms = "paymenttermscode";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string PortalTermsAgreementDate = "msdyn_portaltermsagreementdate";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Preferred Day, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string PreferredDay = "preferredappointmentdaycode";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Power Pages Languages, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string PreferredLanguage = "mspp_userpreferredlcid";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
        public const string PreferredLCIDDeprecated = "adx_preferredlcid";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Preferred Method of Contact, OptionSetType: Picklist, DefaultFormValue: 1</summary>
        public const string PreferredMethodofContact = "preferredcontactmethodcode";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Preferred Time, OptionSetType: Picklist, DefaultFormValue: 1</summary>
        public const string PreferredTime = "preferredappointmenttimecode";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: systemuser</summary>
        public const string PreferredUser = "preferredsystemuserid";
        /// <summary>Type: Uniqueidentifier, RequiredLevel: None</summary>
        public const string Process = "processid";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string ProfileAlert = "adx_profilealert";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string ProfileAlertDate = "adx_profilealertdate";
        /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 4096</summary>
        public const string ProfileAlertInstructions = "adx_profilealertinstructions";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string ProfileIsAnonymous = "adx_profileisanonymous";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string ProfileLastActivity = "adx_profilelastactivity";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string ProfileModifiedOn = "adx_profilemodifiedon";
        /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 64000</summary>
        public const string PublicProfileCopy = "adx_publicprofilecopy";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Relationship Type, OptionSetType: Picklist, DefaultFormValue: 1</summary>
        public const string RelationshipType = "customertypecode";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Role, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Role = "accountrolecode";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string Salutation = "salutation";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string SecurityStamp = "adx_identity_securitystamp";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string SendMarketingMaterials = "donotsendmm";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Shipping Method , OptionSetType: Picklist, DefaultFormValue: 1</summary>
        public const string ShippingMethod = "shippingmethodcode";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: sla</summary>
        public const string SLA = "slaid";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string Spouse_PartnerName = "spousesname";
        /// <summary>Type: State, RequiredLevel: SystemRequired, DisplayName: Status, OptionSetType: State</summary>
        public const string Status = "statecode";
        /// <summary>Type: Status, RequiredLevel: None, DisplayName: Status Reason, OptionSetType: Status</summary>
        public const string StatusReason = "statuscode";
        /// <summary>Type: Uniqueidentifier, RequiredLevel: None</summary>
        public const string Subscription = "subscriptionid";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 10, Format: Text</summary>
        public const string Suffix = "suffix";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
        public const string Telephone3 = "telephone3";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Territory, OptionSetType: Picklist, DefaultFormValue: 1</summary>
        public const string Territory = "territorycode";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 1250, Format: Text</summary>
        public const string TimeSpentbyme = "timespentbymeonemailandmeetings";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: -1500, MaxValue: 1500</summary>
        public const string TimeZone = "adx_timezone";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string TwoFactorEnabled = "adx_identity_twofactorenabled";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string UserName = "adx_identity_username";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 200, Format: Url</summary>
        public const string Website = "websiteurl";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 150, Format: PhoneticGuide</summary>
        public const string YomiFirstName = "yomifirstname";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 450, Format: PhoneticGuide</summary>
        public const string YomiFullName = "yomifullname";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 150, Format: PhoneticGuide</summary>
        public const string YomiLastName = "yomilastname";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 150, Format: PhoneticGuide</summary>
        public const string YomiMiddleName = "yomimiddlename";

        #endregion Attributes

        #region OptionSets

        public enum CustomerSize_OptionSet
        {
            DefaultValue = 1
        }
        public enum Education_OptionSet
        {
            DefaultValue = 1
        }
        public enum Gender_OptionSet
        {
            Male = 1,
            Female = 2
        }
        public enum HasChildren_OptionSet
        {
            DefaultValue = 1
        }
        public enum LeadSource_OptionSet
        {
            DefaultValue = 1
        }
        public enum MaritalStatus_OptionSet
        {
            Single = 1,
            Married = 2,
            Divorced = 3,
            Widowed = 4
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
        public enum PreferredLanguage_OptionSet
        {
            Arabic = 1025,
            Basque_Basque = 1069,
            Bulgarian_Bulgaria = 1026,
            Catalan_Catalan = 1027,
            Chinese_China = 2052,
            Chinese_HongKongSAR = 3076,
            Chinese_Traditional = 1028,
            Croatian_Croatia = 1050,
            Czech_CzechRepublic = 1029,
            Danish_Denmark = 1030,
            Dutch_Netherlands = 1043,
            English = 1033,
            Estonian_Estonia = 1061,
            Finnish_Finland = 1035,
            French_France = 1036,
            Galician_Spain = 1110,
            German_Germany = 1031,
            Greek_Greece = 1032,
            Hebrew = 1037,
            Hindi_India = 1081,
            Hungarian_Hungary = 1038,
            Indonesian_Indonesia = 1057,
            Italian_Italy = 1040,
            Japanese_Japan = 1041,
            Kazakh_Kazakhstan = 1087,
            Korean_Korea = 1042,
            Latvian_Latvia = 1062,
            Lithuanian_Lithuania = 1063,
            Malay_Malaysia = 1086,
            NorwegianBokmål_Norway = 1044,
            Polish_Poland = 1045,
            Portuguese_Brazil = 1046,
            Portuguese_Portugal = 2070,
            Romanian_Romania = 1048,
            Russian_Russia = 1049,
            SerbianCyrillic_Serbia = 3098,
            SerbianLatin_Serbia = 2074,
            Slovak_Slovakia = 1051,
            Slovenian_Slovenia = 1060,
            SpanishTraditionalSort_Spain = 3082,
            Swedish_Sweden = 1053,
            Thai_Thailand = 1054,
            Turkish_Türkiye = 1055,
            Ukrainian_Ukraine = 1058,
            Vietnamese_Vietnam = 1066
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
            DefaultValue = 1
        }
        public enum Role_OptionSet
        {
            DecisionMaker = 1,
            Employee = 2,
            Influencer = 3
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
        public enum Territory_OptionSet
        {
            DefaultValue = 1
        }

        #endregion OptionSets
    }
}
