namespace Warehouse.Persistence.EF.Configuration.Contstants;

public static class ColumnTypeConstants
{
    public const string DecimalColumnType = "decimal(18, 2)";
    public const string BitColumnType = "bit";
    public const int DescriptionMaxLength = 1000;
    public const int TitleMaxLength = 100;
    public const int LoginMaxLength = 50;
    public const int AddressMaxLength = 100;
    public const int RoleMaxLength = 50;
    public const int RefreshTokenMaxLength = 100;
}