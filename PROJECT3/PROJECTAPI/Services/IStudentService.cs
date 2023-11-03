using PROJECTAPI.Models;

namespace PROJECTAPI.Services
{
    public interface IStudentService
    {
        List<StudentVM> GetStudents();
        StudentVM GetStudentById(int id);
        string AddStudent(Student student);
        string UpdateStudent(Student student);
        string DeleteStudent(int id);
    }
}
