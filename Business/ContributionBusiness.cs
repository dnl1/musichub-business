using MusicHubBusiness.Models;
using MusicHubBusiness.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHubBusiness.Business
{
    public class ContributionBusiness:BusinessBase
    {
        public Contribution Create(Contribution contribution)
        {
            PopulateDefaultProperties(contribution);
            Validate(contribution);

            ContributionRepository contributionRepository = new ContributionRepository();
            var retorno = contributionRepository.Create(contribution);

            return retorno;
        }

        public IEnumerable<Musician> GetByMusicalProjectId(int musical_project_id)
        {
            ContributionRepository contributionRepository = new ContributionRepository();
            IEnumerable<Contribution> contributions = contributionRepository.GetByMusicalProjectId(musical_project_id);

            IEnumerable<int> musician_ids  = contributions.Select(c => c.musician_id);

            MusicianRepository musicianRepository = new MusicianRepository();
            IEnumerable<Musician> retorno = musicianRepository.GetMusicians(musician_ids);

            return retorno;
        }

        public IEnumerable<Contribution> GetFreeContributions(int id)
        {
            ContributionRepository contributionRepository = new ContributionRepository();
            return contributionRepository.GetFreeContributions(id);
        }

        private void PopulateDefaultProperties(Contribution contribution)
        {
            contribution.created_at = DateTime.Now;

            if (contribution.type_id == Enum.eContributionType.FreeContribution) contribution.status_id = Enum.eContributionStatus.FreeContribution;
        }

        private void Validate(Contribution contribution)
        {
            if (contribution.musician_id == 0) throw ValidateException("Id do Musician é requerido");
            if (contribution.status_id == 0) throw ValidateException("Status é requerido");
            if (contribution.type_id == 0) throw ValidateException("Tipo de contribuição é requerido");
            if (contribution.musical_genre_id == 0) throw ValidateException("Genero musical é requerido");
        }
    }
}
