using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Refinitiv.EDP.Example.AuthOauth2;
using Refinitiv.EDP.Example.Symbology.Convert;

namespace EDPSymbologyConvertConsoleApp
{
    internal partial class Program
    {
        /// <summary>
        ///     Application running with the following steps
        ///     1. Display arguments list and validate command line argument values
        ///     2. Get Refresh Token from EDP Server
        ///     3. Send Http Request GET/POST to EDP Symbology Convert endpoint to get the data
        ///     4. Validate response and print to console and write to csv if needed.
        /// </summary>
        private static void Main(string[] args)
        {
            // 1. Display arguments list and validate command line argument values
            var appConfig = new Config();
            if (!ShowsConfigCommand(args, ref appConfig))
                return;
            Console.WriteLine("Start EDP(Elektron Data Platform) Symbology Convert Example application");
            var convertRequest = ValidateAndCreateConvertRequest(appConfig);

            if (appConfig.Verbose) DumpAppConfig(appConfig, convertRequest);

            if (convertRequest.Universe.Count == 0)
            {
                Console.WriteLine("Please specify at lease one item in the universe");
                return;
            }


            // 2. Get Refresh Token from EDP Server
            //    If user provide RefreshToken or Access Token, app will skip this step</summary>
            var authToken = DoLoginAndAuthenticate(appConfig);
            if (authToken == null) return;

            // 3. Send Http Request GET/POST to EDP Symbology Convert endpoint to get the data
            // 4. Validate response and print to console and write to csv if needed.
            //    SymbologyConvertGet using Http Get to send the request and
            //    SymbologyConvertPost using Http Post to send the request. 
            var requestIsSuccess = false;
            Symbology symbologyData = null;
            var IsCancelledReqeust = false;
            do
            {
                
                using (var client = new HttpClient(GenerateHttpClientHandler(appConfig)))
                {
                    // Set access token in Http Authorization Header
                    var accessToken = string.IsNullOrEmpty(appConfig.AccessToken)
                        ? authToken.Access_token
                        : appConfig.AccessToken;

                    client.DefaultRequestHeaders.Authorization = authToken is null
                        ? new AuthenticationHeaderValue("Bearer", accessToken)
                        : new AuthenticationHeaderValue(authToken.Token_type, accessToken);

                    //Specify Media Type to application/json
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var symbologyClient = new EDPSymbologyClient(client);

                    //If user specify EDP Symbology based url vi app config, it overrides default symbology based url.
                    if (!string.IsNullOrEmpty(appConfig.SymbologyBaseURL))
                        symbologyClient.BaseUrl = appConfig.SymbologyBaseURL;

                    Console.WriteLine("Retrieving data from EDP Symbology Conversion service...");
                    var cts = new CancellationTokenSource();
                    Console.TreatControlCAsInput = false;
                    Console.CancelKeyPress += (s, ev) =>
                    {
                        IsCancelledReqeust = true;
                        cts.Cancel();
                    };
                    try
                    {
                        symbologyData = !appConfig.UseJsonRequestFile
                            ? SymbologyConvertGet(symbologyClient, convertRequest, appConfig, cts)
                            : SymbologyConvertPost(symbologyClient, convertRequest, appConfig, cts);
                    }
                    catch (EDPSymbologyException<Error> exception)
                    {
                        Console.WriteLine(
                            $"Error ID:{exception.Result.Error1.Id} Code:{exception.Result.Error1.Code} {exception.Result.Error1.Status} {exception.Result.Error1.Message}");
                        if (exception.Result.Error1.Code != "401")
                        {
                            // Assume that there is authorize error occurs.
                            requestIsSuccess = true;
                        }
                        else
                        {
                            requestIsSuccess = false;
                            Console.WriteLine(
                                "The AccessToken you use may not valid, Re-Enter EDP Username and Password");
                            //reset AccessToken
                            appConfig.AccessToken = string.Empty;

                            // Re Enter Username and Password.
                            authToken = DoLoginAndAuthenticate(appConfig);
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"{exception.Message}");
                    }

                    requestIsSuccess = symbologyData != null;
                }
            } while (!IsCancelledReqeust && !requestIsSuccess);

            if (!IsCancelledReqeust)
                PrintSymbologyResponse(symbologyData, appConfig);
            else
            {
                Console.WriteLine("Operation was cancelled. Exit the application");
            }
        }

        public static Tokenresponse DoLoginAndAuthenticate(Config appConfig)
        {
            Tokenresponse authToken = null;
            //If AccessToken appears in appConfig, skip this steps and pass AccessToken the service directly.
            if (!string.IsNullOrEmpty(appConfig.AccessToken)) return authToken;

            if (DoLoginAndGetToken(out authToken, appConfig))
            {
                Console.WriteLine("Login Success!");
                if (appConfig.Verbose)
                    DumpToken(authToken);
            }
            else
            {
                Console.WriteLine("Login Terminated. Exit application.");
            }

            return authToken;
        }
    }
}