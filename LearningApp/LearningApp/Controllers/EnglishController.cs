using LearningApp.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningApp.Controllers
{
    public class EnglishController : Controller
    {
        private IRepository repository;
        private const string excelLocalization = @"C:\Baza_pytan.xlsx";

        public EnglishController(IRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            repository.LoadData(excelLocalization);
            var test=repository.GetWordsToMemoryGame().ToList();
            return View(test);
        }
    }
}
