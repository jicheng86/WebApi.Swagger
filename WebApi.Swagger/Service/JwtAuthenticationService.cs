using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Swagger.DomainModel;

namespace WebApi.Swagger.Service
{

    /// <summary>
    /// token认证服务
    /// </summary>
    public class JwtAuthenticationService : IJwtAuthenticateService
    {
        // private readonly IUserService _userService;
        private readonly JwtManagement _tokenManagement;
        public JwtAuthenticationService(IOptions<JwtManagement> jwtManagementOptions)
        {
            _tokenManagement = jwtManagementOptions.Value;
        }
        public bool IsAuthenticated(RequestDTO request, out string jwtString)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var claims = new[] { new Claim(ClaimTypes.Name, request.Username) };
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: _tokenManagement.Issuer,
                                                                     audience: _tokenManagement.Audience,
                                                                     claims: claims,
                                                                     expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                                                                     signingCredentials: credentials);
            jwtString = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return true;
        }
    }
}