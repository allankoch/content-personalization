using System.Collections.Generic;

namespace ContentPersonalization.Core.Models.Interfaces
{
    public interface IPersona
    {
        string PersonaKey { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        ICollection<IWeightedInterestArea> WeightedInterestAreas { get; set; }
    }

    public interface IScoredPersona : IPersona
    {
        ICollection<IScoredInterestArea> ScoredInterestAreas { get; set; }
    }
}
