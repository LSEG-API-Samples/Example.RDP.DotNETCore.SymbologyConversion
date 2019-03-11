using Newtonsoft.Json;

namespace Refinitiv.EDP.Example.Symbology.Convert
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.13.18.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class ConvertRequest
    {
        [Newtonsoft.Json.JsonProperty("to", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, ItemConverterType = typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public System.Collections.Generic.ICollection<FieldEnum> To { get; set; }

        [Newtonsoft.Json.JsonProperty("universe", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.Collections.Generic.ICollection<string> Universe { get; set; } = new System.Collections.ObjectModel.Collection<string>();

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public static ConvertRequest FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ConvertRequest>(data);
        }

    }
}