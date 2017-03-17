// Owners: Karlov Nikolay

using System.Windows.Markup;
namespace QA.Configuration
{
    /// <summary>
    /// Интерфейс класса с ресурсным словарем, конфигурируемого с помощью Xaml
    /// </summary>
    public interface IResourceContainer
    {
        bool HasResources { get; }
        [Ambient]
        ResourceDictionary Resources { get; }
        bool TryGetResource(object key, out object value);
    }
}
