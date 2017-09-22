using ControleEscolar.Entities.Escola;
using ControleEscolar.Entities.Seguranca;
using ControleEscolar.Infraestructure.ModelConfiguration.Escola;
using ControleEscolar.Infraestructure.ModelConfiguration.Seguranca;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;

namespace ControleEscolar.Infraestructure.Context
{
    public class CustomUserStore : UserStore<Usuario, Role, int, UsuarioLogin, UsuarioRole, UsuarioClaim>
    {
        public CustomUserStore(ControleEscolarDbContext context)
            : base(context)
        {

        }
    }

    public class CustomRoleStore : RoleStore<Role, int, UsuarioRole>
    {
        public CustomRoleStore(ControleEscolarDbContext context)
            : base(context)
        {

        }
    }

    public class ControleEscolarDbContext : IdentityDbContext<Usuario, Role, int, UsuarioLogin, UsuarioRole, UsuarioClaim> 
    {
        public static ControleEscolarDbContext Create()
        {
            return new ControleEscolarDbContext();
        }

        //Sistema de segurança
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<SensoMec> SensoMecs { get; set; }
        public DbSet<SensoMecItem> SensoMecItens { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Municipio> Municipios { get; set; }

        public ControleEscolarDbContext()
            : base("Name=DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            this.Database.Log = x => Debug.Write(x);
        }

        public ControleEscolarDbContext(string name)
            : base(name)
        {
            Configuration.LazyLoadingEnabled = false;
            this.Database.Log = x => Debug.Write(x);
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AlunoConfiguration());
            modelBuilder.Configurations.Add(new SensoMecConfiguration());
            modelBuilder.Configurations.Add(new PaisConfiguration());
            modelBuilder.Configurations.Add(new MunicipioConfiguration());
            modelBuilder.Configurations.Add(new EstadoConfiguration());
            modelBuilder.Configurations.Add(new SensoMecItemConfiguration());
            
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            modelBuilder.Entity<Usuario>().ToTable("Sis_Usuarios");
            modelBuilder.Entity<Role>().ToTable("Sis_Roles");
            modelBuilder.Entity<UsuarioRole>().ToTable("Sis_UsuarioRoles");
            modelBuilder.Entity<UsuarioLogin>().ToTable("Sis_UsuarioLogins");
            modelBuilder.Entity<UsuarioClaim>().ToTable("Sis_UsuarioClaims");
        }        
    }
}
