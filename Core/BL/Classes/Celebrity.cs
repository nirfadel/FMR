using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BL
{
   
    public class Celebrity
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "DateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PersonGender Gender  { get; set; }

        [JsonProperty(PropertyName = "Role")]
        public string Role { get; set; }

        [JsonProperty(PropertyName = "Photo")]
        public string Photo { get; set; }
    }

    public enum PersonGender
    {
        OTHER = 0,
        MALE = 1,
        FEMALE = 2
    }

   
}
