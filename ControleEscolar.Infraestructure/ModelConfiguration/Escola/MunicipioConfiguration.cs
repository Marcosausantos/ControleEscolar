using ControleEscolar.Entities.Escola;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ControleEscolar.Infraestructure.ModelConfiguration.Escola
{
    internal class MunicipioConfiguration : EntityTypeConfiguration<Municipio>
    {
        public MunicipioConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tbMunicipios");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Nome).HasColumnName("Nome").IsRequired();
            Property(x => x.EstadoId).HasColumnName("EstadoId").IsRequired();            
        }
    }
}  

