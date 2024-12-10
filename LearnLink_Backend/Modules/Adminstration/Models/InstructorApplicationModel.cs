namespace LearnLink_Backend.Modules.Adminstration.Models
{
    public class InstructorApplicationModel
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Messsage { get; set; }
        public string Nationality { get; set; }
        public string SpokenLanguage { get; set; }
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? UpdatedBy { get; set; }
    }
}