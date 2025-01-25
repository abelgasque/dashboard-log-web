using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Util;

namespace App.Entities.EF
{
    [Table(Constants.LOG_INTEGRATION_TYPE)]
    public class LogIntegrationTypeEntity
    {
        [Key]
        [Column(Constants.ID_LOG_INTEGRATION_TYPE)]
        public int IdLogIntegrationType { get; set; }

        [Column(Constants.NM_LOG_INTEGRATION_TYPE)]
        public string NmLogIntegrationType { get; set; }

        [Column(Constants.IS_ACTIVE)]
        public bool IsActive { get; set; }
    }
}
