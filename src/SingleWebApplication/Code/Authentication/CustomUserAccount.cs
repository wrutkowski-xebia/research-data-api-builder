using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace SingleWebApplication.Code.Authentication
{   
    public class CustomUserAccount : RemoteUserAccount
    {
        [JsonPropertyName(AuthConst.Roles)]
        public string[] Roles { get; set; } = [];
    }
}
