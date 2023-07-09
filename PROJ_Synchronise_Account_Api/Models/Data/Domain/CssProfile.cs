using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROJ_Synchronise_Account_Api.Models.Data.Domain
{
    [Table("CssProfiles", Schema = "WaterCorp")]
    public class CssProfile : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid CssProfileId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CssId { get; set; }
        public virtual ICollection<CssAccountProfile> CssAccountProfiles { get; set; }

        //public virtual ICollection<OrganisationProfile> OrganisationProfiles { get; set; }
    }
}