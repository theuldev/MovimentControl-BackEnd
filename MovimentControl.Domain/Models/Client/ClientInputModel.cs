using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovimentControl.Domain.Models.Client
{
    public class ClientInputModel
    {
        public int? Id { get; set; }
        public string? FullName { get; set; }
        public string? City { get; set; }
        public string? CEP { get; set; }
        public string? District { get; set; }
        public string? State { get; set; }
        public string? Address { get; set; }
        public string? CPF { get; set; }
        public string? RG { get; set; }
        public int? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Career { get; set; }
        public string? Email { get; set; }
        public int? Service { get; set; }
        public string? Details { get; set; }
        public int? HealthI { get; set; }

        public int? MCivil { get; set; }
    }
}
