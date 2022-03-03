namespace WebApi_Project.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Birth_date { get; set; }
        public string Email { get; set; }
        public Class Class { get; set; }
        public int ClassId { get; set; }

    }
}
