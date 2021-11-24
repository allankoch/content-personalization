using ContentPersonalization.Core.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace ContentPersonalization.Core.Repositories.Interfaces
{
    public interface IVisitorRepository
    {
        /// <summary>
        /// Returns a newly created visitor. Visitor has been persisted to storage
        /// </summary>
        /// <returns></returns>
        IVisitor StoreNewVisitor();

        /// <summary>
        /// Returns a single visitor given an id
        /// </summary>
        /// <param name="visitorId"></param>
        /// <returns></returns>
        IVisitor GetVisitor(Guid visitorId);

        /// <summary>
        /// Returns all visitors
        /// </summary>
        /// <returns></returns>
        ICollection<IVisitor> GetVisitors();

        /// <summary>
        /// Deletes a visitor and returns true if deletion was successfull
        /// </summary>
        /// <param name="visitorId"></param>
        /// <returns></returns>
        bool DeleteVisitor(Guid visitorId);

        /// <summary>
        /// Stores a visit by a visitor. Use this method for un-registered pages
        /// </summary>
        /// <param name="visitorId"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        IVisit StoreNewVisit(Guid visitorId, string url);

        /// <summary>
        /// Stores a visit by a visitor. Use this method for registered pages
        /// </summary>
        /// <param name="visitorId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        IVisit StoreNewVisit(Guid visitorId, IPage page);

        /// <summary>
        /// Returns a single visit
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        IVisit GetVisit(Guid visitId);

        /// <summary>
        /// Returns all visits for a visitor
        /// </summary>
        /// <param name="visitorId"></param>
        /// <returns></returns>
        ICollection<IVisit> GetVisits(Guid visitorId);

        /// <summary>
        /// Deletes a single visit, and returns true if successfull
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        bool DeleteVisit(Guid visitId);
    }
}
