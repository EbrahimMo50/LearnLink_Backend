﻿using LearnLink_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.Entities
{
    //a set up meeting between stuent and instructor
    public class MeetingModel
    {
        public int Id { get; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public string InstructorId { get; set; }
        public Instructor Instructor { get; set; }
        [Range(0, 6)]
        public int Day { get; set; }
        [Range(0, 24)]
        public int StartsAt { get; set; }
        [Range(0, 24)]
        public int EndsAt { get; set; }
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
