using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Importer.Model
{
    public class WordsModel
    {
        public string EnglishMeaning { get; set; }
        public string PolishMeaning { get; set; }
        public string Type { get; set; }
        public string Level { get; set; }
        public Uri ImageUri { get; set; }
    }
}
