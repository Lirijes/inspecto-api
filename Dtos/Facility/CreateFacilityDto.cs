namespace inspecto_API.Dtos.Facility
{
    public class CreateFacilityDto
    {
        public string name { get; set; } = null!;
        public string? location { get; set; }
        public string? type { get; set; }
        public string? description { get; set; }
    }
}
