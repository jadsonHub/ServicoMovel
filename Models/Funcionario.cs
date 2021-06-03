using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL.Models
{
    public class Funcionario
    {
        [Key]
        public int FuncionarioId { get; set; }
        public string NomeFuncionario { get; set; }
        public string StatusFuncionario { get; set; }
        public int Valor { get; set; }
        
        public string [] status = {"Indisponivel","disponivel"};
        public virtual ICollection<Movel> Moveis { get; set; }

    }
}