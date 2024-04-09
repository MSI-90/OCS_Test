using Application.Dto;
using Application.Dto.Applications.CreateApplication;
using Application.Dto.Applications.UpdateApplication;
using Microsoft.AspNetCore.Mvc;
using NotionTestWork.Application.Services;
using NotionTestWork.DataAccess.Repositories;
using System.Net;

namespace NotionTestWork.Api.Controllers;

[Route("applications")]
[ApiController]
public class ApplicationsController : ControllerBase
{
    private readonly APIResponse _response;
    private readonly ActivitiesService _service;
    private readonly IApplication _appRepo;
    public ApplicationsController(IApplication repo, ActivitiesService service)
    {
        _appRepo = repo;
        _service = service;
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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateApplication([FromBody] CreateApplicationRequest newApp)
    {
        if (string.IsNullOrEmpty(newApp.Author.ToString()))
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.ErrorMessages.Add("Необходимо указать пользователя");
            return BadRequest(_response);
        }

        var responseFromBody = await _appRepo.CreateApplicationAsync(newApp);
        if (string.IsNullOrEmpty(responseFromBody.Name))
        {
            return NotFound();
        }

        return Ok(responseFromBody);
    }

    [HttpPut("{applicationId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateApplication([FromBody] UpdateApplicationRequest newApplication, Guid applicationId)
    {
        var getApplication = await _appRepo.GetApplicationById(applicationId);
        if (string.IsNullOrEmpty(getApplication.Name))
            return NotFound();

        var update = await _appRepo.UpdateApplicationAsync(newApplication, applicationId);
        return Ok(update);
    }

    [HttpDelete("{applicationId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteApplicationById(Guid applicationId)
    {
        var application = await _appRepo.GetApplicationById(applicationId);
        if (string.IsNullOrEmpty(application.Name))
            return NotFound();

        await _appRepo.DeleteApplicationById(applicationId);
        return Ok();
    }

    [HttpPost("{applicationId:guid}/submit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SubmitApplication(Guid applicationId)
    {
        var application = await _appRepo.GetApplicationById(applicationId);
        if (string.IsNullOrEmpty(application.Name))
            return NotFound();

        await _appRepo.SendApplicationAsync(applicationId);
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetApplications([FromQuery] DateTime? submittedAfter, [FromQuery] DateTime? unsubmittedOlder)
    {
        if (submittedAfter.HasValue && unsubmittedOlder.HasValue)
            return BadRequest();

        if (submittedAfter.HasValue)
        {
            var result = await _appRepo.GetApplicationIfSubmittedAsync(submittedAfter.Value);
            return Ok(result);
        }
        else if (unsubmittedOlder.HasValue)
        {
            var result = await _appRepo.GetUnsobmitedApplicationAsync(unsubmittedOlder.Value);
            return Ok(result);
        }
        else
            return BadRequest();
    }


    [HttpGet("/users/{userId:guid}/currentapplication")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrentApplication(Guid userId)
    {
        var result = await _appRepo.GetCurrentApplication(userId);
        return Ok(result);
    }

    [HttpGet("/activities")]
    public IActionResult GetActivities()
    {
        var result = _service.GetActivities();
        return Ok(result);
    }

}
