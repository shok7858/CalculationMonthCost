using CalculationMonthCost.DTOs;
using CalculationMonthCost.Services;
using CalculationMonthCost.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CalculationMonthCost.Controllers;

/// <summary>
/// 費用計算APIを提供するControllerです。
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class CostController : ControllerBase
{
    private readonly ICostCalculationService _calculationService;
    private readonly FixedCostOptions _fixedCostOptions;

    /// <summary>
    /// 費用計算サービスと固定費オプションを受け取ります。
    /// </summary>
    public CostController(
        ICostCalculationService calculationService,
        IOptions<FixedCostOptions> fixedCostOptions)
    {
        _calculationService = calculationService;
        _fixedCostOptions = fixedCostOptions.Value;
    }

    /// <summary>
    /// 固定費設定を取得します。
    /// </summary>
    [HttpGet("settings")]
    public ActionResult<object> GetSettings()
    {
        return Ok(new
        {
            personS = _fixedCostOptions.PersonS,
            personA = _fixedCostOptions.PersonA
        });
    }

    /// <summary>
    /// 利用額と固定費から負担額を計算します。
    /// </summary>
    [HttpPost("calculate")]
    [ProducesResponseType(typeof(CostCalculationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<CostCalculationResponse> Calculate(
        [FromBody] CostCalculationRequest request)
    {
        var response = _calculationService.Calculate(request);

        return Ok(response);
    }
}