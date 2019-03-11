namespace Refinitiv.EDP.Example.AuthOauth2
{
    [System.CodeDom.Compiler.GeneratedCode("NSwag", "12.0.14.0 (NJsonSchema v9.13.18.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class AuthorizeResponse<TResult> : AuthorizeResponse
    {
        public TResult Result { get; private set; }
        
        public AuthorizeResponse(int statusCode, System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result) 
            : base(statusCode, headers)
        {
            Result = result;
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "12.0.14.0 (NJsonSchema v9.13.18.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class AuthorizeResponse
    {
        public int StatusCode { get; private set; }

        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }
        
        public AuthorizeResponse(int statusCode, System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> headers) 
        {
            StatusCode = statusCode; 
            Headers = headers;
        }
    }
}