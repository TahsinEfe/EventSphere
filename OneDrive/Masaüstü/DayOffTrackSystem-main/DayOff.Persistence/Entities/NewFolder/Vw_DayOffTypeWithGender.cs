using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Persistence.Entities.NewFolder
{
    [Table("VW_DAY_OFF_TYPES_WITH_GENDER")]
    public class Vw_DayOffTypeWithGender
    {
        [Column("DY_OFF_ID")]
        public decimal DyOffId { get; set; }

        [Column("DY_OFF_NAME")]
        public string DyOffName { get; set; }

        [Column("IS_GENDER_SPECIFIC")]
        public bool? IsGenderSpecific { get; set; }

        [Column("ALLOWED_GENDER_ID")]
        public decimal? AllowedGenderId { get; set; }

        [Column("ALLOWED_GENDER_NAME")]
        public string? AllowedGenderName { get; set; }

        [Column("IS_PARTIAL_ALLOWED")]
        public bool? IsPartialAllowed { get; set; }
    }
}
