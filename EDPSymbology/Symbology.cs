using Newtonsoft.Json;

namespace Refinitiv.EDP.Example.Symbology.Convert
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.13.18.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class Symbology 
    {
        [Newtonsoft.Json.JsonProperty("attributes", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<System.Collections.Generic.ICollection<object>> Attributes { get; set; }
    
        [Newtonsoft.Json.JsonProperty("data", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
       
        public System.Collections.Generic.ICollection<System.Collections.Generic.ICollection<dynamic>> Data { get; set; }
    
        [Newtonsoft.Json.JsonProperty("headers", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<HeaderData> Headers { get; set; }
    
        [Newtonsoft.Json.JsonProperty("links", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Links Links { get; set; }
    
        [Newtonsoft.Json.JsonProperty("messages", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Messages Messages { get; set; }
    
        [Newtonsoft.Json.JsonProperty("universe", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public System.Collections.Generic.ICollection<UniverseResponseData> Universe { get; set; }
    
        [Newtonsoft.Json.JsonProperty("variability", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public SymbologyVariability? Variability { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this,Formatting.Indented);
        }
    
        public static Symbology FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Symbology>(data);
        }
    
    }
}