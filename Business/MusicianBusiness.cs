using MusicHubBusiness.Models;
using MusicHubBusiness.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicHubBusiness.Business
{
    public class MusicianBusiness : BusinessBase
    {
        private MusicianRepository musicianRepository;

        public MusicianBusiness()
        {
            musicianRepository = new MusicianRepository();
        }

        public Musician Get(int id)
        {
            Musician retorno = musicianRepository.Get(id);
            return retorno;
        }

        public Musician Create(Musician musician)
        {
            Validate(musician);

            Musician retorno = musicianRepository.Create(musician);

            return retorno;
        }

        public Musician Login(string email, string password)
        {
            Musician retorno = musicianRepository.Login(email, password);

            return retorno;
        }

        public IEnumerable<Musician> SearchByName(string name)
        {
            IEnumerable<Musician> retorno = musicianRepository.SearchByName(name);

            return retorno;
        }

        public Musician Update(Musician musician)
        {
            if (musician.id == 0) throw ValidateException("O Id deve ser preenchido!");
            Validate(musician, true);

            Musician retorno = musicianRepository.Update(musician);

            return retorno;
        }

        private void VerifyIfEmailExists(string email)
        {
            Musician retorno = musicianRepository.GetByEmail(email);

            if (retorno != null) throw ValidateException("O e-mail está em uso!");
        }

        public IEnumerable<Musician> GetAll()
        {
            IEnumerable<Musician> retorno = musicianRepository.GetAll();
            return retorno;
        }

        private void Validate(Musician musician, bool update = false)
        {
            if (string.IsNullOrEmpty(musician.name))
            {
                throw ValidateException("Insira o seu nome");
            }

            if (musician.name.Length > 100)
            {
                throw ValidateException("O tamanho máximo do nome é 100 caracteres");
            }

            if (string.IsNullOrEmpty(musician.email))
            {
                throw ValidateException("Insira o seu email");
            }

            if (string.IsNullOrEmpty(musician.password))
            {
                throw ValidateException("Insira sua senha");
            }

            if (!update)
                VerifyIfEmailExists(musician.email);

            if (musician.password.Length > 40)
            {
                throw ValidateException("O tamanho máximo da senha é 40 caracteres");
            }
        }

        public IEnumerable<Musician> GetByMusicalProjectId(int musical_project_id)
        {
            ContributionBusiness contributionBusiness = new ContributionBusiness();
            var contributions = contributionBusiness.GetByMusicalProjectId(musical_project_id);

            IEnumerable<int> musician_ids = contributions.Select(c => c.musician_id);

            MusicianRepository musicianRepository = new MusicianRepository();
            IEnumerable<Musician> retorno = musicianRepository.GetMusicians(musician_ids);

            return retorno;
        }
    }
}