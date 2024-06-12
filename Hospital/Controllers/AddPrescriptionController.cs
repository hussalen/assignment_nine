using System.ComponentModel.DataAnnotations;
using assignment_nine.Hospital.Application.DTOs;
using assignment_nine.Hospital.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;

namespace assignment_nine.Hospital.Controllers;

[ApiController]
[Route("api/hospital/prescription")]
public class AddPrescriptionController : ControllerBase
{
    private readonly IAddPrescriptionService _addPrescriptionService;

    public AddPrescriptionController(IAddPrescriptionService addPrescriptionService)
    {
        _addPrescriptionService = addPrescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription(
        [FromBody] AddPrescriptionRequest addPrescriptionRequest
    )
    {
        try
        {
            await _addPrescriptionService.AddPrescription(addPrescriptionRequest);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        return Ok();
    }
}
