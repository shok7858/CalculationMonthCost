using System.ComponentModel.DataAnnotations;

namespace CalculationMonthCost.DTOs;

/// <summary>
/// 費用計算APIへのリクエストDTOです。
/// </summary>
public sealed class CostCalculationRequest
{
    /// <summary>
    /// 月々のクレジット利用額です。
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "利用額は1円以上の整数を指定してください。")]
    public int Amount { get; set; }
}

/// <summary>
/// 費用計算APIのレスポンスDTOです。
/// </summary>
public sealed class CostCalculationResponse
{
    public decimal BaseAmount { get; init; }

    public decimal PersonS { get; init; }

    public decimal PersonA { get; init; }
}