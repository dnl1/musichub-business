using BearerAuthentication;
using MusicHubBusiness.Amazon;
using MusicHubBusiness.Models;
using MusicHubBusiness.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace MusicHubBusiness.Business
{
    public class MusicalProjectBusiness : BusinessBase
    {
        public MusicalProject Create(MusicalProject musicalProject)
        {
            musicalProject.created_at = DateTime.Now;
            PopulateDefaultProperties(musicalProject);

            Validate(musicalProject);

            MusicalProjectRepository musicalProjectRepository = new MusicalProjectRepository();
            var retorno = musicalProjectRepository.Create(musicalProject);

            return retorno;
        }

        private void PopulateDefaultProperties(MusicalProject musicalProject)
        {
            musicalProject.owner_id = Utitilities.GetLoggedUserId();
            musicalProject.updated_at = DateTime.Now;
        }

        public IEnumerable<MusicalProject> SearchByMusicalGenre(int musical_genre_id)
        {
            MusicalProjectRepository musicalProjectRepository = new MusicalProjectRepository();

            int owner_id = Utitilities.GetLoggedUserId();
            var retorno = musicalProjectRepository.SearchByMusicalGenre(musical_genre_id, owner_id);

            return retorno;
        }

        public IEnumerable<Musician> Musicians(int musical_project_id)
        {
            throw new NotImplementedException();
        }

        private void Validate(MusicalProject musicalProject)
        {
            if (string.IsNullOrEmpty(musicalProject.name))
            {
                throw ValidateException("Insira o nome do seu projeto!");
            }

            if (musicalProject.name.Length > 100)
            {
                throw ValidateException("O tamanho máximo do nome é 100 caracteres!");
            }

            bool genderExiste = VerifyIfGenreExists(musicalProject.musical_genre_id);
            if (musicalProject.musical_genre_id == 0 || !genderExiste)
            {
                throw ValidateException("Este genero musical não existe!");
            }

            bool musicianOwnerExiste = VerifyIfMusicianExists(musicalProject.owner_id);
            if (!musicianOwnerExiste)
            {
                throw ValidateException("O código do músico está errado, entre em contato!");
            }

            if (musicalProject.created_at == null)
            {
                throw ValidateException("Data de criação deve ser preenchida!");
            }

            if (musicalProject.updated_at == null)
            {
                throw ValidateException("Data de atualização deve ser preenchida!");
            }
        }

        private bool VerifyIfMusicianExists(int owner_id)
        {
            MusicianRepository musicianRepository = new MusicianRepository();
            var retorno = musicianRepository.Get(owner_id);

            return retorno != null;
        }

        public MusicalProject Get(int id)
        {
            MusicalProjectRepository musicalProjectRepository = new MusicalProjectRepository();
            MusicalProject retorno = musicalProjectRepository.Get(id);

            return retorno;
        }

        private bool VerifyIfGenreExists(int gender_id)
        {
            MusicalGenreRepository musicalGenreRepository = new MusicalGenreRepository();
            var retorno = musicalGenreRepository.Get(gender_id);

            return retorno != null;
        }

        public IEnumerable<MusicalProject> GetProjectsByOwnerId(int owner_id)
        {
            MusicalProjectRepository musicalProjectRepository = new MusicalProjectRepository();
            IEnumerable<MusicalProject> retorno = musicalProjectRepository.GetByOwnerId(owner_id);

            return retorno;
        }
    }
}