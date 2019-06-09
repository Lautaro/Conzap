using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Conzap.Tools
{
    public class ConzapObjectMenu<T> : ConzapObjectPrinter<T>
    {
        public Func<T, string> TitleFactory { get; set; }
        public string Header { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public bool ClearScreen { get; set; }

        public ConzapObjectMenu(IEnumerable<T> objects, Func<T, string> titleFactory, string header = "", string message = "", string errorMessage = "Not an option. Try again..", bool clearSreen = false) : base(objects)
        {
            Header = header;
            Message = message;
            ErrorMessage = errorMessage;
            ClearScreen = clearSreen;
            TitleFactory = titleFactory;
        }

        public override void Run()
        {
            var item = ConzapTools.AskForListChoice<T>(Objects, TitleFactory, Header, Message, ErrorMessage, ClearScreen);
            Conzap.ConzapToolHelpers.ClearScreen(ClearScreen);
            PrintThese.PrintObject<T>(item);

        }

        public override ConzapObjectPrinter<T> AddAndRun(string label, Func<T, string> printThis)
        {
            PrintThese.Add(new ConzapPrintThis<T>(label, printThis));
            this.Run();
            return this;
        }
    }

    public class ConzapObjectPrinter<T>
    {
        public List<ConzapPrintThis<T>> PrintThese { get; set; } = new List<ConzapPrintThis<T>>();
        public IEnumerable<T> Objects { get; set; }
        public ConzapObjectPrinter(IEnumerable<T> objects)
        {
            Objects = objects;

        }
        public ConzapObjectPrinter<T> Add(string label, Func<T, string> printThis)
        {
            PrintThese.Add(new ConzapPrintThis<T>(label, printThis));
            return this;
        }

        public virtual ConzapObjectPrinter<T> AddAndRun(string label, Func<T, string> printThis)
        {
            PrintThese.Add(new ConzapPrintThis<T>(label, printThis));
            Run();
            return this;
        }

        public virtual void Run()
        {
            if (Objects == null)
            {
                throw new Exception("Objects cant be null. Specify a collection of T that should be printed.");

            }
            PrintThese.PrintObjectList<T>(Objects);
        }
    }

    public class ConzapTypePrinter<T>
    {
        public bool UseAttributes { get; set; }
        public bool UseReflection { get; set; }
        public bool UseCustomFields { get; set; }
        public List<T> Objects { get; set; }
        public List<string> IgnoreThese { get; set; } = new List<string>();
        public List<ConzapPrintThis<T>> CustomFields { get; set; }

        public ConzapTypePrinter(List<T> objects, bool useAttribute = true, bool useReflection = false, bool useCustomFields = true)
        {
            UseAttributes = useAttribute;
            UseReflection = useReflection;
            UseCustomFields = useCustomFields;
            Objects = objects;
        }

        public ConzapTypePrinter<T> AddField(string customFieldName, Func<T, string> customFieldValue)
        {
            CustomFields.Add(new ConzapPrintThis<T>(customFieldName, customFieldValue));
            return this;
        }

        public void Run()
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

            var attribute = property.GetCustomAttributes(typeof(ConzapPropertyAttribute), true).First() as ConzapPropertyAttribute;
            var ignoreThisFieldAttribute = attribute != null && attribute.Ignore == true;

            if (!ignoreThisFieldAttribute || !IgnoreThese.Any(name => name.ToLower() == property.Name.ToLower()))
            {
                // 1. Use attribute title and value if UseAttribute is enabled 
                // 2. The fields name is not on ignore list
                if (UseAttributes)
                {
                    fieldTitle = attribute.Title;
                    fieldValue = property.GetValue(item).ToString();
                }

                // User reflection if 
                // 1. Reflection is enabled 
                // 2. There is no IgnoreAttribute 
                // 3. The fields name is not on ignore list
                if (UseReflection && !UseAttributes)
                {
                    fieldTitle = property.Name;
                    fieldValue = property.GetValue(item).ToString();
                }
            }

            return new KeyValuePair<string, string>(fieldTitle, fieldValue);
        }
    }
}
