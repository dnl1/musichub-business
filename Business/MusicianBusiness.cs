using MusicHubBusiness.Models;
using MusicHubBusiness.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHubBusiness.Business
{
    public class MusicianBusiness:BusinessBase
    {
        public Musician Create(Musician musician)
        {
            Validar(musician);

            MusicianRepository musicianRepository = new MusicianRepository();
            var retorno = musicianRepository.Create(musician);

            return retorno;
        }

        public Musician Login(string email, string password)
        {
            MusicianRepository musicianRepository = new MusicianRepository();
            var retorno = musicianRepository.Login(email, password);

            return retorno;
        }

        private void VerificaEmailExiste(string email)
        {
            MusicianRepository musicianRepository = new MusicianRepository();
            var retorno = musicianRepository.GetByEmail(email);

            if (retorno != null) throw ValidateException("O e-mail está em uso!");
        }

        private void Validar(Musician musician)
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

            VerificaEmailExiste(musician.email);

            if (musician.password.Length > 40)
            {
                throw ValidateException("O tamanho máximo da senha é 40 caracteres");
            }
        }
    }
}
