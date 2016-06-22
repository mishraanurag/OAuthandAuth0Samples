using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace TestMVCAPI5
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {            
            //System.Web.HttpContext.Current.User.Identity.Name            
            //System.Security.Principal.WindowsIdentity.GetCurrent().Name  

            await Task.Run(() =>
           {               
               if (CheckValidation(context.Parameters["username"], context.Parameters["password"]))
               {
                   context.Validated();
               }
               else
               {
                   context.SetError("invalid_clientId", "Invalid Credentials. Please verify UserId & Password");
                   return;
               }     
           });

       }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            await Task.Run(() =>
             {
                 context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });                

                 List<string> CustomRoles = UserRoles(context.UserName.ToString().Trim(), context.Password.ToString().Trim());

                 var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                 identity.AddClaim(new Claim("Sub", context.UserName));


                 CustomRoles.ForEach(delegate(string Role)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, Role.Trim()));
                });

                 var ticket = new AuthenticationTicket(identity, null);
                 context.Validated(ticket);
             });
        }


       private bool CheckValidation(string username, string password)
        {
            //***************************
           //Write / Inject Custom Logic to validate passed creds (Windows impersonated context or passed form enclosed values over https) and pull mapped Roles
           //****************************
            // context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);


            if (username.Trim() == "appuser" && password.Trim() == "xyz100")
            {                
                return true;
            }
            else
            {
                return false;
            }        
        }

       private List<string> UserRoles(string username, string password)
       {
            //*************************** 
       //Write / Inject Custom Logic to validate passed creds (Windows impersonated context or passed form enclosed values over https) and pull mapped Roles
       //****************************              

        
             if (username.Trim() == "appuser" && password.Trim() == "xyz100")
              {
                  return new List<string>()
                    {
                        "Tiger", "Lion"
                    };                
              }
             else
             {
                 return new List<string>(null);
             }
        }

    }
}
