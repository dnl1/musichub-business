using BearerAuthentication;
using MusicHubBusiness.Amazon;
using MusicHubBusiness.Audio;
using MusicHubBusiness.Models;
using MusicHubBusiness.Repository;
using System;
using System.IO;
using System.Web;

namespace MusicHubBusiness.Business
{
    public class MusicalProjectInstrumentBusiness : BusinessBase
    {
        public MusicalProjectInstrument Create(MusicalProjectInstrument musicalProjectInstrument)
        {
            Validate(musicalProjectInstrument);

            MusicalProjectInstrumentRepository musicalProjectInstrumentRepository = new MusicalProjectInstrumentRepository();
            var retorno = musicalProjectInstrumentRepository.Create(musicalProjectInstrument);

            return retorno;
        }

        private void Validate(MusicalProjectInstrument musicalProjectInstrument)
        {
            if (musicalProjectInstrument.instrument_id == 0)
            {
                throw ValidateException("Insira o id do instrumento!");
            }

            if (musicalProjectInstrument.musical_project_id == 0)
            {
                throw ValidateException("Insira o id do projeto!");
            }
            if (!VerifyIfMusicalProjectExists(musicalProjectInstrument.musical_project_id))
            {
                throw ValidateException("Insira um Id do projeto musical válido!");
            }
        }

        private bool VerifyIfMusicalProjectExists(int musical_project_id)
        {
            MusicalProjectRepository musicalProjectRepository = new MusicalProjectRepository();
            var retorno = musicalProjectRepository.Get(musical_project_id);

            return retorno != null;
        }

        public void SaveAudio(string audioPath, string folderSave, int idBaseMusicalProjectInstrument)
        {
            string saveFilePath = Path.Combine(folderSave, $"{idBaseMusicalProjectInstrument.ToString()}.mp3");
            File.Copy(audioPath, saveFilePath);
        }
    }
}