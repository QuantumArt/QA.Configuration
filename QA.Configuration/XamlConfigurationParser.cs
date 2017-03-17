using System.IO;
using System.Xaml;

namespace QA.Configuration
{
	public static class XamlConfigurationParser
	{
		public static object CreateFrom(string text)
		{
			return XamlServices.Parse(text);
		}

		public static object CreateFrom(Stream stream)
		{
			using (var reader = new StreamReader(stream))
			{
				return CreateFrom(reader.ReadToEnd());
			}
		}

		public static string CreateFromObject(object obj)
		{
			return XamlServices.Save(obj);
		}
	}
}
