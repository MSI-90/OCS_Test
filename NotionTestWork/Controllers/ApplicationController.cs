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
    public class ApplicationController : ControllerBase
    {
        private readonly APIResponse _response;
        private readonly IApplication _appRepo;
        public ApplicationController(IApplication repo)
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
        public async Task<IActionResult> UpdateApplication([FromForm] DataFroUpdateApplication newApplication, Guid applicationId)
        {
            var getApplication = await _appRepo.GetApplicationById(applicationId);
            if (string.IsNullOrEmpty(getApplication.Name))
                return NotFound();

            var update = await _appRepo.UpdateApplicationAsync(newApplication, applicationId);
            return Ok(update);
        }

        //[HttpGet/*(Name = "GetWeatherForecast")*/]

    }
}
