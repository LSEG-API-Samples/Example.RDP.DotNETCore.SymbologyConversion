using Newtonsoft.Json;

namespace Refinitiv.EDP.Example.Symbology.Convert
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.13.18.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class HeaderData 
    {
        [Newtonsoft.Json.JsonProperty("decimalChar", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DecimalChar { get; set; }
    
        [Newtonsoft.Json.JsonProperty("description", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Description { get; set; }
    
        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Name { get; set; }
    
        [Newtonsoft.Json.JsonProperty("title", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Title { get; set; }
    
        [Newtonsoft.Json.JsonProperty("type", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Type { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    
        public static HeaderData FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<HeaderData>(data);
        }
    
    }
}