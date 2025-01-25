using App.Entities.Settings;
using App.Entities;
using Microsoft.Extensions.Options;
using System;

namespace App.Util
{
    public class DataBaseHelper
    {
        #region Variables
        private readonly AppSettings _appSettings;
        private readonly DataBaseSettings _dbSettings;
        #endregion

        #region Constructor
        public DataBaseHelper(IOptions<AppSettings> appSettings, IOptions<DataBaseSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
            _appSettings = appSettings.Value;
        }
        #endregion

        public string GetConnectionStringSql()
        {
            return string.Format(_dbSettings.ConnectionString, 
                                    CryptoHelper.Decrypt(_dbSettings.Server), 
                                    CryptoHelper.Decrypt(_dbSettings.Database), 
                                    CryptoHelper.Decrypt(_dbSettings.UserId), 
                                    CryptoHelper.Decrypt(_dbSettings.PasswordDb));
        }

        public string GetAuthenticateUser(UserWs pUser)
        {
            return $"EXEC { Constants.SPR_WS_AUTHENTICATE_USER } "
                    + $"{ Constants.P_USER_NAME } = { pUser.UserName },"
                    + $"{ Constants.P_PASSWORD } = { CryptoHelper.Encrypt(pUser.Password) }";
        }

        public string GetLogIntegrationForChartDynamic(bool pMustFilterYear)
        {
            return  $"EXEC { Constants.SPR_WS_GET_LOG_INTEGRATION_FOR_CHART_DYNAMIC } "
                    + $"{ Constants.P_IS_TEST } = { Convert.ToInt32(_appSettings.IsTest) },"
                    + $"{ Constants.P_MUST_FILTER_YEAR } = { Convert.ToInt32(pMustFilterYear) }";
        }

        public string GetLogIntegrationDay()
        {
            return  $"SELECT TOP 100 * FROM { Constants.VW_WS_GET_LOG_INTEGRATION } "
                    + $"WHERE { Constants.IS_TEST } = { Convert.ToInt32(_appSettings.IsTest) }"
                    + $"AND { Constants.IS_ACTIVE } = 1"
                    + $"AND { Constants.DT_CREATION } BETWEEN DATEADD(D,DATEDIFF(D,0,GETDATE()),0) AND DATEADD(MS, -3, GETDATE())"
                    + $"ORDER BY { Constants.DT_CREATION } DESC;";
        }
    }
}
