using System;


namespace LitJson
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class JsonElementAttribute: Attribute
    {
        private string _elementName;
        public JsonElementAttribute()
        { }
        public JsonElementAttribute(string elementName)
        {
            _elementName = elementName;
        }
        public string ElementName
        {
            get { return _elementName; }
        }
    }
}
