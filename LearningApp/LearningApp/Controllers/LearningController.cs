using EFDataAccesLibrary.DataAcces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningApp.Controllers
{
    public class LearningController : BaseController
    {
        private readonly QuestionContext _db;

        public LearningController(QuestionContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var test =_db.Questions.ToList();

            return View();
        }


    }
}
