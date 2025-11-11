using Domain.Models;
using Repository.Data;
using Repository.Exceptions;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class StudentRepository : IStudentRepository
    {
        public void CreateStudent(Student student)
        {
            AppDbContext<Student>.datas.Add(student);
        }

        public void UpdateStudent(int id, Student student)
        {
            var existing = AppDbContext<Student>.datas.FirstOrDefault(s => s.Id == id);
            if (existing == null) throw new NotFoundException("Student not found");

            existing.Name = student.Name;
            existing.Age = student.Age;
            existing.Group = student.Group;        }

        public void DeleteStudent(int id)
        {
            var student = AppDbContext<Student>.datas.FirstOrDefault(s => s.Id == id);
            if (student == null) throw new NotFoundException("Student not found");

            AppDbContext<Student>.datas.Remove(student);
        }

        public Student GetStudentById(int id)
        {
            var student = AppDbContext<Student>.datas.FirstOrDefault(s => s.Id == id);
            if (student == null) throw new NotFoundException("Student not found");

            return student;
        }

        public List<Student> GetAllStudents()
        {
            return AppDbContext<Student>.datas.ToList();
        }

        public List<Student> GetStudentsByAge(int age)
        {
            return AppDbContext<Student>.datas.Where(s => s.Age == age).ToList();
        }

        public List<Student> GetAllStudentsByGroupId(int groupId)
        {
            return AppDbContext<Student>.datas
         .Where(s => s.Group != null && ((Group)s.Group).Id == groupId)
         .ToList();
        }

        public List<Student> SearchStudentByNameOrSurname(string keyword)
        {
            return AppDbContext<Student>.datas
         .Where(s => s.Name != null && s.Name.ToLower().Contains(keyword.ToLower()))
         .ToList();

        }
    }
}
