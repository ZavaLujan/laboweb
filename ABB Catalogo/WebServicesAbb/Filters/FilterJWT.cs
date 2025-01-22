using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebServicesAbb.Filters
{
    public class FIlterJWT : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // Verificar si es el AuthController
            var route = request.GetRouteData();
            if (route != null)
            {
                var controller = route.Values["controller"]?.ToString();
                if (controller?.ToLower() == "auth")
                {
                    return await base.SendAsync(request, cancellationToken);
                }
            }

            // Validar presencia del token
            string token;
            if (!TryRetrieveToken(request, out token))
            {
                return request.CreateResponse(HttpStatusCode.Unauthorized, "Token no proporcionado");
            }

            try
            {
                string clave = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
                byte[] claveEnBytes = Encoding.UTF8.GetBytes(clave.PadRight(32));
                SymmetricSecurityKey key = new SymmetricSecurityKey(claveEnBytes);

                var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
                var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];

                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = audienceToken,
                    ValidIssuer = issuerToken,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero // Opcional: para ser más estricto con la expiración
                };

                // Validar el token
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                // Establecer el principal en ambos contextos
                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = principal;
                }

                return await base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenExpiredException)
            {
                return request.CreateResponse(HttpStatusCode.Unauthorized, "Token expirado");
            }
            catch (SecurityTokenValidationException)
            {
                return request.CreateResponse(HttpStatusCode.Unauthorized, "Token inválido");
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.InternalServerError,
                    "Error al procesar el token: " + ex.Message);
            }
        }

        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) ||
                authzHeaders.Count() > 1)
            {
                return false;
            }

            var bearerToken = authzHeaders.ElementAt(0);
            if (!bearerToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            token = bearerToken.Substring(7);
            return true;
        }
    }
}