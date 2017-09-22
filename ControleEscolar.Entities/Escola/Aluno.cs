using ControleEscolar.Entities.Entity;
using ControleEscolar.Entities.Seguranca;
using System;

namespace ControleEscolar.Entities.Escola
{
    public class Aluno : IEntityBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomePai { get; set; }
        public string NomeMae { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public int? IdUsuarioInclusao { get; set; }
        public Usuario Usuario { get; set; }
        public string Inativo { get; set; }

        //Endereco
        public string LogradouroAluno { get; set; }
        public string NumeroAluno { get; set; }
        public string CEPAluno { get; set; }
        public string ComplementoAluno { get; set; }
        public string BairroAluno { get; set; }
        public string TelefoneAluno { get; set; }
        public int IdMunicipioAluno { get; set; }
        public Municipio MunicipioAluno { get; set; }
        public int IdEstadoAluno { get; set; }
        public Estado EstadoAluno { get; set; }

        //Responsável
        public string NomeResponsavel { get; set; }
        public string RGResponsavel { get; set; }
        public string CPFResponsavel { get; set; }
        public string EmailResponsavel { get; set; }
        public string OpcaoPagamento { get; set; }
        public float PercentualDesconto { get; set; }

        //Endereco
        public string LogradouroResponsavel { get; set; }
        public string NumeroResponsavel { get; set; }
        public string CEPResponsavel { get; set; }
        public string ComplementoResponsavel { get; set; }
        public string BairroResponsavel { get; set; }
        public string TelefoneResponsavel { get; set; }
        public int IdMunicipioResponsavel { get; set; }
        public Municipio MunicipioResponsavel { get; set; }
        public int IdEstadoResponsavel { get; set; }
        public Estado EstadoResponsavel { get; set; }
    }
}
