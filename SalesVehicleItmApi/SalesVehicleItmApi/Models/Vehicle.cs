using System;
using System.Collections.Generic;

namespace SalesVehicleItmApi.Models;

public partial class Vehicle
{
    public int Id { get; set; }

    public int BrandId { get; set; }

    public string Engine { get; set; } = null!;

    public int Doors { get; set; }

    public string FuelType { get; set; } = null!;

    public string? Accessories { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Sale? Sale { get; set; }
}
