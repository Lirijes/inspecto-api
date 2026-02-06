using System;
using System.Collections.Generic;

namespace inspecto_API.Models;

public partial class Inspection
{
    public Guid id { get; set; }

    public Guid? object_id { get; set; }

    public DateOnly inspection_date { get; set; }

    public string? inspector { get; set; }

    public string? status { get; set; }

    public string? notes { get; set; }

    public DateTime? created_at { get; set; }

    public string? title { get; set; }

    public string? description { get; set; }

    public Guid? inspector_id { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual Facility? Facility { get; set; }

    public ICollection<Case> Cases { get; set; } = new List<Case>();
}
