namespace Refinitiv.EDP.Example.Symbology.Convert
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.13.18.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class Messages 
    {
        [Newtonsoft.Json.JsonProperty("codes", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<System.Collections.Generic.ICollection<int>> Codes { get; set; }
    
        [Newtonsoft.Json.JsonProperty("descriptions", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<MessageDescription> Descriptions { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    
        public static Messages FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Messages>(data);
        }
    
    }
}