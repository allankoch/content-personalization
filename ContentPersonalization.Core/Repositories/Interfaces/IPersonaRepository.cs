using ContentPersonalization.Core.Models.Interfaces;
using System.Collections.Generic;

namespace ContentPersonalization.Core.Repositories.Interfaces
{
    public interface IPersonaRepository
    {
        /// <summary>
        /// Returns a single persona given a key
        /// </summary>
        /// <param name="personaKey"></param>
        /// <returns></returns>
        IPersona GetPersona(string personaKey);

        /// <summary>
        /// Returns all personas
        /// </summary>
        /// <returns></returns>
        List<IPersona> GetPersonas();
    }
}
