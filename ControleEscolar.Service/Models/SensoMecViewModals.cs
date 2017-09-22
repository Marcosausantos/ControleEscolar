using System;
using System.ComponentModel.DataAnnotations;

namespace ControleEscolar.Service.Models
{
    public class SensoMecViewModals
    {
        [Required]
        public string NomeAluno { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
    }
}