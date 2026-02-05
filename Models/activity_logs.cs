using System;
using System.Collections.Generic;

namespace inspecto_API.Models;

public partial class activity_logs
{
    public Guid id { get; set; }

    public string entity_type { get; set; } = null!;

    public Guid? entity_id { get; set; }

    public string action { get; set; } = null!;

    public string? details { get; set; }

    public Guid? user_id { get; set; }

    public DateTime created_at { get; set; }
}
