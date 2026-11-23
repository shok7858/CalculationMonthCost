namespace CalculationMonthCost.Settings;

/// <summary>
/// 人ごとの固定費設定です。
/// </summary>
public sealed class FixedCostOptions
{
    /// <summary>
    /// くまの固定費です。
    /// </summary>
    public decimal PersonS { get; set; }

    /// <summary>
    /// おくの固定費です。
    /// </summary>
    public decimal PersonA { get; set; }
}