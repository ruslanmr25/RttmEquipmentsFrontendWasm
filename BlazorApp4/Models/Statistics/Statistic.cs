using System;

namespace BlazorApp4.Models.Statistics;

public class StatisticData
{
    public int UserCount { get; set; }
    public int DeletedEquipmentCount { get; set; }
    public int DepartmentCount { get; set; }
    public int EquipmentsCount { get; set; }
    public List<StatisticCategory> Categories { get; set; } = new();
    public List<StatisticEquipmentType> EquipmentTypes { get; set; } = new();
    public List<StatisticDepartment> Departments { get; set; } = new();

    public List<StatisticUser> Users { get; set; } = new();
    public int YearlyEquipmentCount { get; set; }
}

public class StatisticCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int EquipmentsCount { get; set; }
}

public class StatisticEquipmentType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public int EquipmentsCount { get; set; }
}

public class StatisticDepartment
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? UserId { get; set; }
    public int EquipmentsCount { get; set; }
}

public class StatisticUser
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;

    public int EquipmentsCount { get; set; }
}
