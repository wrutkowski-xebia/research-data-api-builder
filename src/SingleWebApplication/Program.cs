using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SingleWebApplication;
using SingleWebApplication.Code.Authentication;
using SingleWebApplication.Code.HttpClients;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMsalAuthentication<RemoteAuthenticationState, CustomUserAccount>(options =>
{    
    options.ProviderOptions.LoginMode = "redirect";
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.UserOptions.RoleClaim = AuthConst.RoleClaim;
}).AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, CustomUserAccount, CustomAccountFactory>();


builder.Services.AddTransient<UserDabRoleHeader>();
builder.Services.AddHttpClients(builder.Configuration);

await builder.Build().RunAsync();
