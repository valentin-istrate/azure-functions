namespace Demo.SalesAnalyzerDurableFunction.Constants
{
    public static class CustomStatus
    {
        public static readonly string DataAnalysis = nameof(DataAnalysis);
        public static readonly string StoreData = nameof(StoreData);
        public static readonly string AwaitingApproval = nameof(AwaitingApproval);
        public static readonly string Completed = nameof(Completed);
    }
}