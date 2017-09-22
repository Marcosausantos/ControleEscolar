using ControleEscolar.Entities.Escola;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ControleEscolar.Infraestructure.ModelConfiguration.Escola
{
    internal class PaisConfiguration : EntityTypeConfiguration<Pais>
    {
        public PaisConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tbPais");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Nome).HasColumnName("Nome").IsRequired();       
        }
    }
}  

