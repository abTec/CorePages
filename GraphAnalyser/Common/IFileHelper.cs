using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common
{
    public interface IFileHelper
    {
        string ComputeHash(Stream fileStream);
    }
}
