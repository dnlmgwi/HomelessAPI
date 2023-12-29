using System;
using System.Collections.Generic;

namespace HomelessAPI.Entities
{
    public record BenefactorDTO(string Id, string Name, ContactInfo ContactInfo, List<Benefit> ProvidedBenefits);

}
