using System;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataSetRepository : IDataSetRepository
    {
        private readonly DataSetContext context;

        public DataSetRepository(DataSetContext context)
        {
            this.context = context;
        }

        public async Task<DataSet> HashExists(string hash) => await context.DataSet.FirstOrDefaultAsync(ds => ds.Hash == hash);

        public async Task<int> InsertDataSet(DataSet dataSet)
        {
            dataSet.InseretedDateUtc = DateTime.UtcNow;
            context.DataSet.Add(dataSet);
            await context.SaveChangesAsync();

            return dataSet.ID;
        }
    }
}
