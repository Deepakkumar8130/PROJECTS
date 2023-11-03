using PROJECTAPI.Data;
using PROJECTAPI.Models;

namespace PROJECTAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string AddStudent(Student student)
        {
            _context.students.Add(student);
            _context.SaveChanges();
            return "ok";
        }

        public string DeleteStudent(int id)
        {
            var student = _context.students.SingleOrDefault(x => x.Id == id);
            if (student != null)
            {
                _context.students.Remove(student);
                _context.SaveChanges();
                return "ok";
            }
            return "not found";

        }

        public StudentVM GetStudentById(int id)
        {
            var student = (from s in _context.students
                           join c in _context.courses
                           on s.Course equals c.CId
                           where s.Id == id
                           select new StudentVM()
                           {
                               Name = s.Name,
                               Roll_No = s.Roll_No,
                               Course = c.Name,
                               Mobile = s.Mobile,
                               Email = s.Email,
                           });
            return (StudentVM)student;
         
        }

        public List<StudentVM> GetStudents()
        {
            var students = (from s in _context.students
                            join c in _context.courses
                            on s.Course equals c.CId
                            select new StudentVM()
                            {
                                Name = s.Name,
                                Roll_No = s.Roll_No,
                                Course = c.Name,
                                Mobile = s.Mobile,
                                Email = s.Email,
                            }).ToList();
            return students;
        }

        public string UpdateStudent(Student student)
        {
            _context.Update(student);
            _context.SaveChanges();
            return "ok";
        }
    }
}
