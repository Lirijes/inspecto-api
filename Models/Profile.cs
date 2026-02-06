using System;
using System.Collections.Generic;

namespace inspecto_API.Models;

public partial class Profile
{
    public Guid id { get; set; }

    public Guid user_id { get; set; }

    public string? email { get; set; }

    public string? full_name { get; set; }

    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }
}
