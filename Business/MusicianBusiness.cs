using MusicHubBusiness.Models;
using MusicHubBusiness.Repository;
using System.Collections.Generic;

namespace MusicHubBusiness.Business
{
    public class MusicianBusiness : BusinessBase
    {
        private MusicianRepository musicianRepository;

        public MusicianBusiness()
        {
            musicianRepository = new MusicianRepository();
        }

        public Musician Create(Musician musician)
        {
            Validate(musician);

            var retorno = musicianRepository.Create(musician);

            return retorno;
        }

        public Musician Login(string email, string password)
        {
            var retorno = musicianRepository.Login(email, password);

            return retorno;
        }

        public IEnumerable<Musician> SearchByName(string name)
        {
            var retorno = musicianRepository.SearchByName(name);

            return retorno;
        }

        public Musician Update(Musician musician)
        {
            if(musician.id == 0) throw ValidateException("O Id deve ser preenchido!");
            Validate(musician);

            var retorno = musicianRepository.Update(musician);

            return retorno;
        }

        private void VerifyIfEmailExists(string email)
        {
            var retorno = musicianRepository.GetByEmail(email);

            if (retorno != null) throw ValidateException("O e-mail está em uso!");
        }

        private void Validate(Musician musician)
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

            VerifyIfEmailExists(musician.email);

            if (musician.password.Length > 40)
            {
                throw ValidateException("O tamanho máximo da senha é 40 caracteres");
            }
        }
    }
}