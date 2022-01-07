using LearningApp.Interface;
using LearningApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningApp.Repository
{
    public class SQLEnglishWordRepositoy : IRepository
    {
        public List<WordsModel> GetWords()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WordsModel> GetWordsToMemoryGame()
        {
            throw new NotImplementedException();
        }

        public void LoadData(string path)
        {
            throw new NotImplementedException();
        }
    }
}
