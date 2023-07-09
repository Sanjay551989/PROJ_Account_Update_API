using Microsoft.Extensions.Logging;
using PROJ_Synchronise_Account_Api.DI;
using PROJ_Synchronise_Account_Api.Models;
using PROJ_Synchronise_Account_Api.Models.Data.Dto;
using System;
using System.Linq;

namespace PROJ_Synchronise_Account_Api.Services
{
    [Service(typeof(IUserAccountService))]
    public class UserAccountService : IUserAccountService
    {
        #region Property       
        private readonly AppDbContext _appDbContext;
        private readonly ILogger _logger;        
        #endregion

        #region Constructor
        public UserAccountService(AppDbContext appDbContext, ILogger logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;           
        }
        #endregion
        public ApiResponseDto UpdateAccountDetails(UserDto data)
        {
            ApiResponseDto apiResponseDto = new ApiResponseDto();
            try 
            {               
                Guid UserObjectId;
                if (Guid.TryParse(data.UserObjectId, out UserObjectId))
                {
                    var cssAccounts = _appDbContext.CssAccounts.ToList().Where(s => s.UserObjectId == data.UserObjectId && s.IsDeleted == false);

                    if (cssAccounts == null || cssAccounts.Count() == 0)
                    {
                        apiResponseDto.IsSuccess = false;
                        apiResponseDto.Message = "No record found in database to update the user details.";
                        apiResponseDto.Error = "";
                    }
                    else if(cssAccounts.Count() == 1)
                    {
                        var cssAccount = cssAccounts.FirstOrDefault();
                        if (!string.IsNullOrEmpty(data.FirstName) && !string.IsNullOrEmpty(data.LastName))
                        {
                            cssAccount.FirstName = data.FirstName;
                            cssAccount.LastName = data.LastName;
                            cssAccount.UserName = data.UserName;
                            cssAccount.PhoneNo = data.PhoneNumber;
                            cssAccount.Email = data.Email;
                            cssAccount.PreferredContactMethod = data.PreferredContactMethod;
                        }
                        _appDbContext.CssAccounts.Update(cssAccount);
                        _appDbContext.SaveChanges();

                        apiResponseDto.IsSuccess = true;
                        apiResponseDto.Message = "User details updated successfully.";
                        apiResponseDto.Error = "";
                    }
                    else if (cssAccounts.Count() > 1)
                    {
                        apiResponseDto.IsSuccess = false;
                        apiResponseDto.Message = "Duplicate records found in database.";
                        apiResponseDto.Error = "";

                        //if (IsDuplicateRecordsEqual(cssAccounts))
                        //{
                        //}
                        //else
                        //{                            
                        //}
                    }
                }
                else
                {
                    apiResponseDto.IsSuccess = false;
                    apiResponseDto.Message = "UserObjectId is wrong.";
                    apiResponseDto.Error = "";
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

        //public bool IsDuplicateRecordsEqual(IEnumerable<CssAccount> cssAccounts)
        //{           
        //    //To Do...
           
        //    return true;
        //}


    }
}
