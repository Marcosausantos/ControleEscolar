using Jarvis.Entities.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Entities.Seguranca
{    
    public class Permissao : IEntityBase
    {
        public int Id { get; set; }
        public int? GrupoId { get; set; }
        public int? MenuId { get; set; }
        public bool Padrao { get; set; }
        public bool Incluir { get; set; }
        public bool Editar { get; set; }
        public bool Excluir { get; set; }

        public Grupo Grupo { get; set; }
        public Menu Menu { get; set; }
    }
}
