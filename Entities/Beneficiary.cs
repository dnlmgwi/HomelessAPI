using HomelessAPI.Domain.Enums;
using System;
using System.Collections.Generic;

namespace HomelessAPI.Entities
{
    public class Beneficiary
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public AgeGroup AgeGroup { get; set; }
        public string Gender { get; set; }
        public GeoLocation Location { get; set; }
        public List<Benefit> BenefitsClaimed { get; set; } = new List<Benefit>();
    }
}
