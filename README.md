# Blazor Avatar

This Blazor library provides components that render avatar.
Currently this library only supports initial avatar.

This library has been tested only with Blazor WebAssembly.

# Install
Install package from Nuget.
```
dotnet add package Blazor.Avatar --version 0.0.0
```

Then register `Blazor.Avatar` dependencies by calling `AddAvatarComponents()` in `Program.cs`.
```cs
using Blazor.Avatar;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
  .AddAvatarComponents()
  .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
```

# Usage
Use the `InitialAvatar` component in your Razor file.
```razor
@page "/"
@using Blazor.Avatar.Components.InitialAvatarComponent

<p>Render initial avatar with random color.</p>
<InitialAvatar FirstName="Aaron"
               LastName="Doe" />

<p>Render initial avatar with specify color and size.</p>
<InitialAvatar FirstName="Jane"
               LastName="Smith"
               FillColor="green"
               Size=100 />

<p>
  Render initial avatar with specify style. Note that
  specifying <b>Style="border-radius: 50%; width: 80px; height: 60px"</b>
  will override the <b>Size</b> parameter.
</p>
<InitialAvatar FirstName="gavin"
               LastName="baker"
               Size=150
               Style="border-radius: 50%" />

```

The above Razor code will be rendered like this:
![img](images/example-usage-render.PNG)
