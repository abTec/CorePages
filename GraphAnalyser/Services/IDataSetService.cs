using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Services
{
    public interface IDataSetService
    {
        Task<DataSet> CreateDataSetAsync(Stream data);
    }
}
