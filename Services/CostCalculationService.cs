using CalculationMonthCost.DTOs;
using CalculationMonthCost.Settings;
using Microsoft.Extensions.Options;

namespace CalculationMonthCost.Services;

/// <summary>
/// 費用計算を実行するサービスです。
/// </summary>
public sealed class CostCalculationService : ICostCalculationService
{
    private readonly FixedCostOptions _fixedCostOptions;

    public CostCalculationService(
        IOptions<FixedCostOptions> fixedCostOptions)
    {
        _fixedCostOptions = fixedCostOptions.Value;
    }

    /// <summary>
    /// 利用額と固定費から、それぞれの負担額を計算します。
    /// </summary>
    public CostCalculationResponse Calculate(CostCalculationRequest request)
    {
        // 利用額を2人で均等に分けます。
        decimal baseAmount = request.Amount / 2m;

        // 設定ファイルの固定費を加算します。    
        decimal personS = baseAmount + _fixedCostOptions.PersonS;
        decimal personA = baseAmount + _fixedCostOptions.PersonA;

        return new CostCalculationResponse
        {
            BaseAmount = baseAmount,
            PersonS = personS,
            PersonA = personA
        };
    }
}