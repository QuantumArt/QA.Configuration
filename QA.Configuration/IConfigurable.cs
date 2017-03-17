// Owners: Karlov Nikolay

namespace QA.Configuration
{
    /// <summary>
    /// Интерфейс класса с ресурсным словарем, конфигурируемого с помощью Xaml
    /// </summary>
    interface IConfigurable
    {
        bool HasResources { get; }
        ResourceDictionary Resources { get; }
        bool TryGetResource(object key, out object value);
    }
}
