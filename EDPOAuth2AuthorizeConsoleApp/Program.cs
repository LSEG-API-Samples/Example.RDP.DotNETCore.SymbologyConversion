using System;
using System.Net.Http;
using System.Net.Sockets;
using Refinitiv.EDP.Example;
using Refinitiv.EDP.Example.AuthOauth2;

namespace EDPOAuth2AuthorizeConsoleApp
{
    class Program
    {
        // Set the EDP Username and Password before building the application
        const string edpUsername = "<Your EDP Username>";
        const string edpPassword = "<Your EDP Password>";

        static void Main(string[] args)
        {
           
            using (var client = new HttpClient())
            {
                var authClient = new AuthorizeClient(client);
                Console.WriteLine($"Requesting Authorize Token for ClientID {edpUsername} from EDP server\n");
                var authToken = GetToken(edpUsername, edpPassword, authClient);
                DumpToken(authToken);
                Console.WriteLine($"\n\nPress any key to refresh the Token: {authToken.Refresh_token}\n");
                Console.ReadKey();
                authToken=RefreshToken(edpUsername,authToken.Refresh_token,authClient);
                DumpToken(authToken);

            }
        }
        /// <summary>Used to get a new set of token for password grant.</summary>
        /// <param name="username">The resource owner username (typically ClientID/EDP Username).</param>
        /// <param name="password">The resource owner password.</param>
        /// <param name="client">The AuthorizeClient object. Internal codes will call TokenAsync from the AuthorizeClient class to request a new token</param>
        /// <returns><see cref="Tokenresponse"/></returns>
        /// <exception cref="EDPAuthorizeException">A server side error occurred. Internal code will catch the exception and print to console output</exception>
        /// <exception cref="Exception">A general error occurred.Internal code will catch the exception and print to console output</exception>
        public static Tokenresponse GetToken(string username, string password,AuthorizeClient client)
        {      
                try
                {
                    var tokenResult = client
                        .TokenAsync("password", username, password, "", "trapi", "", username, "", "true", "",
                            "")
                        .GetAwaiter().GetResult();
                    return tokenResult.Result;
                }
                catch (EDPAuthorizeException<AuthError> edpAuthorizeException)
                {
                    Console.WriteLine(
                        $"HttpStatusCode:{edpAuthorizeException.StatusCode} {edpAuthorizeException.Result.Error1} {edpAuthorizeException.Result.Error_description} {edpAuthorizeException.Result.Error_uri}");
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

            return null;
        }
        /// <summary>Used to refresh an access token</summary>
        /// <param name="username">The resource owner username (typically ClientID/EDP Username).</param>
        /// <param name="refreshToken">The refreshToken used to get a new Access Token from the EDP Server.</param>
        /// <param name="client">The AuthorizeClient object. Internal codes will call TokenAsync from the AuthorizeClient class to request a new token</param>
        /// <returns><see cref="Tokenresponse"/></returns>
        /// <exception cref="EDPAuthorizeException">A server side error occurred. Internal code will catch the exception and print to console output</exception>
        /// <exception cref="Exception">A general error occurred.Internal code will catch the exception and print to console output</exception>

        public static Tokenresponse RefreshToken(string username,string refreshToken,AuthorizeClient client)
        {
            Tokenresponse tokenResponse = null;
            try
            {
                tokenResponse = client.TokenAsync("refresh_token", username, "", "", "", refreshToken,
                    username, "",
                    "", "", "").GetAwaiter().GetResult().Result;
            }
            catch (EDPAuthorizeException<AuthError> edpAuthorizeException)
            {
                Console.WriteLine(
                    $"HttpStatusCode:{edpAuthorizeException.StatusCode} {edpAuthorizeException.Result.Error1} {edpAuthorizeException.Result.Error_description} {edpAuthorizeException.Result.Error_uri}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return tokenResponse;
        }
        /// <summary>Used to print Token values. It will check if the Tokenreponse object is null before printing the properties</summary>
        /// <param name="token">The Tokenresponse object.</param>
        public static void DumpToken(Tokenresponse token)
        {
            if (token == null)
            {
                Console.WriteLine("Token is null");
                return;
            }

            Console.WriteLine($"AccessToken={token.Access_token}\n" +
                              $"Expired={token.Expires_in}\n" +
                              $"RefreshToken={token.Refresh_token}\n" +
                              $"Scope={token.Scope}\n" +
                              $"TokenType={token.Token_type}");
        }
    }
}
