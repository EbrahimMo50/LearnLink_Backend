using LearnLink_Backend.DTOs;
using LearnLink_Backend.DTOs.InstructorDTOs;
using LearnLink_Backend.DTOs.StudentDTOs;

namespace LearnLink_Backend.Repostories.UserRepos
{
    public interface IUserRepo
    {
        public void SignUp(StudentSet student);
        public void Login(LoginViewModel user);
        public void ChangePassword(LoginViewModel user, string newPass); //security wise this is so down bad the data are passed in the headers
        public void ApplyForInstructor(InstructorAppDto instructorApp);
    }
}
