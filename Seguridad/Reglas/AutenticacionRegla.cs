
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Reglas
{
    public class AutenticacionRegla : IAutenticacionRegla
    {

        private readonly IConfiguration _configuration;
        private readonly IUsuarioDA _usuarioDA;

        private readonly IContrasenaRegla _contrasenaRegla;

        public AutenticacionRegla(IConfiguration configuration, IUsuarioDA usuarioDA, IContrasenaRegla contrasenaRegla)
        {
            _configuration = configuration;
            _usuarioDA = usuarioDA;
            _contrasenaRegla = contrasenaRegla;
        }

        public async Task<Token> Login(Login login)
        {
            Token accessToken = new Token()
            {
                AccessToken = string.Empty,
                ValidacionExitosa = false
            };
            if (!await CredencialesValidas(login))
            {
                return accessToken;
            }
            TokenConfiguracion tokenConfiguracion = _configuration.GetSection("TokenConfiguracion").Get<TokenConfiguracion>();
            JwtSecurityToken token = await GenerarToken(login, tokenConfiguracion);
            accessToken.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
            accessToken.ValidacionExitosa = true;
            return accessToken;
        }

        private async Task<bool> CredencialesValidas(Login login)
        {
            UsuarioResponse usuario = await _usuarioDA.ObtenerUsuario(login.Correo);
            return usuario != null && _contrasenaRegla.Verificar(login.PasswordHash, usuario.PasswordHash) && usuario.Correo == login.Correo && usuario.Estado == true;
        }

        private async Task<JwtSecurityToken> GenerarToken(Login login, TokenConfiguracion tokenConfiguracion)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguracion.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = await GenerarClaims(login);
            var token = new JwtSecurityToken(
                issuer: tokenConfiguracion.Issuer,
                audience: tokenConfiguracion.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(tokenConfiguracion.Expire),
                signingCredentials: credentials
            );
            return token;
        }

        private async Task<List<Claim>> GenerarClaims(Login login)
        {
            List<Claim> claims = new List<Claim>();
            UsuarioResponse usuario = await _usuarioDA.ObtenerUsuario(login.Correo);
            claims.Add(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, login.Correo));
            claims.Add(new Claim(ClaimTypes.Name, usuario.Nombre));
            claims.Add(new Claim(ClaimTypes.Role, usuario.IdRol.ToString()));
            return claims;
        }

    }
}
