﻿using LearnLink_Backend.Entities;
using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.DTOs
{
    public class ApplicationSet
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(6)]
        public string Password { get; set; }
        [MinLength(10)]
        public string Messsage { get; set; }
        [MinLength(4)]
        public string Nationality { get; set; }
        [MinLength(4)]
        public string SpokenLanguage { get; set; }
    }
    public class ApplicationGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Messsage { get; set; }
        public string Nationality { get; set; }
        public string SpokenLanguage { get; set; }
        public static ApplicationGet ToDTO(ApplicationModel app)
        {
            return new ApplicationGet { Id = app.Id, Name = app.Name, Email = app.Email, Messsage = app.Messsage, Nationality = app.Nationality, SpokenLanguage = app.Nationality };
        }

        public static IEnumerable<ApplicationGet> ToDTO(IEnumerable<ApplicationModel> app)
        {
            var result = new List<ApplicationGet>();

            foreach (var appItem in app)
                result.Add(ToDTO(appItem));

            return result;
        }
    }
}
