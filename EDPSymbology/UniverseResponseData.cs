namespace Refinitiv.EDP.Example.Symbology.Convert
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.13.18.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class UniverseResponseData 
    {
        [Newtonsoft.Json.JsonProperty("Company Common Name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public string Common_Name { get; set; }
    
        [Newtonsoft.Json.JsonProperty("Instrument", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public string Instrument { get; set; }
    
        [Newtonsoft.Json.JsonProperty("Reporting Currency", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public string ReportingCurrentcy { get; set; }
    
        [Newtonsoft.Json.JsonProperty("Organization PermID", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public string OrganizationPermID { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    
        public static UniverseResponseData FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<UniverseResponseData>(data);
        }
    
    }
}