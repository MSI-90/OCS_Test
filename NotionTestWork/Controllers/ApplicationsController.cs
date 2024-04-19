using Api.Orchestrator;
using Application.Dto;
using Application.Dto.Applications;
using Application.Dto.Applications.CreateApplication;
using Application.Dto.Applications.UpdateApplication;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NotionTestWork.Api.Controllers;

[Route("applications")]
[ApiController]
public class ApplicationsController(IApplicationService _service,
    IRequestHandler<ApplicationsFromDateQuery, IEnumerable<ApplicationResponse>> _getDateTimeAsQueryHandlercs) : ControllerBase
{
    [HttpGet("{applicationId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetApplicationById(Guid applicationId)
    {
        var request = await _service.GetApplicationById(applicationId);
        return Ok(request);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateApplication([FromBody] CreateApplicationRequest newApp)
    {
        var request = await _service.CreateApplicationAsync(newApp);
        return Ok(request);
    }

    [HttpPut("{applicationId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateApplication([FromBody] UpdateApplicationRequest newApplication, Guid applicationId)
    {
        var request = await _service.UpdateApplicationAsync(newApplication, applicationId);
        return Ok(request);
    }

    [HttpDelete("{applicationId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteApplicationById(Guid applicationId)
    {
        await _service.DeleteApplicationById(applicationId);
        return Ok();
    }

    [HttpPost("{applicationId:guid}/submit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SubmitApplication(Guid applicationId)
    {
        await _service.SendApplicationAsync(applicationId);
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSubmittedApplications([FromQuery] ApplicationsFromDateQuery query)
    {
        var request = await _getDateTimeAsQueryHandlercs.Handle(query);
        return Ok(request);
    }

    [HttpGet("/users/{userId:guid}/currentapplication")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUnsubmittedAppliationForUser(Guid userId)
    {
        var request = await _service.GetCurrentUnsubmittedApplicationForUserAsync(userId);
        return Ok(request);
    }
}
