using CalculationMonthCost.DTOs;

namespace CalculationMonthCost.Services;

/// <summary>
/// 費用計算の契約を定義するインターフェースです。
/// </summary>
public interface ICostCalculationService
{
    CostCalculationResponse Calculate(CostCalculationRequest request);
}