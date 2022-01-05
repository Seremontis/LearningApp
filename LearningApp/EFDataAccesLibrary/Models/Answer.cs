using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccesLibrary.Models
{
    public class Answer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Answerr { get; set; }

        [Required]
        public bool IsCorrect { get; set; }
    }
}
