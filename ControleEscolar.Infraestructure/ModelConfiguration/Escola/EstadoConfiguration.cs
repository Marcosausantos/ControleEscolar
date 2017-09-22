using ControleEscolar.Entities.Escola;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ControleEscolar.Infraestructure.ModelConfiguration.Escola
{
    internal class EstadoConfiguration : EntityTypeConfiguration<Estado>
    {
        public EstadoConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tbEstados");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Nome).HasColumnName("Nome").IsRequired();
            Property(x => x.Sigla).HasColumnName("Sigla").IsRequired();
            Property(x => x.PaisId).HasColumnName("PaisId").IsRequired(); 
        }
    }
}  

