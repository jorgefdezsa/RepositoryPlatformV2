using System.Resources;

namespace DEMO.API.Resources
{
    public class ProxyMessages
    {
        private static ResourceManager _resourceMan;


        public static ResourceManager ProxyResourceManager
        {
            get
            {
                if (object.ReferenceEquals(_resourceMan, null))
                {
                    ResourceManager temp = new("DEMO.API.Resources.Messages", typeof(Messages).Assembly);
                    _resourceMan = temp;
                }
                return _resourceMan;
            }
        }

        public static string GetMessageKey(MessagesConstants key)
        {
            return ProxyResourceManager.GetString(key.ToString());
        }

        public class MessagesConstants
        {
            private MessagesConstants(string value) { Value = value; }

            public string Value { get; private set; }

            public static MessagesConstants FIELD_MANDATORY { get { return new MessagesConstants("FIELD_MANDATORY"); } }
            public static MessagesConstants INCORRECT_VALUES { get { return new MessagesConstants("INCORRECT_VALUES"); } }
            public static MessagesConstants USER_NOT_EXISTS { get { return new MessagesConstants("USER_NOT_EXISTS"); } }
            public static MessagesConstants DOMAIN_INCORECT_VALUES { get { return new MessagesConstants("DOMAIN_INCORECT_VALUES"); } }
            public static MessagesConstants MAXIMUN_STRING_LENGTH { get { return new MessagesConstants("MAXIMUN_STRING_LENGTH"); } }

            public static MessagesConstants RESOURCE_NOT_EXISTS { get { return new MessagesConstants("RESOURCE_NOT_EXISTS"); } }

            public static MessagesConstants USER_ASSOCIATE_WITH_OTHER_RESOURCE { get { return new MessagesConstants("USER_ASSOCIATE_WITH_OTHER_RESOURCE"); } }

            public static MessagesConstants EMPTY_MESSAGE { get { return new MessagesConstants("EMPTY_MESSAGE"); } }

            public static MessagesConstants SOC_SEND_COMMUNICATION_FAIL { get { return new MessagesConstants("SOC_SEND_COMMUNICATION_FAIL"); } }

            public static MessagesConstants BATCH_MESSAGE_EMPTY { get { return new MessagesConstants("BATCH_MESSAGE_EMPTY"); } }

            public static MessagesConstants MESSAGE_SERVICEBUS_NOTFIT { get { return new MessagesConstants("MESSAGE_SERVICEBUS_NOTFIT"); } }

            public static MessagesConstants MESSAGE_SERVICEBUS_BATCH_ERROR { get { return new MessagesConstants("MESSAGE_SERVICEBUS_BATCH_ERROR"); } }

            public static MessagesConstants EXPORT_TIME_ENTRY_PLANNER_NOT_APPROVED { get { return new MessagesConstants("EXPORT_TIME_ENTRY_PLANNER_NOT_APPROVED"); } }

            public static MessagesConstants EXPORT_TIME_ENTRY_PLANNER_NOT_ECONOMIC_FIELDS { get { return new MessagesConstants("EXPORT_TIME_ENTRY_PLANNER_NOT_ECONOMIC_FIELDS"); } }

            public static MessagesConstants ABSENCE_NOT_EXISTS { get { return new MessagesConstants("ABSENCE_NOT_EXISTS"); } }

            public static MessagesConstants TIME_ENTRY_NOT_EXISTS { get { return new MessagesConstants("TIME_ENTRY_NOT_EXISTS"); } }


            public override string ToString()
            {
                return Value;
            }
        }
    }
}
