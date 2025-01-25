using App.Util;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Entities.EF
{
	[Table(Constants.LOG_INTEGRATION)]
	public class LogIntegrationEntity
    {
        [Key]
		[Column(Constants.ID_LOG_INTEGRATION)]
		public long IdLogIntegration { get; set; }
		
		[Column(Constants.ID_PROJECT)]
		public long? IdProject { get; set; }
		
		[Column(Constants.ID_MAILING)]
		public long? IdMailing { get; set; }
		
		[Column(Constants.ID_LOG_INTEGRATION_TYPE)]
		public int IdLogIntegrationType { get; set; }
		
		[Column(Constants.DT_CREATION)]
		public DateTime? DtCreation { get; set; }

		[Column(Constants.DT_LAST_UPDATE)]
		public DateTime? DtLastUpdate { get; set; }
		
		[Column(Constants.NU_VERSION)]
		public string NuVersion { get; set; }
		
		[Column(Constants.DE_METHOD)]
		public string DeMethod { get; set; }
		
		[Column(Constants.DE_URL)]
		public string DeUrl { get; set; }
		
		[Column(Constants.DE_CONTENT)]
		public string DeContent { get; set; }
		
		[Column(Constants.DE_RESULT)]
		public string DeResult { get; set; }
		
		[Column(Constants.DE_MESSAGE)]
		public string DeMessage { get; set; }
		
		[Column(Constants.DE_EXCEPTION_MESSAGE)]
		public string DeExceptionMessage { get; set; }
		
		[Column(Constants.DE_STACK_TRACE)]
		public string DeStackTrace { get; set; }
		
		[Column(Constants.IS_SUCCESS)]
		public bool IsSuccess { get; set; }		

		[Column(Constants.IS_TEST)]
        public bool IsTest { get; set; }

		[Column(Constants.IS_ACTIVE)]
        public bool IsActive { get; set; }
	}
}
