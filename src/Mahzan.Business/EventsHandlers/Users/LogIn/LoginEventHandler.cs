using Mahzan.Business.Events.Users.LogIn;
using Mahzan.Dapper.DTO.Users.LogIn;
using Mahzan.Dapper.Repositories.Users.Login;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Business.EventsHandlers.Users.LogIn
{
    public class LoginEventHandler : ILoginEventHandler
    {
        private readonly ILoginRepository _loginRepository;

        public IConfiguration _config
        {
            get
            {
                return new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .Build();
            }
        }

        public LoginEventHandler(
            ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<string> HandleEvent(LoginEvent loginEvent)
        {
            string result = null;

            await _loginRepository
                .HandleRepository(new LoginDto
                {
                    UserName = loginEvent.UserName,
                    Password = loginEvent.Password
                });


            result = await GetToken(loginEvent);

            return result;
        }

        private async Task<string> GetToken(LoginEvent loginEvent)
        {

            string result = string.Empty;

            Claim[] claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, loginEvent.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            //SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "http://oec.com",
                audience: "http://oec.com",
                expires: DateTime.UtcNow.AddHours(1),
                claims: claims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            result = new JwtSecurityTokenHandler().WriteToken(token);

            return result;
        }
    }
}
