using App.Entities.DataBase;
using App.Entities.EF;
using App.Entities.Settings;
using App.Entities;
using App.Util;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace App.Repositories
{
    public class LogIntegrationRepository
    {
        #region Variables
        private readonly AppSettings _appSettings;

        private readonly ILogger<LogIntegrationRepository> _logger;

        private readonly DataBaseHelper _dbHelper;

        private readonly AppDataContext _context;
        #endregion

        #region Properties
        private SqlConnectionResponse SqlConnectionResponse { get; set; }

        private SqlConnection SqlConnection { get; set; }
        #endregion

        #region Constructor
        public LogIntegrationRepository(IOptions<AppSettings> appSettings, ILogger<LogIntegrationRepository> logger, DataBaseHelper dbHelper, AppDataContext context)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
            _dbHelper = dbHelper;
            _context = context;
        }
        #endregion

        #region Methods

        public async Task<SqlConnectionResponse> InsertAsync(LogIntegrationEntity pLogIntegrationEntity)
        {
            _logger.LogInformation("LogIntegrationRepository.InsertAsync => Start");
            this.SqlConnectionResponse = new SqlConnectionResponse();
            try
            {
                _context.LogIntegration.Add(pLogIntegrationEntity);
                int nuResult = await _context.SaveChangesAsync();      
                if (nuResult > 0)
                {
                    this.SqlConnectionResponse = new SqlConnectionResponse(true, Constants.DeMessageSuccessWS, null);
                }
                else
                {
                    this.SqlConnectionResponse = new SqlConnectionResponse(false, Constants.DeMessageDataNotFoundWS, null);
                }
            }
            catch (SqlException ex)
            {
                this.SqlConnectionResponse = new SqlConnectionResponse(false, Constants.StandardErrorMessageWS, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError("LogIntegrationRepository.InsertAsync => SqlException: " + ex.Message.ToString());
            }
            catch (Exception ex)
            {
                this.SqlConnectionResponse = new SqlConnectionResponse(false, Constants.StandardErrorMessageWS, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError("LogIntegrationRepository.InsertAsync => Exception: " + ex.Message.ToString());
            }
            _logger.LogInformation("LogIntegrationRepository.InsertAsync > Finish");
            return this.SqlConnectionResponse;
        }

        public async Task<SqlConnectionResponse> UpdateAsync(LogIntegrationEntity pLogIntegrationEntity)
        {
            _logger.LogInformation("LogIntegrationRepository.UpdateAsync => Start");
            this.SqlConnectionResponse = new SqlConnectionResponse();
            try
            {
                _context.LogIntegration.Update(pLogIntegrationEntity);
                int nuResult = await _context.SaveChangesAsync();      
                if (nuResult > 0)
                {
                    this.SqlConnectionResponse = new SqlConnectionResponse(true, Constants.DeMessageSuccessWS, null);
                }
                else
                {
                    this.SqlConnectionResponse = new SqlConnectionResponse(false, Constants.DeMessageDataNotFoundWS, null);
                }
            }
            catch (SqlException ex)
            {
                this.SqlConnectionResponse = new SqlConnectionResponse(false, Constants.StandardErrorMessageWS, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError("LogIntegrationRepository.UpdateAsync => SqlException: " + ex.Message.ToString());
            }
            catch (Exception ex)
            {
                this.SqlConnectionResponse = new SqlConnectionResponse(false, Constants.StandardErrorMessageWS, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError("LogIntegrationRepository.UpdateAsync => Exception: " + ex.Message.ToString());
            }
            _logger.LogInformation("LogIntegrationRepository.UpdateAsync > Finish");
            return this.SqlConnectionResponse;
        }

        public async Task<SqlConnectionResponse> GetLogIntegrationForChartDynamic(bool pMustFilterYear)
        {
            _logger.LogInformation("LogIntegrationRepository.GetLogIntegrationForChartDynamic => Start");
            this.SqlConnectionResponse = new SqlConnectionResponse();
            try
            {
                using (this.SqlConnection = new SqlConnection(_dbHelper.GetConnectionStringSql()))
                {
                    _logger.LogInformation("LogIntegrationRepository.GetLogIntegrationForChartDynamic => Running procedure: " + Constants.SPR_WS_GET_LOG_INTEGRATION_FOR_CHART_DYNAMIC);                    
                    await this.SqlConnection.OpenAsync();                    
                    GridReader reader = await this.SqlConnection.QueryMultipleAsync(_dbHelper.GetLogIntegrationForChartDynamic(pMustFilterYear));
                    var returnList = reader.Read().AsList();
                    _logger.LogInformation("LogIntegrationRepository.GetLogIntegrationForChartDynamic => Count list: " + returnList.Count);
                    if (returnList.Count > 0)
                    {
                        object dataObject = null;
                        if (pMustFilterYear)
                        {
                            dataObject = returnList.Select(row => new GetLogIntegrationForChartYearDb(row)).ToList();
                        }
                        else
                        {
                            dataObject = returnList.Select(row => new GetLogIntegrationForChartMonthDb(row)).ToList();
                        }
                        this.SqlConnectionResponse = new SqlConnectionResponse(true, Constants.DeMessageSuccessWS, dataObject);
                    }
                    else
                    {
                        this.SqlConnectionResponse = new SqlConnectionResponse(false, Constants.DeMessageDataNotFoundWS, null);
                    }
                }
            }
            catch (SqlException ex)
            {
                this.SqlConnectionResponse = new SqlConnectionResponse(false, Constants.StandardErrorMessageWS, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError("LogIntegrationRepository.GetLogIntegrationForChartDynamic => SqlException: " + ex.Message.ToString());
            }
            catch (Exception ex)
            {
                this.SqlConnectionResponse = new SqlConnectionResponse(false, Constants.StandardErrorMessageWS, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError("LogIntegrationRepository.GetLogIntegrationForChartDynamic => Exception: " + ex.Message.ToString());
            }

            _logger.LogInformation("LogIntegrationRepository.GetLogIntegrationForChartDynamic => Finish");
            return this.SqlConnectionResponse;
        }

        public async Task<SqlConnectionResponse> GetLogIntegrationDay()
        {
            _logger.LogInformation("LogIntegrationRepository.GetLogIntegrationDay => Start");
            this.SqlConnectionResponse = new SqlConnectionResponse();
            try
            {
                using (this.SqlConnection = new SqlConnection(_dbHelper.GetConnectionStringSql()))
                {
                    await this.SqlConnection.OpenAsync();
                    _logger.LogInformation("LogIntegrationRepository.GetLogIntegrationDay => Running view: " + Constants.VW_WS_GET_LOG_INTEGRATION);                                
                    GridReader reader = await this.SqlConnection.QueryMultipleAsync(_dbHelper.GetLogIntegrationDay());
                    var returnList = reader.Read().AsList();
                    _logger.LogInformation("LogIntegrationRepository.GetLogIntegrationDay => Count list: " + returnList.Count);
                    if (returnList.Count > 0)
                    {
                        object dataObject = returnList.Select(row => new GetLogIntegrationDb(row)).ToList();
                        this.SqlConnectionResponse = new SqlConnectionResponse(true, Constants.DeMessageSuccessWS, dataObject);
                    }
                    else
                    {
                        this.SqlConnectionResponse = new SqlConnectionResponse(false, Constants.DeMessageDataNotFoundWS, null);
                    }
                }
            }
            catch (SqlException ex)
            {
                this.SqlConnectionResponse = new SqlConnectionResponse(false, Constants.StandardErrorMessageWS, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError("LogIntegrationRepository.GetLogIntegrationDay => SqlException: " + ex.Message.ToString());
            }
            catch (Exception ex)
            {
                this.SqlConnectionResponse = new SqlConnectionResponse(false, Constants.StandardErrorMessageWS, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError("LogIntegrationRepository.GetLogIntegrationDay => Exception: " + ex.Message.ToString());
            }

            this._logger.LogInformation("LogIntegrationRepository.GetLogIntegrationDay => Finish");
            return this.SqlConnectionResponse;
        }
        #endregion
    }
}
