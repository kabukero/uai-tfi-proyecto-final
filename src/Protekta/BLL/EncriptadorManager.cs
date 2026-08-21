using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EncriptadorManager
    {
        private const string _salt = "b5N3pWkRwzJH";

        public string Encriptar(string texto)
        {
            byte[] data = Encoding.ASCII.GetBytes(texto + _salt);
            data = new SHA256Managed().ComputeHash(data);
            return Convert.ToBase64String(data);
        }
    }
}
