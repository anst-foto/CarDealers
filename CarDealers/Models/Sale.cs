using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;

namespace CarDealers.Models;

public class Sale
{
    public enum Month
    {
        January, February, March, April, May, June, July, August, September, October, November, December
    }

    public string Car { get; set; }
    public int Year { get; set; }
    public Dictionary<Month, decimal> SalesByMonth { get; set; }

    [JsonIgnore]
    public string January => SalesByMonth.TryGetValue(Month.January, out var value)
        ? value.ToString(CultureInfo.CurrentCulture)
        : "0";

    [JsonIgnore]
    public string February => SalesByMonth.TryGetValue(Month.February, out var value)
        ? value.ToString(CultureInfo.CurrentCulture)
        : "0";

    [JsonIgnore]
    public string March => SalesByMonth.TryGetValue(Month.March, out var value)
        ? value.ToString(CultureInfo.CurrentCulture)
        : "0";

    [JsonIgnore]
    public string April => SalesByMonth.TryGetValue(Month.April, out var value)
        ? value.ToString(CultureInfo.CurrentCulture)
        : "0";

    [JsonIgnore]
    public string May => SalesByMonth.TryGetValue(Month.May, out var value)
        ? value.ToString(CultureInfo.CurrentCulture)
        : "0";

    [JsonIgnore]
    public string June => SalesByMonth.TryGetValue(Month.June, out var value)
        ? value.ToString(CultureInfo.CurrentCulture)
        : "0";

    [JsonIgnore]
    public string July => SalesByMonth.TryGetValue(Month.July, out var value)
        ? value.ToString(CultureInfo.CurrentCulture)
        : "0";

    [JsonIgnore]
    public string August => SalesByMonth.TryGetValue(Month.August, out var value)
        ? value.ToString(CultureInfo.CurrentCulture)
        : "0";

    [JsonIgnore]
    public string September => SalesByMonth.TryGetValue(Month.September, out var value)
        ? value.ToString(CultureInfo.CurrentCulture)
        : "0";

    [JsonIgnore]
    public string October => SalesByMonth.TryGetValue(Month.October, out var value)
        ? value.ToString(CultureInfo.CurrentCulture)
        : "0";

    [JsonIgnore]
    public string November => SalesByMonth.TryGetValue(Month.November, out var value)
        ? value.ToString(CultureInfo.CurrentCulture)
        : "0";

    [JsonIgnore]
    public string December => SalesByMonth.TryGetValue(Month.December, out var value)
        ? value.ToString(CultureInfo.CurrentCulture)
        : "0";
}
