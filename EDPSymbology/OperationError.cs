namespace Refinitiv.EDP.Example.Symbology.Convert
{
    /// an object containing references to the source of the error.
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.13.18.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class OperationError 
    {
        /// <summary>A flag indicate whether the name is invalid name or not.</summary>
        [Newtonsoft.Json.JsonProperty("invalidName", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string InvalidName { get; set; }
    
        /// <summary>Invalid values causing the error: it is an array</summary>
        [Newtonsoft.Json.JsonProperty("invalidValues", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<string> InvalidValues { get; set; }
    
        /// <summary>The key in the request/path that causing the error</summary>
        [Newtonsoft.Json.JsonProperty("key", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Key { get; set; }
    
        /// <summary>The field name causing the error</summary>
        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Name { get; set; }
    
        /// <summary>The value causing the error.</summary>
        [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Value { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    
        public static OperationError FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<OperationError>(data);
        }
    
    }
}