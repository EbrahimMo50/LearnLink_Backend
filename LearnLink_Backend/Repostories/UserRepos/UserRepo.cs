using LearnLink_Backend.DTOs;
using LearnLink_Backend.DTOs.InstructorDTOs;
using LearnLink_Backend.DTOs.StudentDTOs;
using LearnLink_Backend.Models;
using LearnLink_Backend.Services;
using System;
using System.Security.Cryptography;
using System.Text;

namespace LearnLink_Backend.Repostories.UserRepos
{
    public class UserRepo() : IUserRepo
    {
        public void SignUp(StudentSet student)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var salt = new string(Enumerable.Repeat(chars, 25)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            Console.WriteLine(salt);
            var inputBytes = Encoding.UTF8.GetBytes(salt);
            var inputHash = SHA256.HashData(inputBytes);
            Console.WriteLine(Convert.ToHexString(inputHash));
        }
        public void ApplyForInstructor(InstructorAppDto instructorApp)
        {
            throw new NotImplementedException();
        }

        public void ChangePassword(LoginViewModel user, string newPass)
        {
            throw new NotImplementedException();
        }

        public void Login(LoginViewModel user)
        {
            throw new NotImplementedException();
        }
    }
}
