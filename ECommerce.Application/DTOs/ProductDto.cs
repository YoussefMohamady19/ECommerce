using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public record ProductDto(int Id, string Name, string? Description, double Price, int Stock);
}
