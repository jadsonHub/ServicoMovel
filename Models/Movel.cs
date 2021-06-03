using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL.Models
{
    public class Movel
    {
        [Key]
        public int MovelId { get; set; }
        public string Nome { get; set; }
        public string Cor { get; set; }
        public string Material { get; set; }
        public string Medidas { get; set; }
        public string Link { get; set; }
        public string Status { get; set; }
        public int FuncionarioId {get; set;}       
        public  virtual Funcionario Funcionarios { get; set; }
 
    }
}