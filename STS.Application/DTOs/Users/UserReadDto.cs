using STS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS.Application.DTOs.Users
{
    public class UserReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }// kullanicinin hangi sirkette oldugunu gormek istedgimizden

        public int CompanyId { get; set; }
    }
}
