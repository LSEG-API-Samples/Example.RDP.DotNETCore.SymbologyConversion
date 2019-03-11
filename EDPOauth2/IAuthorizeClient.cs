namespace Refinitiv.EDP.Example.AuthOauth2
{
    [System.CodeDom.Compiler.GeneratedCode("NSwag", "12.0.14.0 (NJsonSchema v9.13.18.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial interface IAuthorizeClient
    {
        /// <summary>Used to get a token for implicit grant.</summary>
        /// <param name="client_id">Your application's client id</param>
        /// <param name="response_type">Indicates the type of credentials expected in the response. Possible values include "token".</param>
        /// <param name="scope">The scope required by the application.</param>
        /// <param name="redirect_uri">A URI where the provider will redirect the ressource owner once he is properly identified.</param>
        /// <param name="state">Opaque value returned when the ressource owner is redirected back to the application.</param>
        /// <param name="cookie">A valid IPDP. This a temporary way to auth while the auth provider page does not exist.</param>
        /// <exception cref="EDPAuthorizeException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<AuthorizeResponse> AuthorizeAsync(string client_id, string response_type, string scope, string redirect_uri, string state, string cookie);
    
        /// <summary>Used to get a token for implicit grant.</summary>
        /// <param name="client_id">Your application's client id</param>
        /// <param name="response_type">Indicates the type of credentials expected in the response. Possible values include "token".</param>
        /// <param name="scope">The scope required by the application.</param>
        /// <param name="redirect_uri">A URI where the provider will redirect the ressource owner once he is properly identified.</param>
        /// <param name="state">Opaque value returned when the ressource owner is redirected back to the application.</param>
        /// <param name="cookie">A valid IPDP. This a temporary way to auth while the auth provider page does not exist.</param>
        /// <exception cref="EDPAuthorizeException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<AuthorizeResponse> AuthorizeAsync(string client_id, string response_type, string scope, string redirect_uri, string state, string cookie, System.Threading.CancellationToken cancellationToken);
    
        /// <summary>Used to revoke both types of token (access_token or revoke_token).</summary>
        /// <param name="token">The token that the client wants to get revoked.</param>
        /// <param name="token_type_hint">OPTIONAL.  A hint about the type of the token submitted for revocation.  Clients MAY pass this parameter in order to help the authorization server to optimize the token lookup.  If the server is unable to locate the token using the given hint, it MUST extend its search across all of its supported token types. The value can be access_token or refresh_token</param>
        /// <param name="client_id">OPTIONAL.  The client_id  = Application ID. Must be set here OR in the HTTP "Authorization" header as "basic" (see basicAuth definition here)</param>
        /// <param name="authorization">Basic auth with "client_id:client_secret" in base64.</param>
        /// <returns>[rfc7009#section-2.2](https://tools.ietf.org/html/rfc7009#section-2.2)' The authorization server responds with HTTP status code 200 if the token has been revoked successfully or if the client submitted an invalid token.</returns>
        /// <exception cref="EDPAuthorizeException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<AuthorizeResponse> RevokeAsync(string token, string token_type_hint, string client_id, string authorization);
    
        /// <summary>Used to revoke both types of token (access_token or revoke_token).</summary>
        /// <param name="token">The token that the client wants to get revoked.</param>
        /// <param name="token_type_hint">OPTIONAL.  A hint about the type of the token submitted for revocation.  Clients MAY pass this parameter in order to help the authorization server to optimize the token lookup.  If the server is unable to locate the token using the given hint, it MUST extend its search across all of its supported token types. The value can be access_token or refresh_token</param>
        /// <param name="client_id">OPTIONAL.  The client_id  = Application ID. Must be set here OR in the HTTP "Authorization" header as "basic" (see basicAuth definition here)</param>
        /// <param name="authorization">Basic auth with "client_id:client_secret" in base64.</param>
        /// <returns>[rfc7009#section-2.2](https://tools.ietf.org/html/rfc7009#section-2.2)' The authorization server responds with HTTP status code 200 if the token has been revoked successfully or if the client submitted an invalid token.</returns>
        /// <exception cref="EDPAuthorizeException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<AuthorizeResponse> RevokeAsync(string token, string token_type_hint, string client_id, string authorization, System.Threading.CancellationToken cancellationToken);
    
        /// <summary>Used to get a token for password grant.</summary>
        /// <param name="grant_type">Supported values "password" and "refresh_token"</param>
        /// <param name="username">The resource owner username (typically email).</param>
        /// <param name="password">The resource owner password.</param>
        /// <param name="scope">The scope of the access request.</param>
        /// <param name="refresh_token">The refresh token issued to the client.</param>
        /// <param name="client_id">The client_id  = Application ID. Alternatively, can be provided in Authorization header.</param>
        /// <param name="authorization">"Basic" + base64 encoded "client_id:client_secret", where client_id=Application ID and client_secret is empty</param>
        /// <param name="takeExclusiveSignOnControl">OPTIONAL. This is a Boolean that will allow the API Caller to create session if the nb of concurrent session have been reached (of course, by doing this a valid session will be killed) - default = false</param>
        /// <param name="multiFactorAuthenticationCode">OPTIONAL. This a string that will have to be send only if MFA is required to authenticate the identity. This code will be send by SMS or Email (depending on how MFA has been setup). - default = null</param>
        /// <param name="newPassword">OPTIONAL. This is a string that will have to be send if a new Password
        ///  is required to authenticate. (Note: the current and new passwords
        ///  will be required in order to authenticate) - default = null</param>
        /// <returns>OK</returns>
        /// <exception cref="EDPAuthorizeException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<AuthorizeResponse<Tokenresponse>> TokenAsync(string grant_type, string username, string password, string deviceId, string scope, string refresh_token, string client_id, string authorization, string takeExclusiveSignOnControl, string multiFactorAuthenticationCode, string newPassword);
    
        /// <summary>Used to get a token for password grant.</summary>
        /// <param name="grant_type">Supported values "password" and "refresh_token"</param>
        /// <param name="username">The resource owner username (typically email).</param>
        /// <param name="password">The resource owner password.</param>
        /// <param name="scope">The scope of the access request.</param>
        /// <param name="refresh_token">The refresh token issued to the client.</param>
        /// <param name="client_id">The client_id  = Application ID. Alternatively, can be provided in Authorization header.</param>
        /// <param name="authorization">"Basic" + base64 encoded "client_id:client_secret", where client_id=Application ID and client_secret is empty</param>
        /// <param name="takeExclusiveSignOnControl">OPTIONAL. This is a Boolean that will allow the API Caller to create session if the nb of concurrent session have been reached (of course, by doing this a valid session will be killed) - default = false</param>
        /// <param name="multiFactorAuthenticationCode">OPTIONAL. This a string that will have to be send only if MFA is required to authenticate the identity. This code will be send by SMS or Email (depending on how MFA has been setup). - default = null</param>
        /// <param name="newPassword">OPTIONAL. This is a string that will have to be send if a new Password
        ///  is required to authenticate. (Note: the current and new passwords
        ///  will be required in order to authenticate) - default = null</param>
        /// <returns>OK</returns>
        /// <exception cref="EDPAuthorizeException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<AuthorizeResponse<Tokenresponse>> TokenAsync(string grant_type, string username, string password, string deviceId, string scope, string refresh_token, string client_id, string authorization, string takeExclusiveSignOnControl, string multiFactorAuthenticationCode, string newPassword, System.Threading.CancellationToken cancellationToken);
    
    }
}