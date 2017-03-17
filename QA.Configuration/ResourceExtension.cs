// Owners: Karlov Nikolay

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Markup;
using System.Xaml;

namespace QA.Configuration
{
    /// <summary>
    /// Осуществляет поиск ресурса в ресурсоном словаре текущего или корневого элемента
    /// </summary>
    public class ResourceExtension : MarkupExtension
    {
        /// <summary>
        /// Ключ
        /// </summary>
        public object Key { get; set; }

        public ResourceExtension() { }
        public ResourceExtension(string key) { Key = key; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            object result = null;

            if (result == null)
            {
                var dict = FindDictionary(serviceProvider, Key);
                if (dict != null && dict.TryGetValue(Key, out result))
                {
                    return result;
                }

                throw new KeyNotFoundException(string.Format("An item with the given key '{0}' is not found.", Key));
            }

            return result;
        }

        /// <summary>
        /// Поиск ресурсного словаря. Поддерживаются вложенные ресурсные словари, а также ресурсные словари родительских объектов.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static ResourceDictionary FindDictionary(IServiceProvider serviceProvider, object key)
        {
            var xamlSchemaContextProvider = (IXamlSchemaContextProvider)serviceProvider.GetService(typeof(IXamlSchemaContextProvider));

            if (xamlSchemaContextProvider == null)
            {
                throw new InvalidOperationException(string.Format("The service {0} cannot be resolved.", typeof(IXamlSchemaContextProvider)));
            }

            var ambientProvider = (IAmbientProvider)serviceProvider.GetService(typeof(IAmbientProvider));

            if (ambientProvider == null)
            {
                throw new InvalidOperationException(string.Format("The service {0} cannot be resolved.", typeof(IAmbientProvider)));
            }

            XamlSchemaContext schemaContext = xamlSchemaContextProvider.SchemaContext;
            XamlType xamlType = schemaContext.GetXamlType(typeof(IResourceContainer));
            XamlMember ambientMember = xamlType.GetMember("Resources");
            XamlType[] types = new XamlType[]
			{
				schemaContext.GetXamlType(typeof(ResourceDictionary))
			};

            IEnumerable<AmbientPropertyValue> allAmbientValues = ambientProvider.GetAllAmbientValues(null, false, types, new XamlMember[]
			{				
                ambientMember,
			});

            List<AmbientPropertyValue> list = allAmbientValues as List<AmbientPropertyValue>;

            for (int i = 0; i < list.Count; i++)
            {
                AmbientPropertyValue ambientPropertyValue = list[i];
                if (ambientPropertyValue.Value is ResourceDictionary)
                {
                    ResourceDictionary resourceDictionary = (ResourceDictionary)ambientPropertyValue.Value;
                    if (resourceDictionary.ContainsKey(key))
                    {
                        return resourceDictionary;
                    }
                }
            }
            return null;
        }

        private static object FindClosestValue(IResourceContainer configurableItem, object key)
        {
            object value = null;

            if (configurableItem.TryGetResource(key, out value))
            {
                return value;
            }

            if (configurableItem is ConfigurableItem)
            {
                if (((ConfigurableItem)configurableItem).Parent != null)
                {
                    return FindClosestValue(((ConfigurableItem)configurableItem).Parent, key);
                }
            }

            return null;
        }

        /// <summary>
        /// Осуществляет поиск root-элемента с помощью Reflection
        /// </summary>
        [Obsolete]
        private object FindRoot(object configurableItem, IServiceProvider serviceProvider)
        {
            var root = serviceProvider.GetPrivateFieldValue("_xamlContext").GetPrivatePropertyValue("RootInstance");

            return root;
        }
    }

    #region Reflection HElpers (не используются)

    internal static class ReflectionHelpers
    {
        static Dictionary<Type, FieldInfo> _fields = new Dictionary<Type, FieldInfo>();
        static Dictionary<Type, PropertyInfo> _properties = new Dictionary<Type, PropertyInfo>();

        public static object GetPrivatePropertyValue(this object obj, string propName)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var type = obj.GetType();
            PropertyInfo pi;

            if (_properties.ContainsKey(type))
            {
                pi = _properties[type];
            }
            else
            {
                pi = type.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (pi != null)
                {
                    _properties[type] = pi;
                }
            }

            if (pi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
            return pi.GetValue(obj, null);
        }

        public static object GetPrivateFieldValue(this object obj, string propName)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            Type t = obj.GetType();
            FieldInfo fi = null;
            if (_fields.ContainsKey(t))
            {
                fi = _fields[t];
            }
            else
            {
                while (fi == null && t != null)
                {
                    fi = t.GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    t = t.BaseType;
                }

                if (fi != null)
                {
                    _fields[t] = fi;
                }
            }

            if (fi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Field {0} was not found in Type {1}", propName, obj.GetType().FullName));
            return fi.GetValue(obj);
        }

    }

    #endregion
}
