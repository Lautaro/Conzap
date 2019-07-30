using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Conzap.ObjectPrinting
{
    public class ObjectPrinter<T>
    {
        #region Configuration
        public bool UseAttributes { get; set; } = true;
        public bool UseUnattributedProperties { get; set; } = true;
        public bool UseCustomFields { get; set; } = true;
        #endregion

        public List<T> Objects { get; set; }
        public List<string> IgnoreThese { get; set; } = new List<string>();
        public List<ObjectPrinterField<T>> CustomFields { get; set; }

        public ObjectPrinter(List<T> objects, bool useAttributes = true, bool useUnattributedProperties = true, bool useCustomFields = true)
        {
            UseAttributes = useAttributes;
            UseUnattributedProperties = useUnattributedProperties;
            UseCustomFields = useCustomFields;
            Objects = objects;
        }

        public ObjectPrinter<T> AddField(string customFieldName, Func<T, string> customFieldValue)
        {
            CustomFields.Add(new ObjectPrinterField<T>(customFieldName, customFieldValue));
            return this;
        }

        public void Print()
        {
            var parsedObjects = new List<Dictionary<string, string>>();
            var type = typeof(T);

            foreach (var item in Objects.Select(o => (T)o))
            {
                var currentItem = new Dictionary<string, string>();
                var properties = type.GetProperties();

                foreach (var property in properties)
                {
                    var kvpField = ParseProperty(property, item);
                    if (!string.IsNullOrEmpty(kvpField.Key))
                    {
                        currentItem.Add(kvpField.Key, kvpField.Value);
                    }
                }
                if (CustomFields != null)
                {
                    foreach (var customField in CustomFields)
                    {
                        var label = customField.Label;
                        var value = customField.PrintThis(item);
                        currentItem.Add(label, value);
                    }
                }

                parsedObjects.Add(currentItem);
            }

            foreach (var item in parsedObjects)
            {

                ConzapTools.PrintList("    ", item.Select(kvp => $"{kvp.Key} : {kvp.Value}").ToArray());
            }

            ConzapTools.PauseForKey();

        }


        private KeyValuePair<string, string> ParseProperty(PropertyInfo property, T item)
        {
            var fieldTitle = "";
            var fieldValue = "";

            var attribute = property.GetCustomAttributes(typeof(ObjectPrinterPropertyAttribute), true).FirstOrDefault() as ObjectPrinterPropertyAttribute;
            var ignoreThisFieldAttribute = attribute != null && attribute.Ignore == true;

            if (!ignoreThisFieldAttribute || !IgnoreThese.Any(name => name.ToLower() == property.Name.ToLower()))
            {
                // 1. Use attribute title and value if UseAttribute is enabled 
                // 2. The fields name is not on ignore list
                if (UseAttributes && attribute != null)
                {
                    fieldTitle = attribute.Title;
                    fieldValue = property.GetValue(item).ToString();
                }

                // User reflection if 
                // 1. Reflection is enabled 
                // 2. There is no IgnoreAttribute 
                // 3. The fields name is not on ignore list
                // 4. The field has no attribute
                if (UseUnattributedProperties && attribute == null)
                {
                    fieldTitle = property.Name;
                    fieldValue = property.GetValue(item).ToString();
                }
            }

            return new KeyValuePair<string, string>(fieldTitle, fieldValue);
        }
    }
}
