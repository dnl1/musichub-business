using BearerAuthentication;
using MusicHubBusiness.Models;
using MusicHubBusiness.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            BearerToken bearerToken = new BearerToken();
            var activeToken = bearerToken.GetActiveToken();

            musicalProject.owner_id = int.Parse(activeToken.client);
            musicalProject.updated_at = DateTime.Now;
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

            bool genderExiste = VerifyIfGenreExists(musicalProject.gender_id);
            if (musicalProject.gender_id == 0 || !genderExiste)
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

        private bool VerifyIfGenreExists(int gender_id)
        {
            MusicalGenreRepository musicalGenreRepository = new MusicalGenreRepository();
            var retorno = musicalGenreRepository.Get(gender_id);

            return retorno != null;
        }
    }
}
