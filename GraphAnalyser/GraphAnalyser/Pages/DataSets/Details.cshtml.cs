using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain;
using Services;

namespace GraphAnalyser.Pages.DataSets
{
    public class DetailsModel : PageModel
    {
        private readonly DataSetContext _context;
        private readonly IDataSetService dataSetService;

        public DetailsModel(DataSetContext context, IDataSetService dataSetService)
        {
            _context = context;
            this.dataSetService = dataSetService;
        }

        public string JsonData { get; set; }

        public int Average { get; set; }

        public DataSet DataSet { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DataSet = await _context.DataSet
                .Include(ds => ds.Users)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (DataSet == null)
            {
                return NotFound();
            }

            Average = DataSet.Users.Sum(u => u.NumberOfFriends) / DataSet.NumberOfUsers;
            JsonData = await dataSetService.DataSetToJson(DataSet);
            return Page();
        }
    }
}
