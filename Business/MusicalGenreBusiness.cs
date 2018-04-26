using MusicHubBusiness.Models;
using MusicHubBusiness.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHubBusiness.Business
{
    public class MusicalGenreBusiness : BusinessBase
    {
        public IEnumerable<MusicalGenre> GetAll()
        {
            MusicalGenreRepository musicalGenreRepository = new MusicalGenreRepository();
            var retorno = musicalGenreRepository.GetAll();

            return retorno;
        }

        public MusicalGenre Get(int id)
        {
            MusicalGenreRepository musicalGenreRepository = new MusicalGenreRepository();
            var retorno = musicalGenreRepository.Get(id);

            return retorno;
        }
    }
}
