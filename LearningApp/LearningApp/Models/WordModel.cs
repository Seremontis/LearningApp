using System;

namespace LearningApp.Models
{
    public class WordsModel
    {
        public int Id { get; set; }
        public string EnglishMeaning { get; set; }
        public string PolishMeaning { get; set; }
        public string Type { get; set; }
        public string Level { get; set; }
        public Uri ImageUri { get; set; }
    }
}
