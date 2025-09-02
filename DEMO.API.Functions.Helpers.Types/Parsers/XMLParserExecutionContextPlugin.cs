namespace DEMO.API.Functions.Helpers.Types.Parsers
{
    using System.ComponentModel;
    using System.Xml;

    public class XMLParserExecutionContextPlugin : IParserExecutionContextPlugin
    {
        private string _source = string.Empty;
        private XmlDocument _xml = null;
        public XMLParserExecutionContextPlugin()
        {
        }

        public T getValueForKey<T>(string source, string key)
        {
            if (string.IsNullOrEmpty(_source))
            {
                //Cargamos el source como XML
                _source = source;
                _xml = new XmlDocument();
                _xml.LoadXml(source);
            }

            if (_xml != null)
            {
                var elements = _xml.GetElementsByTagName(key);
                if (elements.Count > 0)
                {
                    return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(elements[0].InnerText);
                }
            }
            else
                throw new Exception("No se ha podido parsear XML:" + source);

            return default(T);
        }
    }
}
