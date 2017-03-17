// Owners: Karlov Nikolay

using System.Windows.Markup;

namespace QA.Configuration
{
    /// <summary>
    /// Базовый класс для контейнера конфигурации.
    /// Поддерживает поддержку ресурсных словарей
    /// </summary>
    public abstract class ConfigurableItem : IResourceContainer
    {
        private ConfigurableItem _parent;
        private ResourceDictionary _resources;

        internal ConfigurableItem Parent
        {
            get { return _parent; }
        }

        /// <summary>
        /// Ресурсный словарь.
        /// </summary>
        [Ambient]        
        public ResourceDictionary Resources
        {
            get
            {
                if (_resources == null)
                {
                    _resources = new ResourceDictionary(this);
                }

                return _resources;
            }
        }

        public bool HasResources
        {
            get
            {
                return _resources != null && _resources.Count > 0;
            }
        }

        internal void SetParent(ConfigurableItem parent)
        {
            _parent = parent;
        }

        public bool TryGetResource(object key, out object value)
        {
            if (!HasResources)
            {
                value = null;
                return false;
            }

            return _resources.TryGetValue(key, out value);
        }
    }
}
