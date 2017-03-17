// Owners: Karlov Nikolay

using System.Xaml;

namespace QA.Configuration
{
    /// <summary>
    /// Класс с конфигурационными настройками, которые могут применяться к объектам с помощью AttachedProperty.
    /// Syntax: x:Config.AttachedObject
    /// </summary>
    public class Config
    {
        #region Config.Priority
        public static int GetPriority(object item)
        {
            int obj = default(int);
            AttachablePropertyServices.TryGetProperty<int>(item, new AttachableMemberIdentifier(typeof(int), "Priority"), out obj);
            return obj;
        }

        public static void SetPriority(object item, int obj)
        {
            AttachablePropertyServices.SetProperty(item, new AttachableMemberIdentifier(typeof(int), "Priority"), obj);
        }
        #endregion

        #region Config.AttachedObject

        /// <summary>
        /// Получение присоединенного к элементу графа объектов объекта
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static object GetAttachedObject(object item)
        {
            object obj = default(object);
            AttachablePropertyServices.TryGetProperty<object>(
              item,
              new AttachableMemberIdentifier(typeof(object), "AttachedObject"),
              out obj);
            return obj;
        }

        /// <summary>
        /// Присоединение произвольного объекта к элементу графа объектов
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static void SetAttachedObject(object book, object item)
        {
            AttachablePropertyServices.SetProperty(
              book,
              new AttachableMemberIdentifier(typeof(object), "AttachedObject"),
              item);
        }
        #endregion
    }
}
