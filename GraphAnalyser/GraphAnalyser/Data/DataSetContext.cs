using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace GraphAnalyser.Models
{
    public class DataSetContext : DbContext
    {
        public DataSetContext (DbContextOptions<DataSetContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.DataSet> DataSet { get; set; }
    }
}
