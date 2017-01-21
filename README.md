# Cake.Http
Cake.Http is set of aliases for [Cake](http://cakebuild.net/) that help simplify HTTP calls for GET, POST, PUT, DELETE, PATCH, etc.

Release notes can be found [here](ReleaseNotes.md).

## Build Status
Continuous Integration is provided by [AppVeyor](https://www.appveyor.com).  
The build can be found at [https://ci.appveyor.com/project/louisfischer/cake-http](https://ci.appveyor.com/project/louisfischer/cake-http).

![AppVeyor](https://ci.appveyor.com/api/projects/status/github/louisfischer/cake-http)


## Referencing
[![NuGet Version](http://img.shields.io/nuget/v/Cake.Http.svg?style=flat)](https://www.nuget.org/packages/Cake.Http/)  
You can reference Cake.Http in your build script as a cake addin:
```cake
#addin "Cake.Http"
```  
 
or nuget reference:  
```cake
#addin "nuget:https://www.nuget.org/api/v2?package=Cake.Http"
```

## Usage

```cake
#addin "Cake.Http"

Task("Http-GET")
    .Description("Basic http 'GET' request.") 
    .Does(() =>
    {
        string responseBody = HttpGet("https://www.google.com");
        Information(responseBody);    
    });

Task("Http-GET-With-Settings")
    .Description("Basic http 'GET' request with settings.") 
    .Does(() =>
    {
        var settings = new HttpSettings
        {
            Headers = new Dictionary<string, string
            {
                { "Authorization", "Bearer 1af538baa9045a84c0e889f672baf83ff24" },
                { "Cache-Control", "no-store" },
                { "Connection", "keep-alive" } 
            },
            UseDefaultCredentials = true,
            EnsureSuccessStatusCode = false
        };
        string responseBody = HttpGet("https://www.google.com", settings);
        Information(responseBody);    
    });

Task("Http-GET-With-Settings-Fluent")
    .Description("Basic http 'GET' request with fluent settings.") 
    .Does(() =>
    {
        string responseBody = HttpGet("https://www.google.com", settings => 
        {
            settings.UseBearerAuthorization("1af538baa9045a84c0e889f672baf83ff24")
                    .SetNoCeche()
                    .AppendHeader("Connection", "keep-alive");
        });
        Information(responseBody);    
    });

Task("Http-POST-With-Settings-Fluent")
    .Description("Basic http 'POST' request with fluent settings and setting request body.") 
    .Does(() =>
    {
        string responseBody = HttpPost("https://www.google.com", settings => 
        {
            settings.UseBearerAuthorization("1af538baa9045a84c0e889f672baf83ff24")
                    .SetContentType("appliication/json")
                    .SetRequestBody("{ \"id\": 123, \"name\": \"Test Test\" }");
        });
        Information(responseBody);    
    });

Task("Http-POST-With-Settings-Form-Url-Encoded")
    .Description("http 'POST' request with fluent settings using form-url-encoded body.") 
    .Does(() =>
    {
        string responseBody = HttpPost("https://www.google.com", settings => 
        {
            var formData = new Dictionary<string, string>
            {
                { "id", "12345" },
                { "name", "Test Test" },
                { "dateOfBirth", "11/22/1970" }
            };
            settings.SetFormUrlEncodedRequestBody(formData);
        });
        Information(responseBody);    
    });

RunTarget("Http-GET");
```

## Documention

Please visit the Cake Documentation site for a list of available aliases:  
[http://cakebuild.net/dsl/http-operations-extended](http://cakebuild.net/dsl/http-operations-extended)

## Tests

Cake.Http is covered by set of xUnit tests.

## Contribution GuideLines

[https://github.com/cake-build/cake/blob/develop/CONTRIBUTING.md](https://github.com/cake-build/cake/blob/develop/CONTRIBUTING.md)

## License

Copyright (c) 2017 Louis Fischer

Cake.Http is provided as-is under the MIT license. For more information see [LICENSE](https://github.com/louisfischer/Cake.Httpp/blob/master/LICENSE).