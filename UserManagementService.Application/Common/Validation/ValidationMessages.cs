namespace Warehouse.Application.Common.Validation;

public static class ValidationMessages
{
    public static string RequiredId(string id) => $"{id} is required";

    public static string RequiredItem(string requiredItem) => $"{requiredItem} is required";

    public static string RequiredOrderDateInPast => "OrderDate must be in the past.";

    public static string ItemBiggerThanZero(string item) => $"{item} must be greater than 0.";

    public static string ItemBiggerOrEqualToZero(string item) => $"{item} must be greater than or equal to 0.";

    public static string IdMustBeProvided(string id) => $"At least one {id} must be provided.";

    public static string ItemExceedCharacters(string item, int charCount) => $"{item} must not exceed {charCount} characters.";

    public const string MinPriceGreaterThanZero = "MinPrice must be greater than zero when provided";
    public const string MinPriceValidDecimal = "MinPrice must be a valid decimal number when provided";
    public const string MaxPriceGreaterThanZero = "MaxPrice must be greater than zero when provided";
    public const string MaxPriceValidDecimal = "MaxPrice must be a valid decimal number when provided";
    public const string SizeMaxLength = "Size length must not exceed 50 characters";
}