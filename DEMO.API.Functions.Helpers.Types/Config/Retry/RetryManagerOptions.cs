namespace DEMO.API.Functions.Helpers.Types.Config.Retry
{
    public sealed class RetryManagerOptions
    {
        public int TopicMaxSizeInMegabytes { get; set; }
        public int DefaultTopicMessageTimeToLive { get; set; }
        public int MaxRetryCount { get; set; }
        public int ScheduleIntervalLinealRetryInMinutes { get; set; }
        public int ScheduleIntervalTransientErrorsRetryInMinutes { get; set; }

    }
}
