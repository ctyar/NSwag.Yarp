# NSwag Yarp

A package to simplify using NSwag behind YARP.

## Usage

1. Add a route for each of your APIs like this:

    ```json
   "route1": {
     "ClusterId": "cluster1",
     "Match": {
       "Path": "/api1/{**catch-all}"
     },
     "Transforms": [
       {
         "PathRemovePrefix": "/api1"
       }
     ]
   }
    ```
    With this configuration, calling `/api1` of your YARP will redirect to your RESTful API.

2. Add `AddYarp()` with the same prefix in your RESTful API:
    ```csharp
    app.UseOpenApi(config =>
    {
        config.AddYarp("/api1");
    });

    app.UseSwaggerUi(config =>
    {
        config.AddYarp("/api1");
    });
    ```

You can check the [samples](/src/samples) directory for a complete working example.

## Build
[Install](https://get.dot.net) the [required](global.json) .NET SDK.

Run:
```
$ dotnet build
```
