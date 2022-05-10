# Rn.NetCore.BasicHttp
Common Http abstraction for your applications

Source code for the project can be found [here](https://github.com/rniemand/Rn.NetCore.BasicHttp).

## Usage
Using the package is as simple as registering the required types with your `DI` container of choice.

```csharp
services
	.AddSingleton<IConfiguration>(config)
	.AddSingleton<IHttpClientFactory, HttpClientFactory>()
```

Injecting an instance of the `IHttpClientFactory` where required, and calling the `.GetHttpClient()` method to get a new instance of the `HttpClientWrapper` class (backed by `IHttpClient`).

When it comes to testing, all you need to do is mock the `IHttpClientFactory`.

<!--(Rn.BuildScriptHelper){
	"version": "1.0.106",
	"replace": false
}(END)-->