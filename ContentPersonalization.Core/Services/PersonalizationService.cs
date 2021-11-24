using ContentPersonalization.Core.Models.Interfaces;
using ContentPersonalization.Core.Repositories.Interfaces;
using ContentPersonalization.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContentPersonalization.Core.Services
{
    public class PersonalizationService<T, TU> : IPersonalizationService
        where T : IScoredPersona
        where TU : IScoredInterestArea
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IInterestAreaRepository _interestAreaRepository;
        private readonly IInterestAreaService _interestAreaService;

        public PersonalizationService(IPersonaRepository personaRepository, IInterestAreaRepository interestAreaRepository, IInterestAreaService interestAreaService)
        {
            _personaRepository = personaRepository;
            _interestAreaRepository = interestAreaRepository;
            _interestAreaService = interestAreaService;
        }

        public List<IScoredPersona> GetScoredPersonas(IVisitor visitor)
        {
            var personas = _personaRepository.GetPersonas();
            var interestAreas = _interestAreaRepository.GetInterestAreas();

            var list = new List<IScoredPersona>();

            foreach (var persona in personas)
            {
                var scoredPersona = (IScoredPersona) Activator.CreateInstance(typeof(T));
                scoredPersona.PersonaKey = persona.PersonaKey;
                scoredPersona.Name = persona.Name;
                scoredPersona.Description = persona.Description;
                scoredPersona.WeightedInterestAreas = persona.WeightedInterestAreas;
                scoredPersona.ScoredInterestAreas = new List<IScoredInterestArea>();

                foreach (var interestArea in interestAreas)
                {
                    var scoredInterestArea = (IScoredInterestArea) Activator.CreateInstance(typeof(TU));
                    scoredInterestArea.InterestAreaKey = interestArea.InterestAreaKey;
                    scoredInterestArea.Name = interestArea.Name;
                    scoredInterestArea.Score = _interestAreaService.GetInterestAreaScore(visitor, persona, interestArea);
                    
                    scoredPersona.ScoredInterestAreas.Add(scoredInterestArea);
                }

                list.Add(scoredPersona);
            }

            return list.OrderByDescending(x => x.ScoredInterestAreas.Sum(y => y.Score)).ToList();
        }

        public double GetTotalScore(IScoredPersona scoredPersona)
        {
            if (scoredPersona == null)
                return 0;
            if (!scoredPersona.ScoredInterestAreas.Any())
                return 0;
            return scoredPersona.ScoredInterestAreas.Sum(x => x.Score);
        }
    }
}
