
using System.Collections.Generic;
namespace QA.Configuration
{
    /// <summary>
    /// Поставщик конфигурации
    /// </summary>
    public interface IConfigurationService
    {
        /// <summary>
        /// Получить стандартный объект конфигурации про типу
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        T GetConfiguration<T>();

        /// <summary>
        /// Получить именованный объект конфигурации про типу
        /// </summary>
        /// <typeparam name="T">Тип</typeparam> T 
        T GetConfiguration<T>(string key);

        /// <summary>
        /// Получить список объектов конфигурации про типу
        /// </summary>
        /// <typeparam name="T">Тип</typeparam> T 
        List<T> GetConfigurations<T>();
    }
}
