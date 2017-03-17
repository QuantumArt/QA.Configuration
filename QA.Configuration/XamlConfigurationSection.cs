// Owners: Karlov Nikolay

using System.Configuration;
using System.Xaml;
using System.Xml;

namespace QA.Configuration
{
    /// <summary>
    /// Обработчик Xaml-секции конфигурационного файла
    /// </summary>
    public class XamlConfigurationSection : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            return XamlServices.Parse(section.OuterXml);
        }
    }
}
