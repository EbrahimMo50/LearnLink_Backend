using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Repositories.AnnouncementsRepo;
using LearnLink_Backend.Repositories.CoursesRepo;

namespace LearnLink_Backend.Services.AnnouncementsService
{
    public class AnnouncementService(IAnnouncementRepo announcementRepo, ICourseRepo courseRepo) : IAnnouncementService
    {
        public async Task<AnnouncementModel> CreateAnnouncementAsync(AnnouncementSet announcement, int courseId, string createrId)
        {
            CourseModel? course = await courseRepo.GetByIdAsync(courseId) ?? throw new NotFoundException("Course not found");
            AnnouncementModel obj = new() { Title = announcement.Title, Description = announcement.Description, CourseId = courseId, AtDate = DateTime.UtcNow, CreatedBy = createrId, Course = course };
            return await announcementRepo.CreateAnnouncementAsync(obj);
        }

        public IEnumerable<AnnouncementGet> GetAllForCourse(int courseId)
        {
            return AnnouncementGet.ToDTO(announcementRepo.GetAllForCourse(courseId));
        }
        public async Task<AnnouncementGet> GetByIdAsync(int id)
        {
            return AnnouncementGet.ToDTO(await announcementRepo.GetByIdAsync(id) ?? throw new NotFoundException("could not find announcement"));
        }
        public void DeleteAnnouncement(int id)
        {
            announcementRepo.DeleteAnnouncement(id);
        }
        public async Task<AnnouncementModel> UpdateAnnouncementAsync(int id, AnnouncementUpdate announcement, string createrId)
        {
            var elementToBeUpdated = await announcementRepo.GetByIdAsync(id) ?? throw new NotFoundException("could not find the announcement with given id");

            elementToBeUpdated.Title = announcement.Title;
            elementToBeUpdated.Description = announcement.Description;
            elementToBeUpdated.UpdateTime = DateTime.UtcNow;
            elementToBeUpdated.UpdatedBy = createrId;
            return await announcementRepo.UpdateAnnouncementAsync(elementToBeUpdated);
        }
    }
}
