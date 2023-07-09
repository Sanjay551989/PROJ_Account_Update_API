using Microsoft.Extensions.Logging;
using PROJ_Synchronise_Account_Api.DI;
using PROJ_Synchronise_Account_Api.Models;
using PROJ_Synchronise_Account_Api.Models.Data.Dto;
using System;
using System.Linq;

namespace PROJ_Synchronise_Account_Api.Services
{
    [Service(typeof(IOrganisationService))]
    public class OrganisationService : IOrganisationService
    {
        #region Property       
        private readonly AppDbContext _appDbContext;
        private readonly ILogger _logger;        
        #endregion

        #region Constructor
        public OrganisationService(AppDbContext appDbContext, ILogger logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;           
        }
        #endregion
        public ApiResponseDto UpdateOrganisationDetails(OrganisationDto data)
        {
            ApiResponseDto apiResponseDto = new ApiResponseDto();
            try 
            {                
                var organisations = _appDbContext.Organisations.ToList().Where(s => s.ABN == data.Old_ABN && s.TradingName == data.Old_TradingName && s.IsDeleted == false);

                if (organisations == null || organisations.Count() == 0)
                {
                    apiResponseDto.IsSuccess = false;
                    apiResponseDto.Message = "No record found in database to update the organisation details.";
                    apiResponseDto.Error = "";
                }
                else if(organisations.Count() == 1)
                {
                    var organisation = organisations.FirstOrDefault();                    
                    organisation.ABN = data.ABN;
                    organisation.TradingName = data.TradingName;
                   
                    _appDbContext.Organisations.Update(organisation);
                    _appDbContext.SaveChanges();

                    apiResponseDto.IsSuccess = true;
                    apiResponseDto.Message = "Organisation details updated successfully.";
                    apiResponseDto.Error = "";
                }
                else if (organisations.Count() > 1)
                {
                    apiResponseDto.IsSuccess = false;
                    apiResponseDto.Message = "Duplicate records found in database.";
                    apiResponseDto.Error = "";

                    //if (IsDuplicateRecordsEqual(Organisations))
                    //{
                    //}
                    //else
                    //{                            
                    //}
                }
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Error found.", ex);
                apiResponseDto.IsSuccess = false;
                apiResponseDto.Message = "Error occured.";
                apiResponseDto.Error = ex.Message;
            }
            return apiResponseDto;
        }

        //public bool IsDuplicateRecordsEqual(IEnumerable<Organisations> organisations)
        //{           
        //    //To Do...

        //    return true;
        //}


    }
}
