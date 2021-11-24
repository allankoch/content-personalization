using ContentPersonalization.Core.Models.Interfaces;
using System.Collections.Generic;

namespace ContentPersonalization.Core.Repositories.Interfaces
{
    public interface IInterestAreaRepository
    {
        /// <summary>
        /// Returns a single interest area given a key
        /// </summary>
        /// <param name="interestAreaKey"></param>
        /// <returns></returns>
        IInterestArea GetInterestArea(string interestAreaKey);

        /// <summary>
        /// Returns all interest areas
        /// </summary>
        /// <returns></returns>
        List<IInterestArea> GetInterestAreas();
    }
}
