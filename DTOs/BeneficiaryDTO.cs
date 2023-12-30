using HomelessAPI.Domain.Enums;
using System;
using System.Collections.Generic;

namespace HomelessAPI.Entities
{
    public record BeneficiaryDTO(Guid Id, string Name, AgeGroup AgeGroup,decimal DistaneTraveled, string Gender, GeoLocation Location);
}
