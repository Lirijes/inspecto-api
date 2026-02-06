using System;
using System.Collections.Generic;

namespace inspecto_API.Models;

public partial class Facility
{
    public Guid id { get; set; }

    public string name { get; set; } = null!;

    public string? type { get; set; }

    public string? location { get; set; }

    public string? description { get; set; }

    public string? status { get; set; }

    public DateTime? created_at { get; set; }

    public Guid? created_by { get; set; }

    public string? object_type { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<Inspection> Inspections { get; set; } = new List<Inspection>();
}
