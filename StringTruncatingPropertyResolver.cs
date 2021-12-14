using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApiWorkshop.Helpers
{
    /// <summary>
    /// This class automatically truncates all the string properties to the value specified by the "MaxLength" attribute
    /// of the string property in a particular SAP object.
    /// For details, see
    /// https://stackoverflow.com/questions/54119426/string-truncation-during-json-deserialization-and-ado-persistance
    /// </summary>
    public class StringTruncatingPropertyResolver : DefaultContractResolver
    {
        public int DefaultMaxLength { get; private set; }

        public StringTruncatingPropertyResolver(int defaultMaxLength)
        {
            DefaultMaxLength = defaultMaxLength;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {

            return type.GetProperties()
                .Select(p =>
                {
                    var jp = base.CreateProperty(p, memberSerialization);
                    var attr = jp.AttributeProvider
                               .GetAttributes(true)
                               .OfType<MaxLengthAttribute>()
                               .FirstOrDefault();
                    int maxLength = attr != null ? attr.Length : DefaultMaxLength;
                    jp.ValueProvider = new StringTruncatingValueProvider(p, maxLength);
                    return jp;
                }).ToList();            
        }
    }

    public class StringTruncatingValueProvider : IValueProvider
    {
        PropertyInfo _MemberInfo;
        private int MaxLength { get; set; }



        public StringTruncatingValueProvider(PropertyInfo memberInfo, int maxLength)
        {
            _MemberInfo = memberInfo;
            MaxLength = maxLength;
        }

        // GetValue is called by Json.Net during serialization.
        // The target parameter has the object from which to read the string;
        // the return value is a string that gets written to the JSON.
        public object GetValue(object target)
        {
            object result = _MemberInfo.GetValue(target);
            if (_MemberInfo.PropertyType == typeof(string))
            {
                if(result == null)
                    result = "";
                else if (((string)result).Length > MaxLength)
                    result = ((string)result).Substring(0, MaxLength);
            }
            return result;
        }

        // SetValue gets called by Json.Net during deserialization.
        // The value parameter has the string value read from the JSON;
        // target is the object on which to set the (possibly truncated) value.
        public void SetValue(object target, object value)
        {
            string s = (string)value;
            if (s != null && s.Length > MaxLength)
            {
                s = s.Substring(0, MaxLength);
            }
            

            _MemberInfo.SetValue(target, s);
        }
    }


}
