using EFDataAccesLibrary.DataAcces;
using Extensions;
using LearningApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningApp.Controllers
{
    public class LearningController : BaseController
    {
        private readonly QuestionContext _db;
        private const string excelLocalization = @"D:\Baza_pytan.xlsx";
        public LearningController(QuestionContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var vieModel = new List<QuizModel>();
            //var test =_db.Questions.ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(excelLocalization)))
            {
                foreach (var myWorksheet in xlPackage.Workbook.Worksheets)
                {
                    if (myWorksheet.Name.Contains("MainQuestion"))
                        continue;
                    var totalRows = myWorksheet.Dimension.End.Row;
                    var totalColumns = myWorksheet.Dimension.End.Column;

                    var sb = new StringBuilder();
                    for (int rowNum = 2; rowNum <= totalRows; rowNum++)
                    {
                        var row = myWorksheet.Cells[rowNum, 1, rowNum, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString()).ToList();

                        if (!string.IsNullOrEmpty(row[1])) //&& row[1].ToLower().Contains("c#"))
                        {
                            vieModel.Add(new QuizModel()
                            {
                                Question = row[0],
                                Type = row[1],
                                Answerr = row[3],
                                IsCorrect = bool.Parse(row[4])
                            });
                        }
                    }
                }
            }
            
            vieModel.Shuffle();

            var groupedViewModel = vieModel.GroupBy(x => x.Question).ToDictionary(x => x.Key, x => x.ToList());

            foreach (var item in groupedViewModel)
                item.Value.Shuffle();

            return View(RandomDictionary(groupedViewModel));
        }

        public Dictionary<string, List<QuizModel>> RandomDictionary(Dictionary<string, List<QuizModel>> quizModelNotSorted)
        {
            var randomDictionary = new Dictionary<string, List<QuizModel>>();
            List<string> keyList = new List<string>(quizModelNotSorted.Keys);
            Random rand = new Random();
            
            for (int i = 0; i < 10; i++)
            {
                string randomKey = keyList[rand.Next(keyList.Count)];
                keyList.Remove(randomKey);
                randomDictionary.Add(randomKey, quizModelNotSorted[randomKey]);
            }
            return randomDictionary;
        }
    }
}
