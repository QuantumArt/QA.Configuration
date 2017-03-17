// Owners: Karlov Nikolay

using System.Configuration;

namespace QA.Configuration
{
    /// <summary>
    /// Методы работы с xaml-конфигурацией
    /// </summary>
    public static class XamlConfigurationManager
    {
        /// <summary>
        /// Получение xaml-конфигурации объекта из app.config
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns></returns>
        public static T GetSection<T>()
        {
            var typeName = typeof(T).Name;
            return (T)ConfigurationManager.GetSection(typeName);
        }
    }
}
