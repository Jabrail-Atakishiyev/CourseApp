using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repository.Repository.Interface
{
    public interface IStudentRepository
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

