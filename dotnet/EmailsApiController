using Microsoft.AspNetCore.Mvc;
using Immersed.Services;
using Immersed.Web.Controllers;
using Microsoft.Extensions.Logging;
using Immersed.Web.Models.Responses;
using System;
using Immersed.Models.Requests;
using Immersed.Models.Enums;
using System.Security.Claims;
using Immersed.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Immersed.Services.Security;


namespace Immersed.Web.Api.Controllers
{
    [Route("api/emails")]
    [ApiController]
    public class EmailsApiController : BaseApiController
    {

        private IEmailsService _service = null;
        private IUserService _userService = null;
        private ITraineeService _traineeService = null;
        private IAuthenticationService<int> _authService = null;
       
        public EmailsApiController(IUserService userService,IEmailsService service, IAuthenticationService<int> authService, 
            ITraineeService traineeService,ILogger<EmailsApiController> logger
           ) : base(logger)
        {
            _service = service;
            _userService = userService;
            _authService = authService;
            _traineeService = traineeService;
        }


        [HttpPost]        
        public ActionResult<ItemResponse<int>> Create(EmailsAddRequest model)
        {

            ObjectResult result = null;

            try
            {

                _service.WelcomeEmail();
                ItemResponse<int> response = new ItemResponse<int>();
                result = Created201(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);
                result = StatusCode(500, response);
            }

            return result;
        }
   
        [HttpPost("phishing")]
        [Authorize(Roles = "SysAdmin,Super")]
        public ActionResult<ItemResponse<int>> Add(PhishingAddRequest model)
        {

            ObjectResult result = null;

            try
            {
                int tokenTypeId = (int)TokenType.TrainingEvent;
                string token = Guid.NewGuid().ToString();
                int id = _authService.GetCurrentUserId();
              

                
                _service.PhishingEmail(token, model);
                _userService.AddUserToken(token, id, tokenTypeId);

                ItemResponse<int> response = new ItemResponse<int>();
                result = Created201(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);
                result = StatusCode(500, response);
            }

            return result;
        }


        [HttpPut("confirm")]
        [AllowAnonymous]
        public ActionResult<SuccessResponse> Confirm(string token)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {

                _traineeService.ConfirmTrainee(token);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }



    }
}
