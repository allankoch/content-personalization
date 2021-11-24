using System;

namespace ContentPersonalization.Core.Models.Interfaces
{
    public interface IVisit
    {
        Guid VisitId { get; set; }
        Guid VisitorId { get; set; }
        string PageKey { get; set; }
        string Url { get; set; }
        DateTime VisitDate { get; set; }
    }
}
