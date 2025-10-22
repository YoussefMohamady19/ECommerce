﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public record CustomerDto(int Id, string Name, string Email, string? Phone);
    public record CreateCustomerDto(string Name, string Email, string? Phone);
}
