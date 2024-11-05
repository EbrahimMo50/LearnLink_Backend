using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnLink_Backend.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Student
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Salt { get; set; }
        public string HashedPassword { get; set; }
        public decimal Balance { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public string SpokenLanguage { get; set; }
        public string Address { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime AtDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
