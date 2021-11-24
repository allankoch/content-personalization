using System.Collections.Generic;

namespace ContentPersonalization.Core.Models.Interfaces
{
    public interface IPage
    {
        string PageKey { get; set; }
        string Url { get; set; }
        ICollection<IWeightedInterestArea> WeightedInterestAreas { get; set; }
    }

    public interface IVisitedPage : IPage
    {
        ICollection<IVisit> Visits { get; set; }
    }
}
