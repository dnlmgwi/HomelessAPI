using System;
using System.Collections.Generic;

namespace HomelessAPI.Entities
{
    public class Benefactor
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required ContactInfo ContactInfo { get; set; }
        public required List<Benefit> ProvidedBenefits { get; set; }
    }
}
