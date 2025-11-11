using Domain.Models;
using Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Service.Services.Implementations
{
    public class StudentService
    {
        private static List<Student> _students = new List<Student>();
        private static int _idCounter = 1;

        public void CreateStudent()
        {
            Helper.WriteConsole(ConsoleColor.Yellow, "Enter full name:");
            string fullName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fullName) || !Regex.IsMatch(fullName, @"[a-zA-Z]") || fullName.Any(char.IsDigit))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Wrong name!");
                return;
            }

            Helper.WriteConsole(ConsoleColor.Yellow, "Enter age:");
            string ageInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(ageInput) || !int.TryParse(ageInput, out int age) || age <= 0)
            {
                Helper.WriteConsole(ConsoleColor.Red, "Wrong age! Age must be greater than 0");
                return;
            }

            Helper.WriteConsole(ConsoleColor.Yellow, "Enter group ID:");
            string groupInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(groupInput) || !int.TryParse(groupInput, out int groupId))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Wrong group ID!");
                return;
            }


            Student student = new Student
            {
                Id = _idCounter++,
                Name = fullName,
                Age = age,
                Group = new Domain.Models.Group { Id = groupId }
            };

            _students.Add(student);
            Helper.WriteConsole(ConsoleColor.Green, $"Student '{fullName}' created successfully!");
        }


        public void UpdateStudent()
        {
            Helper.WriteConsole(ConsoleColor.Yellow, "Enter student ID to update:");
            string idInput = Console.ReadLine();

            if (!int.TryParse(idInput, out int id))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Wrong ID!");
                return;
            }

            var student = _students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                Helper.WriteConsole(ConsoleColor.Red, "Student not found!");
                return;
            }

            Helper.WriteConsole(ConsoleColor.Yellow, "Enter new full name:");
            string newName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(newName) || newName.Any(char.IsDigit))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Wrong name!");
                return;
            }
            student.Name = newName;

            Helper.WriteConsole(ConsoleColor.Yellow, "Enter new age:");
            string ageInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(ageInput) || !int.TryParse(ageInput, out int age))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Wrong age!");
                return;
            }
            student.Age = age;

            Helper.WriteConsole(ConsoleColor.Yellow, "Enter new group ID:");
            string groupInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(groupInput) || !int.TryParse(groupInput, out int groupId))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Wrong group ID!");
                return;
            }

            student.Group.Id = groupId;

            Helper.WriteConsole(ConsoleColor.Green, "Student updated successfully!");
        }


        public void DeleteStudent()
        {
            Helper.WriteConsole(ConsoleColor.Yellow, "Enter student ID to delete:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Wrong ID!");
                return;
            }

            var student = _students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                Helper.WriteConsole(ConsoleColor.Red, "Student not found!");
                return;
            }

            _students.Remove(student);
            Helper.WriteConsole(ConsoleColor.Red, $"Student '{student.Name}' deleted successfully!");
        }

        public void GetStudentById()
        {
            Helper.WriteConsole(ConsoleColor.Yellow, "Enter student ID:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Wrong ID!");
                return;
            }

            var student = _students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                Helper.WriteConsole(ConsoleColor.Red, "Student not found!");
                return;
            }

            Helper.WriteConsole(ConsoleColor.Cyan, $"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}, Group ID: {student.Group.Id}");
        }

        public void GetAllStudents()
        {
            if (!_students.Any())
            {
                Helper.WriteConsole(ConsoleColor.Red, "No students found!");
                return;
            }

            foreach (var student in _students)
            {
                Helper.WriteConsole(ConsoleColor.Cyan, $"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}, Group ID: {student.Group.Id}");
            }
        }

        public List<Student> GetStudentsByAge(int age)
        {

            if (age <= 0)
            {
                Helper.WriteConsole(ConsoleColor.Red, "Wrong age!");
                return new List<Student>();
            }

            var students = _students
                .Where(x => x.Age == age)
                .ToList();

            if (!students.Any())
            {
                Helper.WriteConsole(ConsoleColor.Red, $"No student found with age {age}.");
            }

            return students;
        }


        public List<Student> GetAllStudentsByGroupId(int groupId)
        {
            var students = _students.Where(x => x.Group.Id == groupId).ToList();
            if (!students.Any())
                Helper.WriteConsole(ConsoleColor.Red, $"No student found in group {groupId}");
            return students;
        }

        public List<Student> SearchMethodForStudentsByNameorSurname(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Student name cannot be empty!");
                return new List<Student>();
            }
            var students = _students.Where(x => x.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!students.Any())
                Helper.WriteConsole(ConsoleColor.Red, $"No student found with name '{keyword}'");
            return students;
        }
    }
}
