using ContentPersonalization.Core.Models.Interfaces;
using System.Collections.Generic;

namespace ContentPersonalization.Core.Services.Interfaces
{
    public interface IPersonalizationService
    {
        /// <summary>
        /// List of all calculated personas, which have a final score.
        /// Each persona also have a breakdown of sub-scores from all interest areas.
        /// The calculated personas are sorted by highest score to lowest score.
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        List<IScoredPersona> GetScoredPersonas(IVisitor visitor);
        
        /// <summary>
        /// Gets the total score for a scored persona
        /// </summary>
        /// <param name="scoredPersona"></param>
        /// <returns></returns>
        double GetTotalScore(IScoredPersona scoredPersona);
    }
}
