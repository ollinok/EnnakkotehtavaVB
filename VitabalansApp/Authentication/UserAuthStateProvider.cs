using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace VitabalansApp.Authentication;

public class UserAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;

    public UserAuthStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string token = "";
        try
        {
            token = await _localStorage.GetItemAsStringAsync("jwt");
        }
        catch
        {
            // On App first load the local storage cannot be loaded
            /*
             * https://docs.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/call-javascript-from-dotnet?view=aspnetcore-6.0
             * For Blazor Server apps with prerendering enabled, calling into JS isn't possible during initial prerendering.
             * JS interop calls must be deferred until after the connection with the browser is established.
             * 
             */
        }


        var tokenhandler = new JwtSecurityTokenHandler();
        var validationParams = GetValidationParameters();

        SecurityToken validatedToken;
        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                tokenhandler.ValidateToken(token.Replace("\"", ""), validationParams, out validatedToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await _localStorage.RemoveItemAsync("jwt");
                token = "";
            }
        }

        var identity = new ClaimsIdentity();
        if (!string.IsNullOrEmpty(token))
        {
            identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        }
        
        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }

    // https://github.com/patrickgod/BlazorAuthenticationTutorial/blob/master/BlazorAuthenticationTutorial/Client/CustomAuthStateProvider.cs
    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
    // END

    public static TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters()
        {
            ValidateLifetime = true,
            ValidIssuer = "ennakkotehtava-olli-nokkonen",
            ValidAudience = "ennakkotehtava-olli-nokkonen",
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("supersalainenavain"))
        };
    }
}
