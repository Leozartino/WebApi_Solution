using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_Project.Models
{
    public abstract class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Birth_date { get; set; }
        public string Email { get; set; }
        public Class Class { get; set; }
        public int ClassId { get; set; }

    }
}
