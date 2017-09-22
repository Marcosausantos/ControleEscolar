using ControleEscolar.Entities.Escola;
using ControleEscolar.Entities.Seguranca;
using ControleEscolar.Infraestructure.Context;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace ControleEscolar.Infraestructure.Db
{
    public class ControleEscolarDbInitializer : DropCreateDatabaseAlways<ControleEscolarDbContext>
    {
        Usuario usuario;

        protected override void Seed(ControleEscolarDbContext context)
        {
            SeedSeguranca(context);

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var text = ex.Message;
                throw;
            }

            base.Seed(context);
        }        

        private void SeedSeguranca(ControleEscolarDbContext context)
        {
            //Usuário
            usuario = new Usuario() {
                UserName = "sysdba",
                Email = "sysdba@gmail.com.br",
                PasswordHash = "ANlmxQimWCroMSUX3Lh2d/8uuDCq6hLQz0YKQTuyOPoQu3fr7Qh8jbikOoKBPTbPQA==",
                SecurityStamp = "9738603c-15b6-4b8e-b7c2-ed38c207167c",
                Inativo = false
            };
            context.Users.Add(usuario);

            var pais = new Pais() { Id = 1, Nome = "Brasil" };
            context.Pais.Add(pais);

            var estadoCE = new Estado() { Id = 1, Nome = "Ceará", Pais = pais, Sigla = "CE" };
            context.Estados.Add(estadoCE);
            context.Estados.Add(new Estado() { Id = 2, Nome = "Paraná", Pais = pais, Sigla = "PR" });

            var municipioFortaleza = new Municipio() { Id = 1, Nome = "Fortaleza", Estado = estadoCE };
            context.Municipios.Add(municipioFortaleza);

            var aluno = new Aluno() {
                Id = 1,
                Nome = "Marcos",
                NomePai = "Sr Marcos",
                NomeMae = "Sra Marcos",
                CPF = "000.000.000-00",
                RG = "000.000.000-0",
                DataNascimento = DateTime.Now.Date,
                Inativo = "N",
                Usuario = usuario,
                //Endereco
                LogradouroAluno = "LogradouroAluno",
                NumeroAluno = "NumeroAluno",
                CEPAluno = "CEPAluno",
                ComplementoAluno = "ComplementoAluno",
                BairroAluno = "BairroAluno",
                TelefoneAluno = "85 0000-0000",
                MunicipioAluno = municipioFortaleza,
                EstadoAluno = estadoCE,
                //Responsável
                NomeResponsavel = "NomeResponsavel",
                RGResponsavel = "RGResponsavel",
                CPFResponsavel = "CPFResponsavel",
                OpcaoPagamento = "B",
                PercentualDesconto = 10,
                //Endereco
                LogradouroResponsavel = "LogradouroResponsavel",
                NumeroResponsavel = "NumeroResponsavel",
                CEPResponsavel = "CEPResponsavel",
                ComplementoResponsavel = "ComplementoResponsavel",
                BairroResponsavel = "BairroResponsavel",
                TelefoneResponsavel = "85 0000-0000",
                MunicipioResponsavel = municipioFortaleza,
                EstadoResponsavel = estadoCE 
            };

            context.Alunos.Add(aluno);

            var aluno2 = new Aluno()
            {
                Id = 1,
                Nome = "Marcos 2",
                NomePai = "Sr Marcos2",
                NomeMae = "Sra Marcos2",
                CPF = "000.000.000-00",
                RG = "000.000.000-0",
                DataNascimento = DateTime.Now.Date,
                Inativo = "N",
                Usuario = usuario,
                //Endereco
                LogradouroAluno = "LogradouroAluno",
                NumeroAluno = "NumeroAluno",
                CEPAluno = "CEPAluno",
                ComplementoAluno = "ComplementoAluno",
                BairroAluno = "BairroAluno",
                TelefoneAluno = "85 0000-0000",
                MunicipioAluno = municipioFortaleza,
                EstadoAluno = estadoCE,
                //Responsável
                NomeResponsavel = "NomeResponsavel",
                RGResponsavel = "RGResponsavel",
                CPFResponsavel = "CPFResponsavel",
                OpcaoPagamento = "B",
                PercentualDesconto = 10,
                //Endereco
                LogradouroResponsavel = "LogradouroResponsavel",
                NumeroResponsavel = "NumeroResponsavel",
                CEPResponsavel = "CEPResponsavel",
                ComplementoResponsavel = "ComplementoResponsavel",
                BairroResponsavel = "BairroResponsavel",
                TelefoneResponsavel = "85 0000-0000",
                MunicipioResponsavel = municipioFortaleza,
                EstadoResponsavel = estadoCE
            };

            context.Alunos.Add(aluno2);

            var sensomec = new SensoMec() {
                Id = 1,
                Competencia = DateTime.Now.Date,
                DateGeracao = DateTime.Now.Date,
                Descricao = "Rotina SensoMEC",
                NumeroRotina = "1",
                Status = "Incluído"
            };
            context.SensoMecs.Add(sensomec);

            context.SensoMecItens.Add(new SensoMecItem() { Id = 1, Aluno = aluno, SensoMec = sensomec, Enviar = true });
            context.SensoMecItens.Add(new SensoMecItem() { Id = 2, Aluno = aluno2, SensoMec = sensomec, Enviar = true });
        }
    }
}


