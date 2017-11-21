using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net
{
    class DateSerializer : JsonConverter
    {

        public override bool CanConvert(Type objectType)
        {
            // This converter handles date values directly
            return (objectType == typeof(DateTime) || objectType == typeof(DateTime?));
        }

        private long GetEpoch(DateTime d)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(d.ToUniversalTime() - epoch).TotalMilliseconds;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // The CanConvert method guarantees the value will be a DateTime
            if(value is DateTime)
            {
                writer.WriteValue(GetEpoch((DateTime)value));
            }
            else if (value is DateTime?)
            {
                DateTime? date = (DateTime?)value;
                if (date.HasValue)
                {
                    writer.WriteValue(GetEpoch(date.Value));
                }
            }
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
