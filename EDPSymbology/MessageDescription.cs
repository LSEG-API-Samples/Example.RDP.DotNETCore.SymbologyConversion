namespace Refinitiv.EDP.Example.Symbology.Convert
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.13.18.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class MessageDescription 
    {
        [Newtonsoft.Json.JsonProperty("code", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Code { get; set; }
    
        [Newtonsoft.Json.JsonProperty("description", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Description { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    
        public static MessageDescription FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<MessageDescription>(data);
        }
    
    }
}