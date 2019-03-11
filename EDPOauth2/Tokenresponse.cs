namespace Refinitiv.EDP.Example.AuthOauth2
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.13.18.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class Tokenresponse 
    {
        [Newtonsoft.Json.JsonProperty("access_token", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Access_token { get; set; }
    
        [Newtonsoft.Json.JsonProperty("expires_in", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Expires_in { get; set; }
    
        [Newtonsoft.Json.JsonProperty("refresh_token", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Refresh_token { get; set; }
    
        [Newtonsoft.Json.JsonProperty("scope", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Scope { get; set; }
    
        [Newtonsoft.Json.JsonProperty("token_type", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Token_type { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this,Newtonsoft.Json.Formatting.Indented);
        }
    
        public static Tokenresponse FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Tokenresponse>(data);
        }
    
    }
}