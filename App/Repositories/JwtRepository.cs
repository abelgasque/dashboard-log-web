using App.Entities;
using App.Util;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace App.Repositories
{
    public class JwtRepository
    {
        #region Variables
        private readonly ILogger<JwtRepository> _logger;

        private readonly DataBaseHelper _dbHelper;
        #endregion

        #region Constructor
        public JwtRepository(ILogger<JwtRepository> logger, DataBaseHelper dbHelper)
        {
            this._logger = logger;
            this._dbHelper = dbHelper;
        }
        #endregion

        #region Properties
        private SqlConnectionResponse SqlConnectionResponse { get; set; }

        private SqlConnection SqlConnection { get; set; }
        #endregion

        #region Methods
        public async Task<SqlConnectionResponse> AuthenticateUser(UserWs pUser)
        {
            _logger.LogInformation("JwtRepository.AuthenticateUser => Start");            
            try
           {
                using (this.SqlConnection = new SqlConnection(_dbHelper.GetConnectionStringSql()))
                {
                    _logger.LogInformation("JwtRepository.AuthenticateUser > Running procedure: " + Constants.SPR_WS_AUTHENTICATE_USER);
                    await this.SqlConnection.OpenAsync();
                    GridReader reader = await this.SqlConnection.QueryMultipleAsync(_dbHelper.GetAuthenticateUser(pUser));
                    var returnList = reader.Read().AsList();
                    _logger.LogInformation("JwtRepository.AuthenticateUser => Count list: " + returnList.Count);
                    if (returnList.Count > 0)
                    {
                        List<SqlConnectionResponse> dataList = returnList.Select(row => new SqlConnectionResponse(row)).ToList();
                        this.SqlConnectionResponse = dataList[0];
                    }
                    else
                    {
                        this.SqlConnectionResponse = new SqlConnectionResponse(false, Constants.DeMessageDataNotFoundWS, null);
                    }
                }
            }
            catch (Exception ex)
            {
                this.SqlConnectionResponse = new SqlConnectionResponse(false, Constants.StandardErrorMessageWS, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"JwtRepository.AuthenticateUser => Exception: { ex.Message }");
            }
            _logger.LogInformation("JwtRepository.AuthenticateUser => End");
            return this.SqlConnectionResponse;
        }
        #endregion
    }
}
