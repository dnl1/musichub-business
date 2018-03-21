using MusicHubBusiness.Models;
using MusicHubBusiness.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHubBusiness.Business
{
    public class InstrumentBusiness : BusinessBase
    {
        public IEnumerable<Instrument> GetAll()
        {
            InstrumentRepository instrumentRepository = new InstrumentRepository();
            var retorno = instrumentRepository.GetAll();

            return retorno;
        }
    }
}
