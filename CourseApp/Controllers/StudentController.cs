using System;
using Service.Services.Implementations;

namespace Project.Controllers
{
    public class StudentController
    {
        private StudentService _studentService;
        private GroupService _groupService;

        public StudentController(StudentService studentService, GroupService groupService)
        {
            _studentService = studentService;
            _groupService = groupService;
        }

        public void CreateStudent() => _studentService.CreateStudent();
        public void UpdateStudent() => _studentService.UpdateStudent();
        public void GetStudentById() => _studentService.GetStudentById();
        public void DeleteStudent() => _studentService.DeleteStudent();

        public void GetStudentsByAge()
        {
            Console.WriteLine("Enter age: ");
            if (int.TryParse(Console.ReadLine(), out int age))
                _studentService.GetStudentsByAge(age);
            else
                Console.WriteLine("Invalid age!");
        }

        public void GetAllStudentsByGroupId()
        {
            Console.WriteLine("Enter group ID: ");
            if (int.TryParse(Console.ReadLine(), out int groupId))
                _studentService.GetAllStudentsByGroupId(groupId);
            else
                Console.WriteLine("Invalid group ID!");
        }

        public void SearchMethodForStudentsByNameOrSurname()
        {
            Console.WriteLine("Enter student name or surname to search: ");
            string? keyword = Console.ReadLine();
            _studentService.SearchMethodForStudentsByNameorSurname(keyword);
        }
    }
}
