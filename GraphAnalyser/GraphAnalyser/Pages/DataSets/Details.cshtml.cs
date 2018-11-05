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
    public class DetailsModel : PageModel
    {
        private readonly DataSetContext _context;

        public DetailsModel(DataSetContext context)
        {
            _context = context;
        }

        public DataSet DataSet { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DataSet = await _context.DataSet.FirstOrDefaultAsync(m => m.ID == id);

            if (DataSet == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
