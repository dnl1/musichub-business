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
        private RateMusicianRepository _rateMusicianRepository;

        public RateMusicianBusiness()
        {
            _rateMusicianRepository = new RateMusicianRepository();

        }

        public RateMusician Create(RateMusician rateMusician)
        {
            PopulateDefaultProperties(rateMusician);

            Validate(rateMusician);

            RateMusician objExistente = GetByOwnerId(rateMusician.musician_target_id, rateMusician.musician_owner_id);
            RateMusician retorno = null;

            if (objExistente != null && objExistente.id > 0)
            {
                rateMusician.id = objExistente.id;
                retorno = _rateMusicianRepository.Update(rateMusician);
            }
            else
            {
                retorno = _rateMusicianRepository.Create(rateMusician);
            }

            return retorno;
        }

        public RateMusician GetByOwnerId(int musician_target_id, int musician_owner_id)
        {
            RateMusician retorno = _rateMusicianRepository.GetByOwnerId(musician_target_id, musician_owner_id);
            return retorno;
        }

        private void PopulateDefaultProperties(RateMusician rateMusician)
        {
            rateMusician.musician_owner_id = Utitilities.GetLoggedUserId();
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