using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class FileHelper : IFileHelper
    {
        public string ComputeHash(Stream fileStream)
        {
            var hash = fileStream.ComputeHash();
            return hash;
        }

    }
}
