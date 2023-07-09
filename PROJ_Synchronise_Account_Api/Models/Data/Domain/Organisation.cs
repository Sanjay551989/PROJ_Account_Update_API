using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROJ_Synchronise_Account_Api.Models.Data.Domain
{
    [Table("Organisations", Schema = "WaterCorp")]   
    public class Organisation : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int OrganisationId { get; set; }
                
        //[StringLength(50)]
        public string ABN { get; set; }      

        public string TradingName { get; set; } 
    }
}