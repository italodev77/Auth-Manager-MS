using Auth_ms.Dtos;
using Auth_ms.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auth_ms.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnterpriseController : ControllerBase
{
    private readonly EnterpriseService _service;

    public EnterpriseController(EnterpriseService service)
    {
        _service = service;
    }

    [HttpPost("create-enterprise")]
    public async Task<IActionResult> Create([FromBody] CreateEnterpriseDto dto)
    {
        try
        {
            var result = await _service.CreateEnterpriseAsync(dto);
            return CreatedAtAction(nameof(GetByCnpj), new { cnpj = result.Cnpj }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }

    [HttpGet("{cnpj}")]
    public async Task<IActionResult> GetByCnpj(string cnpj)
    {
        var result = await _service.GetByCnpjAsync(cnpj);
        if (result == null)
            return NotFound(new { error = "Empresa não encontrada." });

        return Ok(result);
    }

    [HttpPut("update-enterprise")]
    public async Task<IActionResult> Update([FromBody] UpdateEnterpriseDto dto)
    {
        try
        {
            var result = await _service.UpdateEnterpriseAsync(dto);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    [HttpDelete("{cnpj}")]
    public async Task<IActionResult> Delete(string cnpj)
    {
        try
        {
            var result = await _service.DeleteByCnpjAsync(cnpj);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }
}
