using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.API.SQL.Integrations.Models
{
    public partial class IntegrationSystem
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public bool? Active { get; set; }

        public virtual ICollection<IntegrationCatalog> IntegrationCatalogs { get; set; } = new List<IntegrationCatalog>();
    }
}
