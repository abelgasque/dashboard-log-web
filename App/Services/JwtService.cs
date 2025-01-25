using App.Entities.EF;
using App.Entities.Settings;
using App.Entities;
using App.Repositories;
using App.Util;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public class JwtService
    {
        #region Variables
        private readonly JwtSettings _jwtSettings;

        private readonly ILogger<JwtService> _logger;

        private readonly JwtRepository _jwtRepository;

        private readonly LogIntegrationService _logIntegrationService;
        #endregion

        #region Properties
        private SqlConnectionResponse SqlConnectionResponse { get; set; }
        #endregion

        #region Constructor
        public JwtService(IOptions<JwtSettings> jwtSettings, ILogger<JwtService> logger, JwtRepository jwtRepository, LogIntegrationService logIntegrationService)
        {
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
            _jwtRepository = jwtRepository;
            _logIntegrationService = logIntegrationService;
        }
        #endregion

        #region Methods
        public async Task<ReturnDTO> AuthenticateUser(UserWs pEntity)
        {
            _logger.LogInformation($"JwtService.AuthenticateUser => Start");
            try
            {
                this.SqlConnectionResponse = await _jwtRepository.AuthenticateUser(pEntity);
                if (this.SqlConnectionResponse.IsSuccess)
                {
                    var UserWsAuthenticated = new UserWsAuthenticated();
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(CryptoHelper.Decrypt(_jwtSettings.Secret));
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.MinutesToExpire),
                        NotBefore = DateTime.Now,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    UserWsAuthenticated.Token = tokenHandler.WriteToken(token);
                    UserWsAuthenticated.DateExpiretedUtcNow = tokenDescriptor.Expires.Value;
                    this.SqlConnectionResponse.DataObject = UserWsAuthenticated;
                    _logger.LogInformation($"JwtService.AuthenticateUser => End - Token: Generated, Token Expire: { tokenDescriptor.Expires }");                    
                }
            }
            catch (Exception ex)
            {
                this.SqlConnectionResponse = new SqlConnectionResponse(false, Constants.StandardErrorMessageWS, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"JwtService.AuthenticateUser => Exception: { ex.Message }");                
            }
            await this._logIntegrationService.AppInsertAsync(null, "AuthenticateUser", null, pEntity, this.SqlConnectionResponse);
            _logger.LogInformation($"JwtService.AuthenticateUser => End");
            return new ReturnDTO(this.SqlConnectionResponse.IsSuccess, this.SqlConnectionResponse.DeMessage, this.SqlConnectionResponse.DataObject);
        }
        #endregion
    }
}
