using Domain.Models;
using System.Collections.Generic;

namespace Service.Services.Interface
{
    public interface IStudentService
    {
        void CreateStudent(Student student);
        void UpdateStudent(int id, Student student);
        void DeleteStudent(int id);
        Student GetStudentById(int id);
        List<Student> GetAllStudents();
        List<Student> GetStudentsByAge(int age);
        List<Student> GetAllStudentsByGroupId(int groupId);
        List<Student> SearchStudentByNameOrSurname(string keyword);
    }
}
