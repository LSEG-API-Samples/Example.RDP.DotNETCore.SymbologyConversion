# Using RDP Symbology Conversion Service with .NET Core

## Introduction

Symbology Conversion service is one of the REST API provided under the [Refinitiv Data Platform (RDP)](developers.refinitiv.com/elektron-data-platform/elektron-data-platform-apis). It leverages existing services that underly Refinitiv products, to create an aggregated list of functionality needed in the Symbology service. The Conversion method allows users to convert identifiers from one type to another. For example, the user can use the Conversion method to convert ISIN or CUSIP to Refinitiv RIC with additional information about the company.

There are two parts in this article the first part is detailed about the Symbology Conversion method including details about the request and response message for the Conversion service. The second part is the details about .NET Core Console example application we create to demonstrate the Symbology API usage.

## Prerequisites

* Understand concepts of RDP and basic usage of the Authorization services. Please find more details from [Developer Portal](developers.refinitiv.com/elektron-data-platform/elektron-data-platform-apis/learning?content=38562&type=learning_material_item).

Required software components:

* [.NET Core Framework](https://www.microsoft.com/net/download/dotnet-core/2.2)- version 2.2 or later version
* [Visual Studio 2017](https://visualstudio.microsoft.com/downloads/)- We use Visual Studio 2017 to develop the application on Windows.

Optional software components:

* [Visual Studio Code](https://code.visualstudio.com/) - IDE for .NET Core development on Windows and mac OS. 
* Other IDE's such as: [Rider](https://www.jetbrains.com/rider/) - IDE provided by Jetbrain for .NET Core projects on mac OS.

## Symbology Convert Method

The symbology convert method supports two ways of converting and retrieving symbology data:

1. With no specified list of return fields.
2. With a specified list of return fields.

### Parameters

The convert method takes two parameters:

* **Universe** (required): This takes a comma-separated list of input symbols or identifiers. Currently, maximum input symbols in the list are **100** items.

* **To** (optional): This takes a comma-separated list of return fields. If not specified the convert method returns all matching return fields. Please refer to  [APIDocs](https://apidocs.refinitiv.com/Apps/ApiDocs) page for available field list and JSON schema for the service.

### Model

Below is a model of a GET Request and Post Request message. Note that the endpoint we use in this article based on the beta version of the RDP.

#### GET REQUEST

```json
Base URL:"https://api.refinitiv.com/data/symbology/beta1"
Endpoint URL: "<Base URL>/convert"
https://api.refinitiv.com/data/symbology/beta1/convert?universe=<Identifier1>,<Identifier2>...<Identifier100>&To=<Field1>,<Field2>...
```

#### POST REQUEST

```json
Base URL:"https://api.refinitiv.com/data/symbology/beta1"
Endpoint URL: "<Base URL>/convert"
Media Type: "application/json"
Authorization Header Type:"Bearer"
                     Value:"<Access Token>"
Request Body:
{
    universe:[
        maxItems: 100
        minItems: 1
        uniqueItems: true
        string
    ]
    to:[
        uniqueItems: true
        string
        Enum:
        Array [ 51 ]
    ]
}
```

### Conversion with No Field List

When no field list is specified for the **To** parameter the convert method converts a symbol and returns all existing symbol types and reference data fields. Below is a sample of both a Get request and a Post request with no specified field list. Note that the Url endpoint provided based on a beta version of the service, it can be changed in the future after released.

#### GET REQUEST Sample

```
https://api.refinitiv.com/data/symbology/beta1/convert?universe=IBM.N,TRI.N
```
#### POST REQUEST Sample

```Json
Endpoint URL:"https://api.refinitiv.com/data/symbology/beta1/convert/convert"
Media Type: "application/json"
Authorization Header Type:"Bearer"+"<Access Token>"
Request Body:
{
  "universe": [
    "MSFT.O",
    "IBM.N"
  ]
}
```

### Conversion with Specified Field List

When a field list is specified for the To parameter the convert method converts a symbol and returns only the requested fields. Below is a sample of both a Get request and a Post request. When the To parameter has specified fields only matched fields will be returned.

#### GET REQUEST Sample

```URL
https://api.refinitiv.com/data/symbology/beta1/convert?universe=IBM.N@RIC&from=RIC&to=ISIN,CUSIP
```

#### POST REQUEST Sample

````Json
Endpoint URL: https://api.refinitiv.com/data/symbology/beta1/convert
JSON request body:
{
    "universe":["IBM.N","TRI.N"],
    "to":["ISIN","CUSIP","CommonName"]
}
````

### Symbology Response Instrument Field

Basically, the collection format of the Conversion response is Comma-separated values(CSV) according to Swagger JSON scheme for Symbology Convert from the [APIDocs](https://apidocs.refinitiv.com/Apps/ApiDocs) website.

The following JSON data is a sample of the response message from the Convert method. Please refer to a full JSON scheme for an additional field provided by the service.

#### Json Response Message Sample

```JSON
{
  "data": [
    [
      "IBM.N",
      "2001-06-08T00:00:00",
      "US4592001014",
      "459200101"
    ],
    [
      "TRI.N",
      "2018-11-27T00:00:00",
      "CA8849037095",
      "884903709"
    ]
  ],
  "headers": [
    {
      "description": "The requested Instrument as defined by the user.",
      "name": "instrument",
      "title": "Instrument",
      "type": "string"
    },
    {
      "description": "Date associated with the returned data.",
      "name": "date",
      "title": "Date",
      "type": "datetime"
    },
    {
      "description": "ISIN Consolidated with ISINCode",
      "name": "ISIN",
      "title": "ISIN",
      "type": "string"
    },
    {
      "description": " Committee on Uniform Securities Identification Procedures Identifier for US & Canadian companies.",
      "name": "CUSIP",
      "title": "CUSIP",
      "type": "string"
    }
  ],
  "links": {
    "count": 3
  },
  "universe": [
    {
      "Company Common Name": "International Business Machines Corp",
      "Instrument": "IBM.N",
      "Reporting Currency": "USD",
      "Organization PermID": "4295904307"
    },
    {
      "Company Common Name": "Thomson Reuters Corp",
      "Instrument": "TRI.N",
      "Reporting Currency": "USD",
      "Organization PermID": "4295861160"
    }
  ]
}
```

The response of the convert method containing a JSON object named **data** and **headers**. Actually, the data object is a collection or list of the array of nullable dynamic type. The array is actual data for each input instrument or symbol in the request message. And the header is a collection of header object describing the meaning of element inside the array.

For example, from the sample response message, the data[1] represents actual data for instrument "TRI.N" and the value of data[1][2] is a string "CA8849037095" which represents an ISIN according to the value of headers[2].title.

The order of array inside the data associated with the order of an element in headers object. Hence, the application has to iterate through the data collection to get a value of each symbol identifier. And then get the data definition for each element in the array by parsing the headers object to get the header description and its title.

The response always includes a collection of **universe** from the request message. It also has a field called **Instrument** which returns the input identifier requested in the **universe** parameter.

## Coding with .NET Core

This section describing the implementation of .NET Core example which is a console application we create to demonstrate the usage of Symbology convert method.

### Create Data Model

Basically, RDP provides Swagger JSON scheme on the [APIDocs](https://apidocs.refinitiv.com/Apps/ApiDocs) website. The developer can download the JSON scheme and use it as a reference to create a request and response message. The JSON scheme also describes the data model used by the services. Below screenshot is the Symbology service Swagger page.

![Symbology Convert JSON Scheme](/images/swaggerjsonscheme.png)

There are two steps the example used to request data from the Symbology Conversion service.

1. Get Access Token from EDS Authentication Service as described in the following [Page](https://developers.refinitiv.com/elektron-data-platform/elektron-data-platform-apis/learning?content=38562&type=learning_material_item).
2. Create JSON request body for Symbology convert method and then pass it to HttpClient class with the Access Token. And then send HTTP GET or Post to Symbology Conversion endpoint.

This example uses the JSON scheme from EDS Authentication service and Symbology Conversion service to generate data model in .NET Core application. The example use [NSwagStudio tool](https://github.com/RSuter/NSwag/wiki/NSwagStudio) to generate CSharp Client and data models from the JSON Scheme files.

Basically, the output from the NSwageStudio is a C# client class with a Data Models and its Enum values. The client classes basically provide an interface for application to request and manage the response. The client class uses a NewtonSoft JSON library to handle the JSON data. It can use the JSON library to serialize the object to JSON request and deserialize the response back to the Symbology object. The NSwagStudio automatically generate .NET async method for retrieving the data using Http Get or Http Post. Please refers to the instruction for using the NSWagStudio application from [NSWagStudio GitHub page](https://github.com/RSuter/NSwag/wiki/NSwagStudio). Note that after generating the client class from JSON scheme, you might need to refactor the codes and modify the class name and enum value yourself to match with your application design.


#### Interface and Class for EDS Authentication

This example uses SWagger JSON scheme to generate client class for EDS Authentication.
The following class and interface were generated by NSWagStudio and it has been modified to use with EDS Authentication service and Symbology Conversion service.

**IAuthorizeClient** provides TokenAsync method for retrieving Token from the server.

```CSharp
 public interface IAuthorizeClient
    {
        System.Threading.Tasks.Task<AuthorizeResponse> AuthorizeAsync(string client_id, string response_type, string scope, string redirect_uri, string state, string cookie);
        System.Threading.Tasks.Task<AuthorizeResponse> AuthorizeAsync(string client_id, string response_type, string scope, string redirect_uri, string state, string cookie, System.Threading.CancellationToken cancellationToken);
        System.Threading.Tasks.Task<AuthorizeResponse> RevokeAsync(string token, string token_type_hint, string client_id, string authorization);
        System.Threading.Tasks.Task<AuthorizeResponse> RevokeAsync(string token, string token_type_hint, string client_id, string authorization, System.Threading.CancellationToken cancellationToken);
        System.Threading.Tasks.Task<AuthorizeResponse<Tokenresponse>> TokenAsync(string grant_type, string username, string password, string deviceId, string scope, string refresh_token, string client_id, string authorization, string takeExclusiveSignOnControl, string multiFactorAuthenticationCode, string newPassword);
        System.Threading.Tasks.Task<AuthorizeResponse<Tokenresponse>> TokenAsync(string grant_type, string username, string password, string deviceId, string scope, string refresh_token, string client_id, string authorization, string takeExclusiveSignOnControl, string multiFactorAuthenticationCode, string newPassword, System.Threading.CancellationToken cancellationToken);
    }
```

**Tokenresponse** returns when the application calls TokenAsync to get Access Token. All class members represent the Token data.

```CSharp
 public class Tokenresponse 
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
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
        public static Tokenresponse FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this,Newtonsoft.Json.Formatting.Indented);
        }
    }
```

#### Interface and Class for Symbology Conversion

**IEDPSymbologyClient** is an interfaces provide async methods for using HttpGet(GetConvertAsync) and HttpPost(GetConvertAsync) with the Symbology Conversion service. There is the ConvertRequest class the application used to pass a request message to the convert methods. The application will convert the class to JSON request body by serializing the ConvertRequest object to JSON data.

```CSharp
 public interface IEDPSymbologyClient
 {
        System.Threading.Tasks.Task<Symbology> GetConvertAsync(string universe, System.Collections.Generic.IEnumerable<FieldEnum> to, Format? format);
        System.Threading.Tasks.Task<Symbology> GetConvertAsync(string universe,  System.Collections.Generic.IEnumerable<FieldEnum> to, Format? format, System.Threading.CancellationToken cancellationToken);

        System.Threading.Tasks.Task<Symbology> PostConvertAsync(ConvertRequest convertRequest, Format? format);
        System.Threading.Tasks.Task<Symbology> PostConvertAsync(ConvertRequest convertRequest, Format? format, System.Threading.CancellationToken cancellationToken);
 }

public class ConvertRequest
{
        [Newtonsoft.Json.JsonProperty("to", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, ItemConverterType = typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public System.Collections.Generic.ICollection<FieldEnum> To { get; set; }

        [Newtonsoft.Json.JsonProperty("universe", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.Collections.Generic.ICollection<string> Universe { get; set; } = new System.Collections.ObjectModel.Collection<string>();

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public static ConvertRequest FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ConvertRequest>(data);
        }
}
```

NSWagStudio also generates Symbology class which is a Data Model for Symbology response data. The GetConvertAsync and PostConvertAsync will return Symbology object to the application. It has to use the Newtonsoft JSON library to deserialize the JSON response message to Symbology object. 

The application has to parse the data object and the headers from the Symbology object. We need to change the Data Property to use dynamic type to support various type including a null value.

```csharp
 public class Symbology
    {
        [Newtonsoft.Json.JsonProperty("attributes", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<System.Collections.Generic.ICollection<object>> Attributes { get; set; }

        [Newtonsoft.Json.JsonProperty("data", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public System.Collections.Generic.ICollection<System.Collections.Generic.ICollection<dynamic>> Data { get; set; }

        [Newtonsoft.Json.JsonProperty("headers", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<HeaderData> Headers { get; set; }

        [Newtonsoft.Json.JsonProperty("links", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Links Links { get; set; }

        [Newtonsoft.Json.JsonProperty("messages", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Messages Messages { get; set; }

        [Newtonsoft.Json.JsonProperty("universe", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public System.Collections.Generic.ICollection<UniverseResponseData> Universe { get; set; }

        [Newtonsoft.Json.JsonProperty("variability", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public SymbologyVariability? Variability { get; set; }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this,Formatting.Indented);
        }
        public static Symbology FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Symbology>(data);
        }
    }
```

#### Input

The example is a console based application. There are two options for the user to specify the symbol/identifier list with custom fields for the request message.

* Specify an identifier list with required fields via command line argument.
* User can create JSON file containing a request message and then pass a file path to the command line argument. Then the application will create a request using parameters from the JSON file instead.

At the beginning of the application, the user has to enter the username and password. Then the application will pass it to Authentication service to get a new Access Token.

#### Output

For the output, the example will print the headers and its data to console output. The example also writes the data to a CSV file named "csvoutput.csv" by default.

#### Usage

The following list is the command line options generated by the application.
It uses C# [Commandline Parser](https://github.com/commandlineparser/commandline) to generate the help page and manage the command line arguments. There are sample usage and options that the user can specify when running and setting the command line argument. Note that if you have to connect to the RDP Server through your proxy server you might need to specify the proxy server using --proxy command line option.

```
$./EDPSymbologyConvert --help

EDPSymbologyConvert 1.0.0
Copyright (C) 2019 Refinitiv
USAGE:
Convert multiple RICs:
  EDPSymbologyConvert --universe IBM.N,MSFT.O,VOD.L
Convert multiples types of symbols:
  EDPSymbologyConvert --universe IBM.N,037833100,TH0001010014
Convert multiple RICs to ISIN and CommonName:
  EDPSymbologyConvert --to ISIN,CommonName --universe IBM.N,MSFT.O,VOD.L
Read convert parameter from JSON file:
  EDPSymbologyConvert --jsonfile ./request.json
Read convert parameter from JSON file and use symbols list from the file named ISINList.txt
instead:
  EDPSymbologyConvert --itemfile ./ISINList.txt --jsonfile ./request.json
Connecting through Proxy Server where "10.42.52.32:80" is proxy server IP address:
  EDPSymbologyConvert --proxy 10.42.52.32:80 --universe IBM.N

  --universe         Required. Symbol or item list in comma separated format. For example,
                     --universe IBM.N,037833100,TH0001010014 ,where 037833100 is CUSIP and
                     TH0001010014 is ISIN.

  --to               List of the field from Symbology Convert service. Set it to an empty string or
                     not set, the service will return all available fields for the universe.

  --jsonfile         Required. Allow the application to read a request parameter from the JSON file
                     instead. If specify this option, it overrides values from --universe and --to.

  -o, --csvoutput    (Default: ./csvoutput.csv) File name or absolute path to CSV file. Specify
                     this option to set the file path that application writes.CSV file.

  --itemfile         (Default: ) If specifying this option, application read universe list from the
                     file instead.The format is a multi - line symbol list.

  --proxy            (Default: ) Specify your proxy server.

  --verbose          (Default: false) Print additional logs to console output.

  --help             Display this help screen.

  --version          Display version information.


```

### Compile and Build the example application

You can download the application from [Github](https://github.com/TR-API-Samples/Example.EDP.DotNETCore.SymbologyConversion.git) and then compile and build it on Windows, Linux or macOS according to [.NET Core supported platform](https://dotnet.microsoft.com/download).
You have to install [.NET Core SDK 2.x](https://dotnet.microsoft.com/download) and then open solution file **edpapi.sln** with Visual Studio 2017.
Or you can compile and build the example using **dotnet buiild** or publish the project using **dotnet publish**  command.

You can follow the following step to publish the example project.

1.) Clone the example project from GitHub.

```
$ git clone https://github.com/TR-API-Samples/Example.EDP.DotNETCore.SymbologyConversion.git
```

2.) Go to folder EDPSymbologyConvertConsoleApp

```
$ cd EDPSymbologyConvertConsoleApp/
$ ls
EDPSymbologyConvertConsoleApp.csproj  Program.cs  ProgramConfig.cs  ProgramCore.cs  Properties  request.json
```

3.) Running **dotnet publish** command to publish the project to the native executable file and then you can run executable file directly without using __dotnet__ command on .Net Core supported OS. You can also share the app to another PC which running the same OS by copying all files under the publishing folder.

Please find more details about **dotnet publish** command from  [MSDN Document](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish?tabs=netcore21).

Below sample is a command to publish self-contained .NET core executable file on Linux Ubuntu. It will generate execute file including required DLLs in the publish folder. For another OS you can change Linux-x64 to a new one and you can find the list from [rid-catalog page](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog).

```
$ dotnet publish -c release -r linux-x64 -o publish/
```

You should see the console output like the following sample output and you can find execute file under publish directory.

```
$ dotnet publish -c release -r linux-x64 -o publish/
Microsoft (R) Build Engine version 15.9.20+g88f5fadfbe for .NET Core
Copyright (C) Microsoft Corporation. All rights reserved.

  Restore completed in 69.45 ms for /home/mcca/edpapi/EDPSymbology/EDPSymbology.csproj.
  Restore completed in 69.45 ms for /home/mcca/edpapi/EDPOauth2/EDPAuthOauth2.csproj.
  Restoring packages for /home/mcca/edpapi/EDPSymbologyConvertConsoleApp/EDPSymbologyConvertConsoleApp.csproj...
  Generating MSBuild file /home/mcca/edpapi/EDPSymbologyConvertConsoleApp/obj/EDPSymbologyConvertConsoleApp.csproj.nuget.g.props.
  Generating MSBuild file /home/mcca/edpapi/EDPSymbologyConvertConsoleApp/obj/EDPSymbologyConvertConsoleApp.csproj.nuget.g.targets.
  Restore completed in 295.87 ms for /home/mcca/edpapi/EDPSymbologyConvertConsoleApp/EDPSymbologyConvertConsoleApp.csproj.
  EDPAuthOauth2 -> /home/mcca/edpapi/EDPOauth2/bin/release/netcoreapp2.2/EDPAuthOauth2.dll
  EDPSymbology -> /home/mcca/edpapi/EDPSymbology/bin/release/netcoreapp2.2/EDPSymbology.dll
  EDPSymbologyConvertConsoleApp -> /home/mcca/edpapi/EDPSymbologyConvertConsoleApp/bin/release/netcoreapp2.2/linux-x64/EDPSymbologyConvert.dll
  EDPSymbologyConvertConsoleApp -> /home/mcca/edpapi/EDPSymbologyConvertConsoleApp/publish/

```
The publish command generate application named **EDPSymbologyConvert** under the folder "<Project Directory>/publish".

## Running the example

To test the example you must have a valid RDP username and password with permission to access Symbology Conversion service. 
The following command used to request the data for RIC **IBM.N**, CUSIP **037833100** and ISIN **TH0001010014**. A user may use -o to specify a new CSV file name to save the data.

A user may also use **--verbose** to print additional output such as the JSON request and response.

```
$ cd publish
$ ./EDPSymbologyConvert --universe IBM.N,037833100,TH0001010014 -o csvoutputIBM.csv --verbose

```

Below sample is output when you run the command line. It will return three sets of the array inside the data object. Each of element inside the array is field value for the fields the application request from the server. In this case, it returns all available fields because the user does not specify the fields list.

```
$./EDPSymbologyConvert --universe IBM.N,037833100,TH0001010014 -o csvoutputIBM.csv --verbose
Start RDP(Refinitiv Data Platform) Symbology Convert Example application
========= Application Configuration List =============
Universe:IBM.N,037833100,TH0001010014
ToEnum:
Use Json File: False
Export to CSV File:csvoutputIBM.csv True
======================================================

Signin to RDP(RDP Data Platform) Press Ctrl+C to cancel
=============================
Enter Username:GE-A-01103xxxxxxx
Enter Password:*****************
=============================
Logging in to the RDP server, please wait

Login Success!
*************Token Details**************
AccessToken=****************************************
Expired=300
RefreshToken=******
Scope=trapi.admin.accounts trapi.admin.accounts.groups trapi.admin.accounts.licenses trapi.admin.accounts.users trapi.alerts.news-headlines.crud trapi.alerts.news-stories.crud trapi.alerts.news.crud trapi.alerts.research.crud trapi.auth.cloud-credentials trapi.cfs.claimcheck.read trapi.cfs.claimcheck.write trapi.cfs.publisher.read trapi.cfs.publisher.setup.write trapi.cfs.publisher.stream.write trapi.cfs.publisher.write trapi.cfs.subscriber.read trapi.data.3pri.test.read trapi.data.aggregates.read trapi.data.api.test trapi.data.esg.bulk.read trapi.data.esg.metadata.read trapi.data.esg.read trapi.data.esg.universe.read trapi.data.esg.views-basic.read trapi.data.esg.views-measures-full.read trapi.data.esg.views-measures-standard.read trapi.data.esg.views-measures.read trapi.data.esg.views-scores-full.read trapi.data.esg.views-scores-standard.read trapi.data.esg.views-scores.read trapi.data.historical-pricing.events.read trapi.data.historical-pricing.read trapi.data.historical-pricing.summaries.read trapi.data.news-story-viewer.read trapi.data.news.read trapi.data.portfolios.read trapi.data.portfolios.write trapi.data.pricing.read trapi.data.quantitative-analytics.read trapi.data.research.read trapi.data.symbology.bulk.read trapi.data.symbology.read trapi.deployment.crud trapi.metadata.read trapi.streaming.pricing.read
TokenType=Bearer
****************************************
Retrieving data from RDP Symbology Conversion service...
======== JSon Request Body =======
{
  "to": [],
  "universe": [
    "IBM.N",
    "037833100",
    "TH0001010014"
  ]
}
==================================
============= Response Body in Json Format=================
{
  "data": [
    [
      "IBM.N",
      "4295904307",
      "International Business Machines Ord Shs",
      "USD",
      "18228",
      "COM",
      "Business Organization",
      "COR",
      "Company",
      "International Business Machines Corp",
      "4295904307",
      "4295904307",
      "1 New Orchard Rd",
      "",
      "",
      "",
      "",
      "ARMONK",
      "10504-1722",
      "NEW YORK",
      "US",
      "VGRQXHF3J8VDLUA7XE92",
      "54",
      "Professional, Scientific, and Technical Services",
      "5720101010",
      "IT Services & Consulting (NEC)",
      "Computer consultancy activities (NACE) (62.02)",
      "4741N",
      "30064843098",
      "IBM",
      "8590927768",
      "IBM",
      "I510600000",
      "459200",
      "100085757",
      "C000002474",
      "International Business Machines Ord Shs",
      "459200101",
      "US4592001014",
      null,
      null,
      "IBM.N",
      "IBM.N",
      null,
      "IBM",
      "XNYS",
      "IBM-N,USD,NORM",
      "US",
      "United States of America",
      "2005973"
    ],
    [
      "037833100",
      "4295905573",
      "Apple Ord Shs",
      "USD",
      "19775",
      "COM",
      "Business Organization",
      "COR",
      "Company",
      "Apple Inc",
      "4295905573",
      "4295905573",
      "1 Apple Park Way",
      "",
      "",
      "",
      "",
      "CUPERTINO",
      "95014-0642",
      "CALIFORNIA",
      "US",
      "HWUPKR0MPOU8FGXBT394",
      "31-33",
      "Manufacturing",
      "5710602011",
      "Phones & Smart Phones",
      "Manufacture of communication equipment (NACE) (26.30)",
      "05680",
      "30064826814",
      "AAPL",
      "8590932301",
      "AAPL.O",
      "A714301250",
      null,
      "100087573",
      "C000000378",
      "Apple Ord Shs",
      "037833100",
      "US0378331005",
      null,
      null,
      "AAPL.OQ",
      "AAPL.OQ",
      null,
      "AAPL",
      "XNGS",
      "AAPL-O,USD,NORM",
      "US",
      "United States of America",
      "2046251"
    ],
    [
      "TH0001010014",
      "4295893055",
      "Bangkok Bank Ord Shs F",
      "THB",
      "38999",
      "COM",
      "Business Organization",
      "BNK",
      "Bank",
      "Bangkok Bank PCL",
      "4295893055",
      "4295893055",
      "333 ถนนสีลม แขวงสีลม เขตบางรัก",
      "",
      "",
      "",
      "",
      "BANGKOK",
      "10500",
      "BANGKOK METROPOLIS",
      "TH",
      "549300CCL2BKJGMYXV60",
      "52",
      "Finance and Insurance",
      "5510101010",
      "Banks (NEC)",
      "Other monetary intermediation (NACE) (64.19)",
      "A4A9A",
      "30064789107",
      "@BKC/1",
      "8590070555",
      "BBLf.BK",
      "B108582000",
      "059895",
      "100010431",
      "C000087498",
      "Bangkok Bank Ord Shs F",
      null,
      "TH0001010014",
      null,
      null,
      "BBLf.BK",
      "BBLf.BK",
      null,
      "BBL-F",
      "XBKF",
      "BBL'F-TH,THB,NORM",
      "TH",
      "Thailand",
      "6077019"
    ]
  ],
  "headers": [
    {
      "description": "The requested Instrument as defined by the user.",
      "name": "instrument",
      "title": "Instrument",
      "type": "string"
    },
    {
      "description": "Organization level permanent identifier. (OA Permid, Organization Permid, etc)",
      "name": "TR.OrganizationID",
      "title": "Organization Perm ID",
      "type": "string"
    },
    {
      "description": "Instrument Name",
      "name": "TR.InstrumentName",
      "title": "Instrument Name",
      "type": "string"
    },
    {
      "description": "Currency Code of Report.",
      "name": "TR.CompanyReportCurrency",
      "title": "Report Currency",
      "type": "string"
    },
    {
      "description": "NDAOrgId",
      "name": "TR.NDAOrgId",
      "title": "NDAOrgId",
      "type": "string"
    },
    {
      "description": "Organization Type Code",
      "name": "TR.OrganizationTypeCode",
      "title": "Organization Type Code",
      "type": "string"
    },
    {
      "description": "Organization Type Description",
      "name": "TR.OrganizationType",
      "title": "Organization Type Description",
      "type": "string"
    },
    {
      "description": "Organization Sub-Type Code",
      "name": "TR.OrgSubtypeCode",
      "title": "Organization Sub-Type Code",
      "type": "string"
    },
    {
      "description": "Organization Sub-Type Description",
      "name": "TR.OrganizationSubtype",
      "title": "Organization Sub-Type Description",
      "type": "string"
    },
    {
      "description": "Where available provides the name of the organisation most commonly used.",
      "name": "TR.CommonName",
      "title": "Company Common Name",
      "type": "string"
    },
    {
      "description": "ImmediateParentId",
      "name": "TR.ImmediateParentId",
      "title": "Immediate Parent Id",
      "type": "string"
    },
    {
      "description": "The organization permid of the ultimate parent organization of this organization. The organization that resides at the top of a hierarchy tree, which has no Immediate Parent other than itself. Ultimate Parent is a derived relationship, based on the Immediate Parent populated.",
      "name": "TR.UltimateParentId",
      "title": "Ultimate Parent Id",
      "type": "string"
    },
    {
      "description": "HQAddressLine1",
      "name": "TR.HQAddressLine1",
      "title": "HQ Address Line 1",
      "type": "string"
    },
    {
      "description": "HQAddressLine2",
      "name": "TR.HQAddressLine2",
      "title": "HQ Address Line 2",
      "type": "string"
    },
    {
      "description": "HQAddressLine3",
      "name": "TR.HQAddressLine3",
      "title": "HQ Address Line 3",
      "type": "string"
    },
    {
      "description": "HQAddressLine4",
      "name": "TR.HQAddressLine4",
      "title": "HQ Address Line 4",
      "type": "string"
    },
    {
      "description": "HQAddressLine5",
      "name": "TR.HQAddressLine5",
      "title": "HQ Address Line 5",
      "type": "string"
    },
    {
      "description": "HQAddressCity",
      "name": "TR.HQAddressCity",
      "title": "HQ Address City",
      "type": "string"
    },
    {
      "description": "HQAddressPostalCode",
      "name": "TR.HQAddressPostalCode",
      "title": "HQ Address Postal Code",
      "type": "string"
    },
    {
      "description": "HQAddressStateProvince",
      "name": "TR.HQAddressStateProvince",
      "title": "HQ Address State Province",
      "type": "string"
    },
    {
      "description": "HQAddressCountryISO",
      "name": "TR.HQAddressCountryISO",
      "title": "HQ Address Country ISO",
      "type": "string"
    },
    {
      "description": "The Legal Entity Identifier (LEI) initiative is designed to create a global reference data system that uniquely identifies every legal entity or structure, in any jurisdiction, that is party to a financial transaction.",
      "name": "TR.LegalEntityIdentifier",
      "title": "Legal Entity ID (LEI)",
      "type": "string"
    },
    {
      "description": "Primary North American Industry Classification System (NAICS) Sector Code.  NAICS Classifies companies with increasing granularity by Economic Sector, Business Sector, Industry Group, Industry and Activity.",
      "name": "TR.NAICSSectorCode",
      "title": "NAICS Sector Code",
      "type": "string"
    },
    {
      "description": "Primary North American Industry Classification System (NAICS) Sector Description. NAICS Classifies companies with increasing granularity by Economic Sector, Business Sector, Industry Group, Industry and Activity.",
      "name": "TR.NAICSSector",
      "title": "NAICS Sector Name",
      "type": "string"
    },
    {
      "description": "Primary Thomson Reuters Business Classification (TRBC) Activity Code. TRBC Classifies companies with increasing granularity by Economic Sector, Business Sector, Industry Group, Industry and Activity.",
      "name": "TR.TRBCActivityCode",
      "title": "TRBC Activity Code",
      "type": "string"
    },
    {
      "description": "Primary Thomson Reuters Business Classification (TRBC) Activity Description. TRBC Classifies companies with increasing granularity by Economic Sector, Business Sector, Industry Group, Industry and Activity.",
      "name": "TR.TRBCActivity",
      "title": "TRBC Activity Name",
      "type": "string"
    },
    {
      "description": "NACE (for the French term 'Nomenclature statistique des Activités Economiques dans la Communauté Européenne'), is the industry standard classification system used in the European Union",
      "name": "TR.NACEClassification",
      "title": "NACE Classification",
      "type": "string"
    },
    {
      "description": "Reporting Entity (Alphanumeric)",
      "name": "TR.REPNo",
      "title": "Reporting Entity",
      "type": "string"
    },
    {
      "description": "EstimateId",
      "name": "TR.EstimateId",
      "title": "EstimateId",
      "type": "string"
    },
    {
      "description": "IBESTicker",
      "name": "TR.IBESTicker",
      "title": "IBESTicker",
      "type": "string"
    },
    {
      "description": "InstrumentID",
      "name": "TR.InstrumentID",
      "title": "Instrument ID",
      "type": "string"
    },
    {
      "description": "Reuters Identification Code for the primary issue of a company.",
      "name": "TR.PrimaryRIC",
      "title": "Primary Issue RIC",
      "type": "string"
    },
    {
      "description": "DisclosureId",
      "name": "TR.DisclosureId",
      "title": "DisclosureId",
      "type": "string"
    },
    {
      "description": "SDCCusip",
      "name": "TR.SDCCusip",
      "title": "SDC Cusip",
      "type": "string"
    },
    {
      "description": "MXID",
      "name": "TR.MXID",
      "title": "MXID",
      "type": "string"
    },
    {
      "description": "TMTCompanyID",
      "name": "TR.TMTCompanyID",
      "title": "TMT Company ID",
      "type": "string"
    },
    {
      "description": "InstrumentCommonName",
      "name": "TR.InstrumentCommonName",
      "title": "Instrument Common Name",
      "type": "string"
    },
    {
      "description": " Committee on Uniform Securities Identification Procedures Identifier for US & Canadian companies.",
      "name": "TR.CUSIP",
      "title": "CUSIP",
      "type": "string"
    },
    {
      "description": "ISIN Consolidated with ISINCode",
      "name": "TR.ISIN",
      "title": "ISIN",
      "type": "string"
    },
    {
      "decimalChar": ".",
      "description": "Depository Receipt Ratio",
      "name": "TR.DRRatio",
      "title": "DRRatio",
      "type": "number"
    },
    {
      "decimalChar": ".",
      "description": "Depository Receipt Quantity",
      "name": "TR.DRQuantity",
      "title": "DR Quantity",
      "type": "number"
    },
    {
      "description": "Primary Quote RIC",
      "name": "TR.PrimaryQuote",
      "title": "Primary Quote RIC",
      "type": "string"
    },
    {
      "description": "Reuters Instrument Code consolidated with RICCode",
      "name": "TR.RIC",
      "title": "RIC",
      "type": "string"
    },
    {
      "description": "Quote Retire Date",
      "name": "TR.RetireDate",
      "title": "RetireDate",
      "type": "date"
    },
    {
      "description": "Consolidated Ticker Symbol",
      "name": "TR.TickerSymbol",
      "title": "Ticker Symbol",
      "type": "string"
    },
    {
      "description": "Exchange Market Identifier Code (MIC)",
      "name": "TR.ExchangeMIC",
      "title": "Exchange Market Identifier Code",
      "type": "string"
    },
    {
      "description": "IlxId",
      "name": "TR.IlxId",
      "title": "IlxId",
      "type": "string"
    },
    {
      "description": "ISO2 country code where the instrument trades.",
      "name": "TR.ExchangeCountryCode",
      "title": "Exchange Country Code",
      "type": "string"
    },
    {
      "description": "Country where the instrument trades",
      "name": "TR.ExchangeCountry",
      "title": "Country of Exchange",
      "type": "string"
    },
    {
      "description": "SEDOL consolidated with SEDOLCode",
      "name": "TR.SEDOL",
      "title": "SEDOL",
      "type": "string"
    }
  ],
  "links": {
    "count": 3
  },
  "universe": [
    {
      "Company Common Name": "International Business Machines Corp",
      "Instrument": "IBM.N",
      "Reporting Currency": "USD",
      "Organization PermID": "4295904307"
    },
    {
      "Company Common Name": "Apple Inc",
      "Instrument": "037833100",
      "Reporting Currency": "USD",
      "Organization PermID": "4295905573"
    },
    {
      "Company Common Name": "Bangkok Bank PCL",
      "Instrument": "TH0001010014",
      "Reporting Currency": "THB",
      "Organization PermID": "4295893055"
    }
  ],
  "variability": "variable"
}
===========================================================

=======================================
Common Name:International Business Machines Corp
Instrument:IBM.N
Organization Perm ID:4295904307
Reporting Currency:USD
Common Name:Apple Inc
Instrument:037833100
Organization Perm ID:4295905573
Reporting Currency:USD
Common Name:Bangkok Bank PCL
Instrument:TH0001010014
Organization Perm ID:4295893055
Reporting Currency:THB

Row Count: 3
| Instrument(instrument) | Organization Perm ID(TR.OrganizationID) | Instrument Name(TR.InstrumentName) | Report Currency(TR.CompanyReportCurrency) | NDAOrgId(TR.NDAOrgId) | Organization Type Code(TR.OrganizationTypeCode) | Organization Type Description(TR.OrganizationType) | Organization Sub-Type Code(TR.OrgSubtypeCode) | Organization Sub-Type Description(TR.OrganizationSubtype) | Company Common Name(TR.CommonName) | Immediate Parent Id(TR.ImmediateParentId) | Ultimate Parent Id(TR.UltimateParentId) | HQ Address Line 1(TR.HQAddressLine1) | HQ Address Line 2(TR.HQAddressLine2) | HQ Address Line 3(TR.HQAddressLine3) | HQ Address Line 4(TR.HQAddressLine4) | HQ Address Line 5(TR.HQAddressLine5) | HQ Address City(TR.HQAddressCity) | HQ Address Postal Code(TR.HQAddressPostalCode) | HQ Address State Province(TR.HQAddressStateProvince) | HQ Address Country ISO(TR.HQAddressCountryISO) | Legal Entity ID (LEI)(TR.LegalEntityIdentifier) | NAICS Sector Code(TR.NAICSSectorCode) | NAICS Sector Name(TR.NAICSSector) | TRBC Activity Code(TR.TRBCActivityCode) | TRBC Activity Name(TR.TRBCActivity) | NACE Classification(TR.NACEClassification) | Reporting Entity(TR.REPNo) | EstimateId(TR.EstimateId) | IBESTicker(TR.IBESTicker) | Instrument ID(TR.InstrumentID) | Primary Issue RIC(TR.PrimaryRIC) | DisclosureId(TR.DisclosureId) | SDC Cusip(TR.SDCCusip) | MXID(TR.MXID) | TMT Company ID(TR.TMTCompanyID) | Instrument Common Name(TR.InstrumentCommonName) | CUSIP(TR.CUSIP) | ISIN(TR.ISIN) | DRRatio(TR.DRRatio) | DR Quantity(TR.DRQuantity) | Primary Quote RIC(TR.PrimaryQuote) | RIC(TR.RIC) | RetireDate(TR.RetireDate) | Ticker Symbol(TR.TickerSymbol) | Exchange Market Identifier Code(TR.ExchangeMIC) | IlxId(TR.IlxId) | Exchange Country Code(TR.ExchangeCountryCode) | Country of Exchange(TR.ExchangeCountry) | SEDOL(TR.SEDOL)

|IBM.N|4295904307|International Business Machines Ord Shs|USD|18228|COM|Business Organization|COR|Company|International Business Machines Corp|4295904307|4295904307|1 New Orchard Rd|||||ARMONK|10504-1722|NEW YORK|US|VGRQXHF3J8VDLUA7XE92|54|Professional, Scientific, and Technical Services|5720101010|IT Services & Consulting (NEC)|Computer consultancy activities (NACE) (62.02)|4741N|30064843098|IBM|8590927768|IBM|I510600000|459200|100085757|C000002474|International Business Machines Ord Shs|459200101|US4592001014|Null |Null |IBM.N|IBM.N|Null |IBM|XNYS|IBM-N,USD,NORM|US|United States of America|2005973

|037833100|4295905573|Apple Ord Shs|USD|19775|COM|Business Organization|COR|Company|Apple Inc|4295905573|4295905573|1 Apple Park Way|||||CUPERTINO|95014-0642|CALIFORNIA|US|HWUPKR0MPOU8FGXBT394|31-33|Manufacturing|5710602011|Phones & Smart Phones|Manufacture of communication equipment (NACE) (26.30)|05680|30064826814|AAPL|8590932301|AAPL.O|A714301250|Null |100087573|C000000378|Apple Ord Shs|037833100|US0378331005|Null |Null |AAPL.OQ|AAPL.OQ|Null |AAPL|XNGS|AAPL-O,USD,NORM|US|United States of America|2046251

|TH0001010014|4295893055|Bangkok Bank Ord Shs F|THB|38999|COM|Business Organization|BNK|Bank|Bangkok Bank PCL|4295893055|4295893055|333 ถนนสีลม แขวงสีลม เขตบางรัก|||||BANGKOK|10500|BANGKOK METROPOLIS|TH|549300CCL2BKJGMYXV60|52|Finance and Insurance|5510101010|Banks (NEC)|Other monetary intermediation (NACE) (64.19)|A4A9A|30064789107|@BKC/1|8590070555|BBLf.BK|B108582000|059895|100010431|C000087498|Bangkok Bank Ord Shs F|Null |TH0001010014|Null |Null |BBLf.BK|BBLf.BK|Null |BBL-F|XBKF|BBL'F-TH,THB,NORM|TH|Thailand|6077019
=======================================

Export the data to CSV file csvoutputIBM.csv
Writing data to csvoutputIBM.csv complete

```

## Summary

This article explains the usage of Symbology Conversion service. It also provides an example application to demonstrate the usage of Authentication and Symbology convert method. The example can convert a symbol or list of symbols to a specified output type. RDP API user can use the example application to test the service and apply the codes from the example with another service on the RDP.

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Authors

* **Moragodkrit Chumsri** - Release 1.0.  *Initial work*

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

## Reference
* [Refinitiv Data Platform (RDP) Learning](developers.refinitiv.com/elektron-data-platform/elektron-data-platform-apis)
* [APIDocs Swagger](https://apidocs.refinitiv.com/Apps/ApiDocs)
* [NSwagStudio](https://github.com/RSuter/NSwag/wiki/NSwagStudio)
* [Using NSwagStudio with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag?view=aspnetcore-2.2&tabs=visual-studio%2Cvisual-studio-xml)
* [Dotnet Core Publish Command](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish?tabs=netcore21)
* [.NET Commandlineparser](https://github.com/commandlineparser/commandline)
