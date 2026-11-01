using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class BackupManager
    {
        private BackupMapper mapper = BackupMapper.Instance;
        private const string DATE_FORMAT_FOR_FILE = "yyyy_MM_dd_HH_mm_ss";

        public List<Backup> Obtener()
        {
            return mapper.Obtener();
        }

        public void HacerBackup(Backup backup)
        {
            string rutaBackup = ConfigurationManager.AppSettings.Get("ubicacionBackups");
            Directory.CreateDirectory(rutaBackup); // Si la carpeta no existe, la crea

            string nombreArchivo = DateTime.Now.ToString(DATE_FORMAT_FOR_FILE);
            mapper.HacerBackup(rutaBackup + nombreArchivo + ".bak");
            backup.NombreArchivo = nombreArchivo;
            mapper.Alta(backup);

        }
    }
}
