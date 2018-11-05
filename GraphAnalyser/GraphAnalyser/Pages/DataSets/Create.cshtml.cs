using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain;
using Microsoft.AspNetCore.Http;
using Services;

namespace GraphAnalyser.Pages.DataSets
{
    public class CreateModel : PageModel
    {
        private readonly IDataSetService dataSetService;
        private readonly IDataSetRepository dataSetRepository;

        public CreateModel(IDataSetService dataSetService, IDataSetRepository dataSetRepository)
        {
            this.dataSetService = dataSetService;
            this.dataSetRepository = dataSetRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DataSet DataSet { get; set; }


        [Required]
        [Display(Name = "Input Data File")]
        public IFormFile InputFile { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                using (var stream = InputFile.OpenReadStream())
                {
                    DataSet = await dataSetService.CreateDataSetAsync(stream);
                    await dataSetRepository.InsertDataSet(DataSet);
                    return RedirectToPage("./Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return Page();
            }
        }
    }
}