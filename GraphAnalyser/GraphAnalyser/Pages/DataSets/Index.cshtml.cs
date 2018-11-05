using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace GraphAnalyser.Pages.DataSets
{
    public class IndexModel : PageModel
    {
        private readonly DataSetContext _context;

        public IndexModel(DataSetContext context)
        {
            _context = context;
        }

        public IList<DataSet> DataSet { get;set; }

        public async Task OnGetAsync()
        {
            DataSet = await _context.DataSet.ToListAsync();
        }
    }
}
