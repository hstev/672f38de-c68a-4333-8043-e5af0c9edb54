using System;
using System.Collections.Generic;

namespace SalesVehicleItmApi.Models;

public partial class Sale
{
    public int Id { get; set; }

    public int AgencyId { get; set; }

    public int CustomerId { get; set; }

    public int VehicleId { get; set; }

    public DateTime? SaleDate { get; set; }

    public virtual Agency Agency { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
