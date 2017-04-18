# Build Script

You can reference Cake.Http in your build script as a cake addin:

```cake
#addin "Cake.Http"
```

or nuget reference:

```cake
#addin "nuget:https://www.nuget.org/api/v2?package=Cake.Http"
```

Then some examples:

```cake
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
            Headers = new Dictionary<string, string>
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
                    .SetContentType("application/json")
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