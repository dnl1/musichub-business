using BearerAuthentication;
using MusicHubBusiness.Amazon;
using MusicHubBusiness.Models;
using MusicHubBusiness.Repository;
using System;
using System.IO;
using System.Web;

namespace MusicHubBusiness.Business
{
    public class RateMusicalProjectBusiness : BusinessBase
    {
        public RateMusicalProject Create(RateMusicalProject rateMusicalProject)
        {
            PopulateDefaultProperties(rateMusicalProject);

            Validate(rateMusicalProject);

            RateMusicalProjectRepository rateMusicalProjectRepository = new RateMusicalProjectRepository();
            var retorno = rateMusicalProjectRepository.Create(rateMusicalProject);

            return retorno;
        }

        private void PopulateDefaultProperties(RateMusicalProject rateMusicalProject)
        {
            BearerToken bearerToken = new BearerToken();
            var activeToken = bearerToken.GetActiveToken();

            rateMusicalProject.musician_id = int.Parse(activeToken.client);
        }

        private void Validate(RateMusicalProject rateMusicalProject)
        {
            ValidateMusicianId(rateMusicalProject);
            ValidateMusicalProjectId(rateMusicalProject);

            if (rateMusicalProject.rate_value <= 0 || rateMusicalProject.rate_value > 5)
            {
                throw ValidateException("O voto deve ser de 1 a 5!");
            }

        }

        private void ValidateMusicalProjectId(RateMusicalProject rateMusicalProject)
        {
            if (rateMusicalProject.musical_project_id == 0) throw ValidateException("Id do projeto está inválido!");

            MusicalProject musicalProject = GetMusicalProjectById(rateMusicalProject.musical_project_id);

            if (rateMusicalProject.musician_id == musicalProject.owner_id)
            {
                throw ValidateException("Você não pode votar em seu proprio projeto musical!");
            }
        }

        private void ValidateMusicianId(RateMusicalProject rateMusicalProject)
        {
            if (rateMusicalProject.musician_id == 0)
            {
                throw ValidateException("O músico deve ser selecionado!");
            }

            bool targetIdExists = MusicianExists(rateMusicalProject);
            if (!targetIdExists) throw ValidateException("O ID deste usuario é inexistente!");
        }

        private MusicalProject GetMusicalProjectById(int musical_project_id)
        {
            MusicalProjectRepository musicalProjectRepository = new MusicalProjectRepository();
            return musicalProjectRepository.Get(musical_project_id);
        }

        private bool MusicianExists(RateMusicalProject rateMusicalProject)
        {
            MusicianRepository musicianRepository = new MusicianRepository();
            var retorno = musicianRepository.Get(rateMusicalProject.musician_id);

            return retorno != null;
        }
    }
}