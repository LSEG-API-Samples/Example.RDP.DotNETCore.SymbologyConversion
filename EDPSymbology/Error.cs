namespace Refinitiv.EDP.Example.Symbology.Convert
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.13.18.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class Error 
    {
        /// <summary>Error object</summary>
        [Newtonsoft.Json.JsonProperty("error", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public ErrorDetail Error1 { get; set; } = new ErrorDetail();
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    
        public static Error FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Error>(data);
        }
    
    }
}