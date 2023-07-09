using PROJ_Synchronise_Account_Api.Models.Data.Dto;

namespace PROJ_Synchronise_Account_Api.Services
{
    public interface IOrganisationService
    {
        ApiResponseDto UpdateOrganisationDetails(OrganisationDto data);       
    }
}
