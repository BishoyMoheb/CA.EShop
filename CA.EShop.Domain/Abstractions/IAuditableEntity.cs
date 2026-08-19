using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.EShop.Domain.Abstractions
{
    public interface IAuditableEntity
    {
        DateTime CreatedOnUTC { get; set; }

        DateTime? ModifiedOnUTC { get; set; }
    }
}
