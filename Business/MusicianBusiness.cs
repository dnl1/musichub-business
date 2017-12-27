using MusicHubBusiness.Models;
using MusicHubBusiness.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHubBusiness.Business
{
    public class MusicianBusiness
    {
        public Musician Create(Musician musician)
        {
            MusicianRepository musicianRepository = new MusicianRepository();
            var retorno = musicianRepository.Create(musician);

            return retorno;
        }
    }
}
