using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROJ_Synchronise_Account_Api.Models.Data.Domain
{
    [Table("CssAccounts", Schema = "WaterCorp")]
    public class CssAccount : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid CssAccountId { get; set; }
       
        [StringLength(50)]
        public string UserObjectId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNo { get; set; }

        public bool CanView3rdPartyBills { get; set; }

        public string ContactEmailAddress { get; set; }

        public bool IsNonResidential { get; set; }

        public string PreferredContactMethod { get; set; }

        //public virtual ICollection<OrganisationUser> OrganisationUsers { get; set; }

        public virtual ICollection<CssAccountProfile> CssAccountProfiles { get; set; }

        public int RegistrationTypeId { get; set; }

        //[ForeignKey("RegistrationTypeId")]
        //public virtual RegistrationType RegistrationType { get; set; }

        // Flag to show (or not) the onboarding experience
        public bool IsOnboardingComplete { get; set; } = false;
    }
}