using Conzap.ViewStyling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Conzap.ObjectPrinting
{
    public class ObjectPrinter<T>
    {
        #region Configuration

        /// <summary>
        /// Defaults to true. Allows fields to use the alternative title in a ObjectPrinterPropertyAttribute
        /// </summary>
        public bool UseAttributes { get; set; } = true;

        /// <summary>
        /// Defaults to true. Allows fields from object properties that have no ObjectPrinterPropertyAttribute
        /// </summary>
        public bool UseUnattributedProperties { get; set; } = true;

        /// <summary>
        /// Defaults to false. If true only specified custom fields will be used.
        /// </summary>
        public bool UseOnlyCustomFields { get; set; } = false;
        #endregion

        #region Public properties
        
        public List<T> Objects { get; set; } = new List<T>();
        public List<ObjectPrinter<object>> NesteObjectPrinters { get; set; } = new List<ObjectPrinter<object>>();

        #endregion

        #region Private properties
        public Type TypeOfThis { get; }
        List<PropertyInfo> IgnoreThese { get; set; } = new List<PropertyInfo>();
        Func<T, string> _itemHeadingFactory;
        List<ObjectPrinterField<T>> CustomFields { get; set; } = new List<ObjectPrinterField<T>>();
        #endregion

        #region Constructors
        public ObjectPrinter(List<T> objects, bool useAttributes = true, bool useUnattributedProperties = true, bool useOnlyCustomFields = false)
        {
            UseAttributes = useAttributes;
            UseUnattributedProperties = useUnattributedProperties;
            UseOnlyCustomFields = useOnlyCustomFields;
            Objects = objects;
            TypeOfThis = typeof(T);
        }

        public ObjectPrinter(T printObject, bool useAttributes = true, bool useUnattributedProperties = true, bool useOnlyCustomFields = false)
        {
            UseAttributes = useAttributes;
            UseUnattributedProperties = useUnattributedProperties;
            UseOnlyCustomFields = useOnlyCustomFields;
            Objects.Add(printObject);
        }
        #endregion

        #region Custom fields
        public ObjectPrinter<T> CustomField(string customFieldName, Func<T, string> customFieldValue)
        {
            CustomFields.Add(new ObjectPrinterField<T>(customFieldName, customFieldValue));
            return this;
        }

        public ObjectPrinter<T> ClearAllCustomFields()
        {
            CustomFields.Clear();
            return this;
        }
        #endregion

        #region IgnoreProperty
        public ObjectPrinter<T> IgnoreProperty(Expression<Func<T, object>> ignoreProprty)
        {
           var propertyInfo =  Helper.GetPropertyFromExpression<T>(ignoreProprty);
            if (!IgnoreListContains(propertyInfo))
            {
                IgnoreThese.Add(propertyInfo);
            }
            
            return this;
        }

        bool IgnoreListContains(PropertyInfo pi)
        {
            return IgnoreThese.Any(i => i.Name == pi.Name);
        }

        public ObjectPrinter<T> RemoveFromIgnoreList(Expression<Func<T, object>> ignoreProprty)
        {
            var propertyInfo = Helper.GetPropertyFromExpression<T>(ignoreProprty);

            IgnoreThese.RemoveAll(i => i.Name == propertyInfo.Name);
            return this;
        }
        #endregion

        #region Print


        /// <summary>
        /// Prints only the object at index
        /// </summary>
        public ObjectPrinter<T> Print(int index)
        {
            if ((Objects.ElementAtOrDefault(index) != null))
            {
                ActuallyPrint(new List<T>());
            }
            else
            {
                throw new Exception("Objects list does not contain an index " + index);
            }
            
            return this;
        }

        /// <summary>
        /// Prints all objects
        /// </summary>
        public ObjectPrinter<T> Print()
        {
                ActuallyPrint(Objects);

            return this;
        }

        //public ObjectPrinter<T>PrintMenu(Func<T, string> objectTitleFabric, string heading = null, bool clearScreen = true)
        //{
        //    ConzapToolHelpers.ClearScreen(clearScreen);
        //    ConzapToolHelpers.PrintHeading(heading);
        //    var stringList = Objects.Select(o => objectTitleFabric(o)).ToList();
        //    stringList.Insert(0, "Quit");

        //    while (true)
        //    {
        //        var chosenIndex = Choose.ChooseFromList(stringList.ToArray());
        //        if (chosenIndex == 0)
        //        {
        //            return this;
        //        }

        //        var item = Objects.ToArray()[chosenIndex];
        //        new ObjectPrinter<T>(new List<T>() { item }).Print();

        //        Misc.PauseForKey();
        //    }
        //}

        /// <summary>
        /// Prints all objects
        /// </summary>
        private void ActuallyPrint(List<T> ObjectsToPrint)
        {
            var parsedObjects = new List<Dictionary<string, string>>();
            var type = typeof(T);

            
            // ADD FIELDS FOR EACH ITEM
            foreach (var item in ObjectsToPrint.Select(o => (T)o))
            {
                var currentItem = new Dictionary<string, string>();
                var properties = type.GetProperties();

                // ADD HEADER IF HEADER USED
                if (_itemHeadingFactory !=  null)
                {
                    currentItem.Add(_itemHeadingFactory(item), "");
                }

                // CHECK FOR TYPE DEFINITION
                var itemType = Helper.GetListType(item.GetType());
                var exists = 

                // IF HAS TYPE DEFINITION RECURSIVE PRINT AND EXIT TO NEST ITEM


                // ADD FROM CUSTOM FIELDS
                if (CustomFields != null)
                {
                    foreach (var customField in CustomFields)
                    {
                        var label = customField.Label;
                        var value = customField.PrintThis(item);
                        currentItem.Add(label, value);
                    }
                }


                // EXIT IF ONLY CUSTOM FIELDS
                if (UseOnlyCustomFields)
                {
                    continue;
                }

                // AUTO GENERATE FIELD IF NOT EXITED - LAST RESORT
                foreach (var property in properties)
                {
                    if (IgnoreThese.Any( i => i.Name == property.Name))
                        continue;

                    var kvpField = ParseProperty(property, item);

                    if (!string.IsNullOrEmpty(kvpField.Key))
                    {
                        currentItem.Add(kvpField.Key, kvpField.Value);
                    }
                }

                parsedObjects.Add(currentItem);
            }


            // PRINT EACH OBJECT
            foreach (var dic in parsedObjects)
            {
                var items = dic.Select(kvp =>
                {
                    var key = string.IsNullOrEmpty(kvp.Key) ? "" : kvp.Key + " : ";
                    return key + kvp.Value;
                });
                ConzapTools.PrintHeading(items.ElementAt(0), false);
                ConzapTools.PrintList(items.Skip(1).ToArray());
                ConzapTools.SkipLines(1);
            }

            ConzapTools.PauseForKey();
        }


        private KeyValuePair<string, string> ParseProperty(PropertyInfo property, T item)
        {
            var fieldTitle = "";
            var fieldValue = "";

            var attribute = property.GetCustomAttributes(typeof(ObjectPrinterPropertyAttribute), true).FirstOrDefault() as ObjectPrinterPropertyAttribute;
            var hasAttribute = attribute != null ;
            var onIgnoreList = IgnoreThese.Any(ignore => ignore.Name == property.Name);

            if (!onIgnoreList)
            {
                // 1. Use attribute title and value if UseAttribute is enabled 
                // 2. The fields name is not on ignore list
                if (UseAttributes && hasAttribute)
                {
                    fieldTitle = attribute.Title;
                    fieldValue = property.GetValue(item).ToString();
                }

                // User reflection if 
                // 1. Reflection is enabled 
                // 2. The fields name is not on ignore list
                // 3. Attribute has not been used
                
                if (UseUnattributedProperties || (!UseAttributes && hasAttribute ))
                {
                    fieldTitle = property.Name;
                    fieldValue = property.GetValue(item).ToString();
                }
            }

            return new KeyValuePair<string, string>(fieldTitle, fieldValue);
        }
        #endregion

        #region 
        bool HasObjectPrinterForType (Type T)
        {
            foreach (var op in NesteObjectPrinters)
            {
                var type = op.GetType().GetGenericArguments()[0];

                if (T == type)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Configure ObjectPrinter

        public ObjectPrinter<T> ItemHeadingFactory(Func<T, string> itemHeadingFactory)
        {
            _itemHeadingFactory = itemHeadingFactory;
            return this;
        }
        public ObjectPrinter<T> Configure(ObjectPrinterOptions options)
        {
            bool containsOption(ObjectPrinterOptions match)
            {
                return (options & match) == match;
            }

            if (containsOption(ObjectPrinterOptions.IgnoreAttributes))
                UseAttributes = false;

            if (containsOption(ObjectPrinterOptions.IgnoreUnattributedProperties))
                UseUnattributedProperties = false;

            if (containsOption(ObjectPrinterOptions.UseAttributes))
                UseAttributes = true;

            if (containsOption(ObjectPrinterOptions.UseUnattributedProperties))
                UseUnattributedProperties = true;

            return this;
        }

        #endregion
    }

    [System.Flags]
    public enum ObjectPrinterOptions
    {
        UseAttributes = 1,
        IgnoreAttributes = 2,
        UseUnattributedProperties = 3, 
        IgnoreUnattributedProperties = 4,
        UseOnlyCustomFields = 5,
        UseAllFieldTypes = 6
     }
}
