using BearerAuthentication;
using MusicHubBusiness.Amazon;
using MusicHubBusiness.Models;
using MusicHubBusiness.Repository;
using System;
using System.IO;
using System.Web;

namespace MusicHubBusiness.Business
{
    public class RateContributionBusiness : BusinessBase
    {
        public RateContribution Create(RateContribution rateContribution)
        {
            PopulateDefaultProperties(rateContribution);

            Validate(rateContribution);

            RateContributionRepository rateContributionRepository = new RateContributionRepository();
            var retorno = rateContributionRepository.Create(rateContribution);

            return retorno;
        }

        private void PopulateDefaultProperties(RateContribution rateContribution)
        {
            BearerToken bearerToken = new BearerToken();
            var activeToken = bearerToken.GetActiveToken();

            rateContribution.musician_id = int.Parse(activeToken.client);
        }

        private void Validate(RateContribution rateContribution)
        {
            ValidateContributionId(rateContribution);
            ValidateMusicianId(rateContribution);

            if (rateContribution.rate_value <= 0 || rateContribution.rate_value > 5)
            {
                throw ValidateException("O voto deve ser de 1 a 5!");
            }

        }

        private void ValidateContributionId(RateContribution rateContribution)
        {
            if (rateContribution.contribution_id == 0) throw ValidateException("Id da Contribuição inválido!");
        }

        private void ValidateMusicianId(RateContribution rateContribution)
        {
            if (rateContribution.musician_id == 0)
            {
                throw ValidateException("O músico deve ser selecionado!");
            }

            bool targetIdExists = MusicianExists(rateContribution);
            if (!targetIdExists) throw ValidateException("O ID deste usuario é inexistente!");

            Contribution contribution = GetContributionById(rateContribution.contribution_id);

            if (rateContribution.musician_id == contribution.musician_id)
            {
                throw ValidateException("Você não pode votar em sua propria contribuição mesmo!");
            }
        }

        private Contribution GetContributionById(int contribution_id)
        {
            ContributionRepository contributionRepository = new ContributionRepository();
            return contributionRepository.Get(contribution_id);
        }

        private bool MusicianExists(RateContribution rateContribution)
        {
            MusicianRepository musicianRepository = new MusicianRepository();
            var retorno = musicianRepository.Get(rateContribution.musician_id);

            return retorno != null;
        }
    }
}