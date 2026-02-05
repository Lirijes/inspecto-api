using System;
using System.Collections.Generic;

namespace inspecto_API.Models;

public partial class cases
{
    public Guid id { get; set; }

    public Guid? inspection_id { get; set; }

    public string title { get; set; } = null!;

    public string? description { get; set; }

    public string? severity { get; set; }

    public string? status { get; set; }

    public DateTime? created_at { get; set; }

    public string? priority { get; set; }

    public Guid? created_by { get; set; }

    public Guid? assigned_to { get; set; }

    public DateTime? resolved_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual inspections? inspection { get; set; }
}
