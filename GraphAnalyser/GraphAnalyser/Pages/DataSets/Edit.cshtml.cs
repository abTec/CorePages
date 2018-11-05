using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using GraphAnalyser.Models;

namespace GraphAnalyser.Pages.DataSets
{
    public class EditModel : PageModel
    {
        private readonly GraphAnalyser.Models.DataSetContext _context;

        public EditModel(GraphAnalyser.Models.DataSetContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DataSet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataSetExists(DataSet.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DataSetExists(int id)
        {
            return _context.DataSet.Any(e => e.ID == id);
        }
    }
}
