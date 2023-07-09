using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PROJ_Synchronise_Account_Api.Models.Data.Dto;
using PROJ_Synchronise_Account_Api.Services;
using System;
using System.IO;

namespace PROJ_Synchronise_Account_Api
{
    public class UpdateUserDetails
    {
        #region Property   
        private readonly IUserAccountService _userAccountService;
        private readonly IOrganisationService _organisationService;
        #endregion

        #region Constructor
        public UpdateUserDetails(IUserAccountService userAccountService, IOrganisationService organisationService)
        {  
            _userAccountService = userAccountService;
            _organisationService = organisationService;
        }
        #endregion


        #region Azure Functions
        /// <summary>
        /// Update User Details
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("UpdateUserDetails")]
        public JsonResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "UpdateUserDetails")] HttpRequest req, ILogger log)
        {
            ApiResponseDto apiResponseDto = new ApiResponseDto();
            try
            {
                log.LogInformation("Updating user account details to Public Website Sitecore database.");
                var requestBody = new StreamReader(req.Body).ReadToEnd();
                UserDto requestData = JsonConvert.DeserializeObject<UserDto>(requestBody);
                if (requestData == null)
                {
                    apiResponseDto.IsSuccess = false;
                    apiResponseDto.Message = "Request data is null.";
                    apiResponseDto.Error = "";                   
                }
                else
                {
                    if (string.IsNullOrEmpty(requestData.UserObjectId))
                    {
                        apiResponseDto.IsSuccess = false;
                        apiResponseDto.Message = "UserObjectId is missing.";
                        apiResponseDto.Error = "";                        
                    }
                    else
                    {
                        apiResponseDto = _userAccountService.UpdateAccountDetails(requestData);                        
                    }
                }                
            }
            catch (Exception ex)
            {
                log.LogInformation("Error found.", ex);
                apiResponseDto.IsSuccess = false;
                apiResponseDto.Message = "Error occured.";
                apiResponseDto.Error = ex.Message;
            }
            return new JsonResult(new { IsSuccess = apiResponseDto.IsSuccess, Message = apiResponseDto.Message, Error = apiResponseDto.Error });
        }
        #endregion



        #region Azure Functions
        /// <summary>
        /// Update Organisation Details
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("UpdateOrganisationDetails")]
        public JsonResult Run1(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "UpdateOrganisationDetails")] HttpRequest req, ILogger log)
        {
            ApiResponseDto apiResponseDto = new ApiResponseDto();
            try
            {
                log.LogInformation("Updating organisation details to Public Website Sitecore database.");
                var requestBody = new StreamReader(req.Body).ReadToEnd();
                OrganisationDto requestData = JsonConvert.DeserializeObject<OrganisationDto>(requestBody);
                if (requestData == null)
                {
                    apiResponseDto.IsSuccess = false;
                    apiResponseDto.Message = "Request data is null.";
                    apiResponseDto.Error = "";
                }
                else
                {
                    if (string.IsNullOrEmpty(requestData.Old_ABN) || string.IsNullOrEmpty(requestData.Old_TradingName))
                    {
                        apiResponseDto.IsSuccess = false;
                        apiResponseDto.Message = "Please provide both Old_ABN and Old_TradingName.";
                        apiResponseDto.Error = "";
                    }
                    else
                    {
                        apiResponseDto = _organisationService.UpdateOrganisationDetails(requestData);
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogInformation("Error found.", ex);
                apiResponseDto.IsSuccess = false;
                apiResponseDto.Message = "Error occured.";
                apiResponseDto.Error = ex.Message;
            }
            return new JsonResult(new { IsSuccess = apiResponseDto.IsSuccess, Message = apiResponseDto.Message, Error = apiResponseDto.Error });
        }
        #endregion

    }
}
