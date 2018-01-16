using BearerAuthentication;
using MusicHubBusiness.Amazon;
using MusicHubBusiness.Models;
using MusicHubBusiness.Repository;
using System;
using System.IO;
using System.Web;

namespace MusicHubBusiness.Business
{
    public class RateMusicianBusiness : BusinessBase
    {
        public RateMusician Create(RateMusician rateMusician)
        {
            PopulateDefaultProperties(rateMusician);

            Validate(rateMusician);

            RateMusicianRepository rateMusicianRepository = new RateMusicianRepository();
            var retorno = rateMusicianRepository.Create(rateMusician);

            return retorno;
        }

        private void PopulateDefaultProperties(RateMusician rateMusician)
        {
            BearerToken bearerToken = new BearerToken();
            var activeToken = bearerToken.GetActiveToken();

            rateMusician.musician_owner_id = int.Parse(activeToken.client);
        }

        private void Validate(RateMusician rateMusician)
        {

            if (rateMusician.musician_target_id == 0)
            {
                throw ValidateException("O músico deve ser selecionado!");
            }

            if (rateMusician.musician_target_id == rateMusician.musician_owner_id)
            {
                throw ValidateException("Você não pode votar em si mesmo!");
            }

            bool targetIdExists = TargetIdExists(rateMusician);
            if (!targetIdExists) throw ValidateException("O ID deste usuario é inexistente!");

            if (rateMusician.rate_value <= 0 || rateMusician.rate_value > 5)
            {
                throw ValidateException("O voto deve ser de 1 a 5!");
            }

        }

        private bool TargetIdExists(RateMusician rateMusician)
        {
            MusicianRepository musicianRepository = new MusicianRepository();
            var retorno = musicianRepository.Get(rateMusician.musician_target_id);

            return retorno != null;
        }
    }
}