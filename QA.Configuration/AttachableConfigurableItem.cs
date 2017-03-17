// Owners: Karlov Nikolay

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Xaml;

namespace QA.Configuration
{
    /// <summary>
    /// Базовый класс для контейнера конфигурации.
    /// Поддерживает поддержку ресурсных словарей и
    /// является хранилищем для собственных AttachedProperties
    /// </summary>
    public abstract class AttachableConfigurableItem : ConfigurableItem, IAttachedPropertyStore
    {
        #region IAttachedPropertyStore Members
        /// <summary>
        /// Словарь значений
        /// </summary>
        protected ConcurrentDictionary<AttachableMemberIdentifier, object> attachedProperties =
            new ConcurrentDictionary<AttachableMemberIdentifier, object>();

        /// <summary>
        /// Копирование свойств
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyPropertiesTo(KeyValuePair<AttachableMemberIdentifier, object>[] array,
                                     int index)
        {
            ((IDictionary<AttachableMemberIdentifier, object>)attachedProperties).CopyTo(array, index);
        }

        /// <summary>
        /// Количество свойств
        /// </summary>
        public int PropertyCount
        {
            get { return attachedProperties.Count; }
        }

        public bool RemoveProperty(AttachableMemberIdentifier member)
        {
            object value;
            return attachedProperties.TryRemove(member, out value);
        }

        public void SetProperty(AttachableMemberIdentifier member, object value)
        {
            attachedProperties[member] = value;
        }

        public bool TryGetProperty(AttachableMemberIdentifier member, out object value)
        {
            return attachedProperties.TryGetValue(member, out value);
        }
        #endregion
    }
}
