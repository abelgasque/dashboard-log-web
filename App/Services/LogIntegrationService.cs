using App.Entities.DataBase;
using App.Entities.EF;
using App.Entities.Settings;
using App.Entities;
using App.Repositories;
using App.Util;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace App.Services
{
    public class LogIntegrationService
    {
        #region Variables
        private readonly ILogger<LogIntegrationService> _logger;

        private readonly LogIntegrationRepository _logIntegrationRepository;

        private readonly AppSettings _appSettings;
        #endregion

        #region Properties
        private SqlConnectionResponse SqlConnectionResponse { get; set; }
        #endregion

        #region Constructor
        public LogIntegrationService(ILogger<LogIntegrationService> logger, LogIntegrationRepository logIntegrationRepository, IOptions<AppSettings> appSettings)
        {
            this._logger = logger;
            this._logIntegrationRepository = logIntegrationRepository;
            this._appSettings = appSettings.Value;
        }
        #endregion

        #region Methods
        public async Task<ReturnDTO> AppInsertAsync(long? pIdMailing, string pDeMethod, string pDeUrl, object pContent, SqlConnectionResponse pSqlConnectionResponse)
        {                                                                                       
            _logger.LogInformation($"LogIntegrationService.InsertAsync => Start");
            try
            {
                LogIntegrationEntity entity = new LogIntegrationEntity()
                {
                    IdProject = (_appSettings.IdProject != null && _appSettings.IdProject.Value > 0) ? _appSettings.IdProject: null,
                    IdMailing = (pIdMailing != null && pIdMailing.Value > 0) ? pIdMailing : null,
                    IdLogIntegrationType = _appSettings.IdLogIntegrationType,
                    DtCreation = DateTime.Now,
                    DtLastUpdate = null,
                    NuVersion = (!string.IsNullOrEmpty(_appSettings.NuVersion) ? _appSettings.NuVersion : null),
                    DeMethod = (!string.IsNullOrEmpty(pDeMethod) ? pDeMethod : null),
                    DeUrl = (!string.IsNullOrEmpty(pDeUrl) ? pDeUrl : null),
                    DeContent = (pContent != null) ? JsonConvert.SerializeObject(pContent) : null,
                    DeResult = (pSqlConnectionResponse.DataObject != null) ? JsonConvert.SerializeObject(pSqlConnectionResponse.DataObject) : null,
                    DeMessage = (!string.IsNullOrEmpty(pSqlConnectionResponse.DeMessage)) ? pSqlConnectionResponse.DeMessage : null,
                    DeExceptionMessage = (!string.IsNullOrEmpty(pSqlConnectionResponse.DeExceptionMessage)) ? pSqlConnectionResponse.DeExceptionMessage : null,
                    DeStackTrace = (!string.IsNullOrEmpty(pSqlConnectionResponse.DeStackTrace) ? pSqlConnectionResponse.DeStackTrace : null),
                    IsSuccess = pSqlConnectionResponse.IsSuccess,
                    IsTest = _appSettings.IsTest,
                    IsActive = true
                };
                this.SqlConnectionResponse = await this._logIntegrationRepository.InsertAsync(entity);
            }
            catch (Exception ex)
            {
                this.SqlConnectionResponse = new SqlConnectionResponse(false, Constants.StandardErrorMessageWS, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogIntegrationService.GetLogIntegrationForChartDynamic => Exception: { ex.Message }");
            }
            _logger.LogInformation($"LogIntegrationService.InsertAsync => End");
            return new ReturnDTO(this.SqlConnectionResponse.IsSuccess, this.SqlConnectionResponse.DeMessage, this.SqlConnectionResponse.DataObject);
        }

        public async Task<ReturnDTO> InsertAsync(LogIntegrationEntity pLogIntegrationEntity)
        {
            _logger.LogInformation($"LogIntegrationService.InsertAsync => Start");
            try
            {                
                this.SqlConnectionResponse = await this._logIntegrationRepository.InsertAsync(pLogIntegrationEntity);
            }
            catch (Exception ex)
            {
                this.SqlConnectionResponse = new SqlConnectionResponse(false, Constants.StandardErrorMessageWS, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogIntegrationService.GetLogIntegrationForChartDynamic => Exception: { ex.Message }");
            }
            _logger.LogInformation($"LogIntegrationService.InsertAsync => End");
            return new ReturnDTO(this.SqlConnectionResponse.IsSuccess, this.SqlConnectionResponse.DeMessage, this.SqlConnectionResponse.DataObject);
        }

        public async Task<ReturnDTO> GetLogIntegrationForChartDynamic(bool pMustFilterYear)
        {
            _logger.LogInformation($"LogIntegrationService.GetLogIntegrationForChartDynamic => Start => pMustFilterYear: { pMustFilterYear }");
            try
            {
                this.SqlConnectionResponse = await _logIntegrationRepository.GetLogIntegrationForChartDynamic(pMustFilterYear);
                _logger.LogInformation($"LogIntegrationService.GetLogIntegrationForChartDynamic => sqlConnectionResponse.IsSuccess: { this.SqlConnectionResponse.IsSuccess }");
            }
            catch (Exception ex)
            {
                this.SqlConnectionResponse = new SqlConnectionResponse(false, Constants.StandardErrorMessageWS, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogIntegrationService.GetLogIntegrationForChartDynamic => Exception: { ex.Message }");
            }
            _logger.LogInformation($"LogIntegrationService.GetLogIntegrationForChartDynamic => End => pMustFilterYear: { pMustFilterYear }");
            return new ReturnDTO(this.SqlConnectionResponse.IsSuccess, this.SqlConnectionResponse.DeMessage, this.SqlConnectionResponse.DataObject);
        }

        public async Task<ReturnDTO> GetLogIntegrationDay()
        {
            _logger.LogInformation($"LogIntegrationService.GetLogIntegrationDay => Start");
            try
            {
                this.SqlConnectionResponse = await _logIntegrationRepository.GetLogIntegrationDay();
                _logger.LogInformation($"LogIntegrationService.GetLogIntegrationDay => sqlConnectionResponse.IsSuccess: { this.SqlConnectionResponse.IsSuccess }");
            }
            catch (Exception ex)
            {
                this.SqlConnectionResponse = new SqlConnectionResponse(false, Constants.StandardErrorMessageWS, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogIntegrationService.GetLogIntegrationDay => Exception: { ex.Message }");
            }
            _logger.LogInformation($"LogIntegrationService.GetLogIntegrationDay => End");
            return new ReturnDTO(this.SqlConnectionResponse.IsSuccess, this.SqlConnectionResponse.DeMessage, this.SqlConnectionResponse.DataObject);
        }
        #endregion
    }
}
