using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Mahzan.Business.V1.Commands.User;
using Mahzan.Persistance.V1.Dto.User;
using Mahzan.Persistance.V1.Filters.User;
using Mahzan.Persistance.V1.Repositories.User.LogIn;
using Mahzan.Persistance.V1.ViewModel.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

namespace Mahzan.Business.V1.CommandHandlers.User.LogIn
{
    public class LogInCommandHandler
        :CommandHandlerBase<LogInCommad, LogInViewModel>, 
            ILogInCommandHandler
    {
        private readonly ILogInRepository _logInRepository;
        
        private readonly IConfiguration _configuration;
        
        public LogInCommandHandler(
            NpgsqlConnection connection,
            IConfiguration configuration,
            ILogInRepository logInRepository,
            ILogger<CommandHandlerBase<LogInCommad, LogInViewModel>> logger) 
            : base(connection, logger)
        {
            _configuration = configuration;
            _logInRepository = logInRepository;
        }

        protected override async Task<LogInViewModel> HandleTransaction(LogInCommad command)
        {
            LogInDto logInDto =await _logInRepository
                .Update(new LogInDto
                {
                    UserName = command.UserName,
                    Password = command.Password
                });

            LogInViewModel logInViewModel = new LogInViewModel();
            
            logInViewModel.AccessToken = await GetToken(logInDto);

            return logInViewModel;
        }

        protected override Task HandlePrevalidations(LogInCommad command)
        {
            return base.HandlePrevalidations(command);
        }

        #region :: Prevalidations ::



        #endregion

        #region :: Jwt Token Generator ::
        
        private async Task<string> GetToken(LogInDto logInDto)
        {

            string result = string.Empty;

            Claim[] claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, logInDto.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, logInDto.MemberId.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
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
        

        #endregion
    }
}