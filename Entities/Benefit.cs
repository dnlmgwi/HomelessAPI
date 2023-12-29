namespace HomelessAPI.Entities
{
    public class Benefit
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
