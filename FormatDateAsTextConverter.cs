using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace ApiWorkshop.Helpers
{
    /// <summary>
    /// Custom converter for rendering all Date fields as yyyyMMdd formatted text in JSON
    /// </summary>
    internal sealed class FormatDateAsTextConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanRead => false;
        public override bool CanWrite => true;
        public override bool CanConvert(Type type) => type == typeof(DateTime);

        public override void WriteJson(
            JsonWriter writer, object value, JsonSerializer serializer)
        {
            DateTime d = (DateTime)value;

            writer.WriteValue(d.ToString("yyyyMMdd"));
        }

        public override object ReadJson(
            JsonReader reader, Type type, object existingValue, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
    }
}
