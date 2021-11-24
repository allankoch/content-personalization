using System;

namespace ContentPersonalization.Core.Models.Interfaces
{
    public interface IVisitor
    {
        Guid VisitorId { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
