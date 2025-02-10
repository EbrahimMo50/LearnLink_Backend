using LearnLink_Backend.Models;

namespace LearnLink_Backend.Repositories.UserMangementRepo
{
    public interface IUserRepo
    {
        public Student AddStudent(Student student);
        public Instructor AddInstructor(Instructor instructor);
        public Admin AddAdmin(Admin admin);
        public Student? GetStudentById(string id);
        public Instructor? GetInstructorById(string id);
        public Admin? GetAdminById(string id);
        public Student? GetStudentByEmail(string email);
        public Instructor? GetInstructorByEmail(string email);
        public Admin? GetAdminByEmail(string email);
        public void DeleteStudent(string id);
        public void DeleteInstructor(string id);
        public IEnumerable<Student> GetStudents(List<string>? Ids);
        public IEnumerable<Instructor> GetInstructors(List<string>? Ids);
        public Student UpdateStudent(Student student);
        public Instructor UpdateInstructor(Instructor instructor);
    }
}
