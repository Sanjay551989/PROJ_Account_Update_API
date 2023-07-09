using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROJ_Synchronise_Account_Api.Models.Data.Domain
{
    [Table("CssAccountProfiles", Schema = "WaterCorp")]
    public class CssAccountProfile : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid CssAccountProfileId { get; set; }

        public Guid CssAccountId { get; set; }

        [ForeignKey("CssAccountId")]
        public virtual CssAccount CssAccount { get; set; }

        public Guid CssProfileId { get; set; }

        [ForeignKey("CssProfileId")]
        public virtual CssProfile CssProfile { get; set; }
    }
}