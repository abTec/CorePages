using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain;
using GraphAnalyser.Models;

namespace GraphAnalyser.Pages.DataSets
{
    public class CreateModel : PageModel
    {
        private readonly GraphAnalyser.Models.DataSetContext _context;

        public CreateModel(GraphAnalyser.Models.DataSetContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DataSet DataSet { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.DataSet.Add(DataSet);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}