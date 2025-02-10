namespace LearnLink_Backend.Entities
{
    public class AnnouncementModel
    {
        public int Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }
        public CourseModel Course { get; set; }
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
