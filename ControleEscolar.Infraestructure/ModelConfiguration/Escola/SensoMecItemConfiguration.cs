using ControleEscolar.Entities.Escola;
using System.Data.Entity.ModelConfiguration;

namespace ControleEscolar.Infraestructure.ModelConfiguration.Seguranca
{
    internal class SensoMecItemConfiguration : EntityTypeConfiguration<SensoMecItem>
    {
        public SensoMecItemConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tbSensoMecItem");
            HasKey(x => x.Id);
            HasRequired(x => x.Aluno);
        }
    }
}  

