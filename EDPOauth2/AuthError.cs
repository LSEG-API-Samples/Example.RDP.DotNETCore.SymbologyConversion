namespace Refinitiv.EDP.Example.AuthOauth2
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.13.18.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class AuthError 
    {
        [Newtonsoft.Json.JsonProperty("error", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Error1 { get; set; }
    
        [Newtonsoft.Json.JsonProperty("error_description", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Error_description { get; set; }
    
        [Newtonsoft.Json.JsonProperty("error_uri", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Error_uri { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    
        public static AuthError FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<AuthError>(data);
        }
    
    }
}