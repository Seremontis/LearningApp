using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningApp.ViewModel
{
    public class QuizModel
    {
        public string Question { get; set; }
        public string Type { get; set; }
        public string Answerr { get; set; }

        public bool IsCorrect { get; set; }

    }
}
