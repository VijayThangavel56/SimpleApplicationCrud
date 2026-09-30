using System;
using System.ComponentModel.DataAnnotations;

namespace SampleApplication.Server.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        [MaxLength(200)]
        public string? Email { get; set; }

        public DateTime DateOfJoining { get; set; }

        [MaxLength(100)]
        public string? Position { get; set; }
    }
}
