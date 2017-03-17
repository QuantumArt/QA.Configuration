using System.Collections.Generic;
using System.Linq;

namespace QA.Configuration
{
    /// <summary>
    /// Конфигурационный сервис.
    /// </summary>
    public class ConfigurationService : IConfigurationService
    {
        private ConfigurationStorage _section;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ConfigurationService()
        {
            _section = XamlConfigurationManager.GetSection<ConfigurationStorage>();
        }

        /// <summary>
        /// Получить стандартный объект конфигурации про типу
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        public T GetConfiguration<T>()
        {
            return (T)_section.Items.Values
                .Where(x => typeof(T).IsAssignableFrom(x.GetType()))
                .FirstOrDefault();
        }

        /// <summary>
        /// Получить именованный объект конфигурации про типу
        /// </summary>
        /// <typeparam name="T">Тип</typeparam> T 
        public T GetConfiguration<T>(string key)
        {
            object item;
            
            if (_section.Items.TryGetValue(key, out item))
            {
                return (T)item;
            }

            return default(T);
        }

        /// <summary>
        /// Получить список объектов конфигурации про типу
        /// </summary>
        /// <typeparam name="T">Тип</typeparam> T 
        public List<T> GetConfigurations<T>()
        {
            return _section.Items.Values
                .Where(x => typeof(T).IsAssignableFrom(x.GetType()))
                .Cast<T>()
                .ToList();
        }
    }
}
