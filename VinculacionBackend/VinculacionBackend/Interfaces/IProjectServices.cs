using System.Linq;
using System.Net.Http;
using VinculacionBackend.Data.Entities;
using VinculacionBackend.Models;

namespace VinculacionBackend.Interfaces
{
    public interface IProjectServices
    {
        Project Find(long id);
        IQueryable<Project> All();
        Project Add(ProjectModel project);
        Project Delete(long projectId);
        IQueryable<User> GetProjectStudents(long projectId);
        Project UpdateProject(long projectId, ProjectModel model);
        bool AssignSection(ProjectSectionModel model);
        bool RemoveFromSection(long projectId, long sectionId);
        IQueryable<Project> GetUserProjects(long userId, string[] roles);
        HttpResponseMessage GetFinalReport(long projectId, int fieldHours, int calification, int beneficiariesQuantities, string beneficiariGroups);
    }
}