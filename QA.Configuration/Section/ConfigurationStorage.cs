using System.Collections.Generic;
using System.Windows.Markup;

namespace QA.Configuration
{
    /// <summary>
    /// Хранилище конфигурации
    /// </summary>
    [ContentProperty("Items")]
    public class ConfigurationStorage : ConfigurableItem
    {
        /// <summary>
        /// Элементы
        /// </summary>
        public IDictionary<string, object> Items{ get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public ConfigurationStorage()
        {
            Items = new Dictionary<string, object>();
        }
    }
}
