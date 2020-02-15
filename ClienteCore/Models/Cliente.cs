using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteCore.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome do cliente", AllowEmptyStrings = false)]
        public string NomeCliente { get; set; }

        [Display(Name = "E-Mail")]
        [Required(ErrorMessage = "Informe o e-mail do cliente", AllowEmptyStrings = false)]
        [RegularExpression(
            @"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
            ErrorMessage = "Informe um email válido.")]
        public string EmailCliente { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "Informe o cpf do cliente", AllowEmptyStrings = false)]
        public string CpfCliente { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "Informe a data de nascimento do cliente", AllowEmptyStrings = false)]
        public DateTime DtNascCliente { get; set; }

        public bool Ativo { get; set; }
    }
}