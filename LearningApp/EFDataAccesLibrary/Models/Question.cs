using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccesLibrary.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Questionn { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
}
