using LearningApp.Interface;
using LearningApp.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LearningApp.Repository
{
    public class ExcelEnglishWordRepositoy : IRepository
    {
        private List<WordsModel> _models = new List<WordsModel>();
        public void LoadData(string pathToFile)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(pathToFile)))
            {
                foreach (var worksheet in xlPackage.Workbook.Worksheets)
                {
                    if (worksheet.Name.Contains("English Words"))
                    {
                        for (int x = 2; x <= worksheet.Dimension.End.Row; x++)
                        {
                            _models.Add(new WordsModel()
                            {
                                Id=x-1,
                                EnglishMeaning = worksheet.Cells[x, 1].Value?.ToString(),
                                PolishMeaning = worksheet.Cells[x, 2].Value?.ToString(),
                                Type = worksheet.Cells[x, 3].Value?.ToString(),
                                Level = worksheet.Cells[x, 4].Value?.ToString(),
                                ImageUri = null,
                            });
                        }
                    }
                }
            }
        }

        public List<WordsModel> GetWords()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WordsModel> GetWordsToMemoryGame()
        {
            int[] wordsIndex = new int[10];
            int id;
            Random rand=new Random();
            for (int i = 0; i < wordsIndex.Count(); i++)
            {
                do
                {
                    id = rand.Next(0, _models.Count - 1);
                } while (wordsIndex.Contains(id));
                wordsIndex[i] = id;
            }
            foreach (int index in wordsIndex)
                yield return _models.ElementAt(index);   
        }

    }
}
