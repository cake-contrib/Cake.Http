# Cake.Http
Cake.Http is set of aliases for [Cake](http://cakebuild.net/) that help simplify HTTP calls for GET, POST, PUT, DELETE, etc.

Release notes can be found [here](ReleaseNotes.md).

## Build Status
Continuous Integration is provided by [AppVeyor](https://www.appveyor.com).  
The build can be found at [https://ci.appveyor.com/project/louisfischer/cake-http[](https://ci.appveyor.com/project/louisfischer/cake-http).

![AppVeyor](https://ci.appveyor.com/api/projects/status/github/louisfischer/Cake.Http)

## Referencing

You can reference Cake.Http in your build script as a cake addin:
```cake
#addin "Cake.Http"
```  

or nuget reference:

```cake
#addin "nuget:https://www.nuget.org/api/v2?package=Cake.Http"
```

## Usage

```csharp
#addin "Cake.Http"

var target = Argument("target", "Default"); 

RunTarget(target);
```

## Documention

Please visit the Cake Documentation site for a list of available aliases:  
[http://cakebuild.net/dsl/http-extended](http://cakebuild.net/dsl/http-extended)

## Tests

Cake.Http is covered by set of xUnit tests.

## Contribution GuideLines

[https://github.com/cake-build/cake/blob/develop/CONTRIBUTING.md](https://github.com/cake-build/cake/blob/develop/CONTRIBUTING.md)

## License

Copyright (c) 2017 Louis Fischer

Cake.Http is provided as-is under the MIT license. For more information see [LICENSE](https://github.com/louisfischer/Cake.Httpp/blob/master/LICENSE).