using Api.Infrastructure;
using Api.Middlewares.ExceptionMiddleware;
using Application.Dto.Applications.CreateApplication;
using Application.Dto.Applications.UpdateApplication;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NotionTestWork.Api.Controllers;

[Route("applications")]
[ApiController]
public class ApplicationsController : ControllerBase
{
    private readonly IActivities _activities;
    private readonly IApplyAction _action;
    public ApplicationsController(IApplyAction action, IActivities activities)
    {
        _activities = activities;
        _action = action;
    }

    [HttpGet("{applicationId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid applicationId)
    {
        var request = await _action.GetById(applicationId);
        return Ok(request);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateApplication([FromBody] CreateApplicationRequest newApp)
    {
        var request = await _action.CreateOrNot(newApp);
        return Ok(request);
    }

    [HttpPut("{applicationId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateApplication([FromBody] UpdateApplicationRequest newApplication, Guid applicationId)
    {
        //var getApplication = await _appRepo.GetApplicationById(applicationId);
        //if (string.IsNullOrEmpty(getApplication.Name))
        //    return NotFound();

        //var update = await _appRepo.UpdateApplicationAsync(newApplication, applicationId);
        //return Ok(update);
        return Ok();
    }

    [HttpDelete("{applicationId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteApplicationById(Guid applicationId)
    {
        //var application = await _appRepo.GetApplicationById(applicationId);
        //if (string.IsNullOrEmpty(application.Name))
        //    return NotFound();

        //await _appRepo.DeleteApplicationById(applicationId);
        //return Ok();
        return Ok();
    }

    [HttpPost("{applicationId:guid}/submit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SubmitApplication(Guid applicationId)
    {
        //var application = await _appRepo.GetApplicationById(applicationId);
        //if (string.IsNullOrEmpty(application.Name))
        //    return NotFound();

        //await _appRepo.SendApplicationAsync(applicationId);
        //return Ok();
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetApplications([FromQuery] DateTime? submittedAfter, [FromQuery] DateTime? unsubmittedOlder)
    {
        //if (submittedAfter.HasValue && unsubmittedOlder.HasValue)
        //    return BadRequest();

        //if (submittedAfter.HasValue)
        //{
        //    var result = await _appRepo.GetApplicationIfSubmittedAsync(submittedAfter.Value);
        //    return Ok(result);
        //}
        //else if (unsubmittedOlder.HasValue)
        //{
        //    var result = await _appRepo.GetUnsobmitedApplicationAsync(unsubmittedOlder.Value);
        //    return Ok(result);
        //}
        //else
        //    return BadRequest();
        return Ok();
    }


    [HttpGet("/users/{userId:guid}/currentapplication")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrentApplication(Guid userId)
    {
        //var result = await _appRepo.GetCurrentApplication(userId);
        //return Ok(result);
        return Ok();
    }

    [HttpGet("/activities")]
    public IActionResult GetActivities()
    {
        var result = _activities.GetActivities();
        return Ok(result);
    }

}
