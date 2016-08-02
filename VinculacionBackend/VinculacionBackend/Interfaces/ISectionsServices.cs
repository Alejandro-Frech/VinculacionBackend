using System.Linq;
using VinculacionBackend.Data.Entities;
using VinculacionBackend.Models;

namespace VinculacionBackend.Interfaces
{
    public interface ISectionsServices
    {
        IQueryable<Section> All();
        Section Delete(long sectionId);
        void Add(Section section);
        Section Find(long id);
        bool AssignStudents(SectionStudentModel model);
        void Map(Section section, SectionEntryModel sectionModel);
        bool RemoveStudents(SectionStudentModel model);
        Section UpdateSection(long sectionId,SectionEntryModel model);
        IQueryable<User> GetSectionStudents(long sectionId);
        IQueryable<Project> GetSectionsProjects(long sectionId);
        IQueryable<Section> GetCurrentPeriodSections();
    }
}
