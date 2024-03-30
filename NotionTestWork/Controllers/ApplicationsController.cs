using Microsoft.AspNetCore.Mvc;
using MyTaskManager.Models;
using NotionTestWork.Models.DTO_models;
using NotionTestWork.Repositories;
using NotionTestWork.Models.DTO_models.Update;
using System.Net;
using System;

namespace NotionTestWork.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly APIResponse _response;
        private readonly IApplication _appRepo;
        public ApplicationsController(IApplication repo)
        {
            _appRepo = repo;
            this._response = new();
        }

        [HttpGet("{applicationId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid applicationId)
        {
            var result = await _appRepo.GetApplicationById(applicationId);
            if (string.IsNullOrEmpty(result.Name))
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreatApp([FromBody] ApplicationRequest newApp)
        {
            var responseFromBody = await _appRepo.CreateApplicationAsync(newApp);
            if (string.IsNullOrEmpty(responseFromBody.Name))
            {
                return NotFound();
            }

            return Ok(responseFromBody);
        }

        [HttpPut("{applicationId:guid}")]
        public async Task<IActionResult> UpdateApplication([FromBody] DataFroUpdateApplication newApplication, Guid applicationId)
        {
            var getApplication = await _appRepo.GetApplicationById(applicationId);
            if (string.IsNullOrEmpty(getApplication.Name))
                return NotFound();

            var update = await _appRepo.UpdateApplicationAsync(newApplication, applicationId);
            return Ok(update);
        }

        [HttpDelete("{applicationId:guid}")]
        public async Task<IActionResult> DeleteById(Guid applicationId)
        {
            var application = await _appRepo.GetApplicationById(applicationId);
            if (string.IsNullOrEmpty(application.Name))
                return NotFound();

            await _appRepo.DeleteApplicationById(applicationId);
            return Ok();
        }

        [HttpPost("{applicationId:guid}/submit")]
        public async Task<IActionResult> Submit(Guid applicationId)
        {
            var application = await _appRepo.GetApplicationById(applicationId);
            if (string.IsNullOrEmpty(application.Name))
                return NotFound();

            await _appRepo.SendApplicationAsync(applicationId);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetSubmittedApplications([FromQuery] DateTime submittedAfter)
        {
            var result = await _appRepo.GetApplicationIfSubmittedAsync(submittedAfter);
            return Ok(result);
        }



        //[HttpGet/*(Name = "GetWeatherForecast")*/]

    }
}
