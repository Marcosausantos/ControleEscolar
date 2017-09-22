using ControleEscolar.Entities.Escola;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ControleEscolar.Infraestructure.ModelConfiguration.Seguranca
{
    internal class SensoMecConfiguration : EntityTypeConfiguration<SensoMec>
    {
        public SensoMecConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tbSensoMec");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.NumeroRotina).HasColumnName("NumeroRotina").IsRequired();
            Property(x => x.Competencia).HasColumnName("Competencia").IsRequired();
            Property(x => x.Descricao).HasColumnName("Descricao").IsRequired();
            Property(x => x.DateGeracao).HasColumnName("DateGeracao").IsRequired();
            Property(x => x.DataEnvio).HasColumnName("DataEnvio");
            Property(x => x.NumeroProtocoloRetorno).HasColumnName("NumeroProtocoloRetorno");
        }
    }
}  

