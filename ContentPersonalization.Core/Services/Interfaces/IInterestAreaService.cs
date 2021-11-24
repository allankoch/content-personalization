using ContentPersonalization.Core.Models.Interfaces;

namespace ContentPersonalization.Core.Services.Interfaces
{
    public interface IInterestAreaService
    {
        /// <summary>
        /// Get the score for an interest area for a certain visitor towards a certain persona and certain intrest area
        /// </summary>
        /// <param name="visitor"></param>
        /// <param name="persona"></param>
        /// <param name="interestArea"></param>
        /// <returns></returns>
        double GetInterestAreaScore(IVisitor visitor, IPersona persona, IInterestArea interestArea);
    }
}
