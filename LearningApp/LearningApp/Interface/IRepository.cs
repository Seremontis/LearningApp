using LearningApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningApp.Interface
{
    public interface IRepository
    {
        void LoadData(string path);
        IEnumerable<WordsModel> GetWordsToMemoryGame();
        List<WordsModel> GetWords();
    }
}
