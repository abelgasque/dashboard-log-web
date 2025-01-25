using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Util;

namespace App.Entities.EF
{
    [Table(Constants.USER)]
    public class UserEntity
    {
        [Key]
        [Column(Constants.ID_USER)]
        public long IdUser { get; set; }

        [Column(Constants.NM_USER)]
        public string NmUser { get; set; }

        [Column(Constants.NM_PASSWORD)]
        public string NmPassword { get; set; }

        [Column(Constants.IS_ACTIVE)]
        public bool IsActive { get; set; }
    }
}
