namespace DEMO.API.Functions.Helpers.Types.Parsers
{
    public interface IParserExecutionContextPlugin
    {
        T getValueForKey<T>(string source, string key);
    }
}
