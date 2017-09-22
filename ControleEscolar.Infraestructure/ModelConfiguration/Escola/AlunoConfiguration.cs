using ControleEscolar.Entities.Escola;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ControleEscolar.Infraestructure.ModelConfiguration.Seguranca
{
    internal class AlunoConfiguration : EntityTypeConfiguration<Aluno>
    {
        public AlunoConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tbAlunos");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Nome).HasColumnName("Nome").IsRequired();
            Property(x => x.NomePai).HasColumnName("NomePai");
            Property(x => x.NomeMae).HasColumnName("NomeMae").IsRequired();
            Property(x => x.RG).HasColumnName("RG");
            Property(x => x.CPF).HasColumnName("CPF");
            Property(x => x.DataNascimento).HasColumnName("DataNascimento").IsRequired();
           
            // Endereco Aluno
            Property(x => x.LogradouroAluno).HasColumnName("LogradouroAluno");
            Property(x => x.NumeroAluno).HasColumnName("NumeroAluno");
            Property(x => x.CEPAluno).HasColumnName("CEPAluno");
            Property(x => x.ComplementoAluno).HasColumnName("ComplementoAluno");
            Property(x => x.BairroAluno).HasColumnName("BairroAluno");
            Property(x => x.IdEstadoAluno).HasColumnName("IdEstadoAluno");
            Property(x => x.IdMunicipioAluno).HasColumnName("IdMunicipioAluno");

            // Responsavel
            Property(x => x.NomeResponsavel).HasColumnName("NomeResponsavel").IsRequired();
            Property(x => x.CPFResponsavel).HasColumnName("CPFResponsavel");
            Property(x => x.RGResponsavel).HasColumnName("RGResponsavel");
            Property(x => x.RGResponsavel).HasColumnName("EmailResponsavel");
            Property(x => x.OpcaoPagamento).HasColumnName("OpcaoPagamento");
            Property(x => x.PercentualDesconto).HasColumnName("PercentualDesconto");

            // Endereco Responsavel
            Property(x => x.LogradouroResponsavel).HasColumnName("LogradouroResponsavel");
            Property(x => x.NumeroResponsavel).HasColumnName("NumeroResponsavel");
            Property(x => x.CEPResponsavel).HasColumnName("CEPResponsavel");
            Property(x => x.ComplementoResponsavel).HasColumnName("ComplementoResponsavel");
            Property(x => x.BairroResponsavel).HasColumnName("BairroResponsavel");
            Property(x => x.TelefoneResponsavel).HasColumnName("TelefoneResponsavel");
            Property(x => x.IdEstadoResponsavel).HasColumnName("IdEstadoResponsavel");
            Property(x => x.IdMunicipioResponsavel).HasColumnName("IdMunicipioResponsavel");
            
            
        }
    }
}  

