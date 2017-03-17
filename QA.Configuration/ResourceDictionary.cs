// Owners: Karlov Nikolay

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Markup;

namespace QA.Configuration
{
    /// <summary>
    /// Ресурсный словарь
    /// </summary>
    [Ambient]
    [UsableDuringInitializationAttribute(true)]
    public class ResourceDictionary : ResourceDictionary<object, object>
    {
        public ResourceDictionary(Object parentNode) : base(parentNode) { }
    }
}
