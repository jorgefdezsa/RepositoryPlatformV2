namespace DEMO.API.Functions.Extensions
{
    using DEMO.API.Functions.Common.Wrapper;
    using DEMO.API.Functions.Common.Wrapper.Batch;
    using Microsoft.Azure.Functions.Worker;
    using System.Text.Json;

    public static class FunctionContextExtension
    {
        public static DateTime? GetCustomExecutionTimeStampMessage(this FunctionContext context)
        {
            IReadOnlyDictionary<string, object> bindingData = context.BindingContext.BindingData;
            DateTime TimeStamp;

            if (bindingData.ContainsKey("UserProperties"))
            {
                string userPropertiesStr = bindingData["UserProperties"].ToString();
                JsonDocument json = JsonDocument.Parse(userPropertiesStr);
                if (json.RootElement.TryGetProperty("executionTimeStamp", out var userProperty))
                {
                    if (DateTime.TryParse(userProperty.ToString(), out TimeStamp))
                        return TimeStamp;
                }
            }
            return null;
        }

        public static bool IsHttpTriggerFunction(this FunctionContext context)
        {
            return context.FunctionDefinition.InputBindings.Values
                          .First(a => a.Type.EndsWith("Trigger")).Type.ToLower() == "httptrigger";
        }

        public static bool IsServiceBusTriggerFunction(this FunctionContext context)
        {
            return context.FunctionDefinition.InputBindings.Values
                          .First(a => a.Type.EndsWith("Trigger")).Type.ToLower() == "servicebustrigger";
        }

        public static bool IsBatchFunction(this FunctionContext context)
        {
            return true;
        }

        public static void AddMessageSB(this FunctionContext context, string message, string topic)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (!context.Items.ContainsKey("messageBody"))
                    context.Items.Add("messageBody", message);
                else
                    context.Items["messageBody"] = message;
            }
            if (!string.IsNullOrEmpty(topic))
            {
                if (!context.Items.ContainsKey("topic"))
                    context.Items.Add("topic", topic);
                else
                    context.Items["topic"] = topic;
            }
        }

        public static void AddBatchInfo(this FunctionContext context, string jobNumber, string currentPage, string totalPages, int numItems)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(jobNumber) || !string.IsNullOrEmpty(currentPage) || !string.IsNullOrEmpty(totalPages))
            {
                if (!string.IsNullOrEmpty(jobNumber))
                    result.Add("JobNumber", jobNumber);

                if (!string.IsNullOrEmpty(currentPage))
                    result.Add("CurrentPage", currentPage);

                if (!string.IsNullOrEmpty(totalPages))
                    result.Add("TotalPages", totalPages);


                result.Add("NumItems", numItems.ToString());

                if (!context.Items.ContainsKey("batchInfo"))
                    context.Items.Add("batchInfo", result);
                else
                    context.Items["batchInfo"] = result;
            }

        }

        public static void AddResponseFunction(this FunctionContext context, ResponseMessage message)
        {
            if (message != null)
            {
                if (!context.Items.ContainsKey("responseFunction"))
                    context.Items.Add("responseFunction", message);
                else
                    context.Items["responseFunction"] = message;
            }
        }

        public static void AddResponseFunction(this FunctionContext context, BatchResponseMessage message)
        {
            if (message != null)
            {
                if (!context.Items.ContainsKey("responseBatchFunction"))
                    context.Items.Add("responseBatchFunction", message);
                else
                    context.Items["responseBatchFunction"] = message;
            }
        }

        public static int GetCustomRetryCountMessage(this FunctionContext context)
        {
            return GetValueFromCustomPropertyServiceBus<int>(context, "retryCount");
        }

        public static int? GetCustomJobNumberMessage(this FunctionContext context)
        {
            return GetValueFromCustomPropertyServiceBus<int?>(context, "JobNumber");
        }

        public static int? GetCustomCurrentPageMessage(this FunctionContext context)
        {
            return GetValueFromCustomPropertyServiceBus<int?>(context, "CurrentPage");
        }

        public static string GetTopic(this FunctionContext context)
        {
            string result = string.Empty;

            if (context.Items.TryGetValue("topic", out var topic))
                result = topic.ToString();
            return result;
        }

        public static Dictionary<string, string> GetBatchInfo(this FunctionContext context)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (context.Items.TryGetValue("batchInfo", out var batchInfo))
                result = (Dictionary<string, string>)batchInfo;
            return result;
        }

        #region Private

        private static T GetValueFromCustomPropertyServiceBus<T>(FunctionContext context, string keyProperty)
        {
            IReadOnlyDictionary<string, object> bindingData = context.BindingContext.BindingData;
            int retryCount;

            if (bindingData.ContainsKey("UserProperties"))
            {
                string userPropertiesStr = bindingData["UserProperties"].ToString();
                JsonDocument json = JsonDocument.Parse(userPropertiesStr);
                if (json.RootElement.TryGetProperty(keyProperty, out var userProperty))
                    return userProperty.Deserialize<T>();
            }

            return default;
        }

        #endregion
    }
}
