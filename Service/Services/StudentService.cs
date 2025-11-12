using Domain.Models;
using Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Service.Services.Implementations
{
    public class StudentService
    {
        private static List<Student> _students = new List<Student>();
        private static int _idCounter = 1;

        public void CreateStudent()
        {
            string? name;
            while (true)
            {
                Helper.WriteConsole(ConsoleColor.Yellow, "Enter name:");
                name = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(name) && Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                    break;
                Helper.WriteConsole(ConsoleColor.DarkRed, "Invalid name! Please try again.");
            }

            string? surname;
            while (true)
            {
                Helper.WriteConsole(ConsoleColor.Yellow, "Enter surname:");
                surname = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(surname) && Regex.IsMatch(surname, @"^[a-zA-Z]+$"))
                    break;
                Helper.WriteConsole(ConsoleColor.DarkRed, "Invalid surname! Please try again.");
            }

            string fullName = $"{name} {surname}";

            int age;
            while (true)
            {
                Helper.WriteConsole(ConsoleColor.DarkYellow, "Enter age:");
                string? ageInput = Console.ReadLine();

                if (int.TryParse(ageInput, out age) && age > 0)
                    break;
                Helper.WriteConsole(ConsoleColor.Red, "Invalid age!");
            }

            int groupId;
            while (true)
            {
                Helper.WriteConsole(ConsoleColor.Yellow, "Enter group ID:");
                string? groupInput = Console.ReadLine();

                if (int.TryParse(groupInput, out groupId))
                {
                    bool groupExists = GroupService._groups.Any(x => x.Id == groupId);
                    if (groupExists)
                        break;

                    Helper.WriteConsole(ConsoleColor.Red, "Group with this ID does not exist!");
                }
                else
                {
                    Helper.WriteConsole(ConsoleColor.Red, "Invalid group ID! Please enter a number.");
                }
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
            int id;
            while (true)
            {
                Helper.WriteConsole(ConsoleColor.Yellow, "Enter student ID to update:");
                if (int.TryParse(Console.ReadLine(), out id))
                    break;
                Helper.WriteConsole(ConsoleColor.DarkRed, "Invalid ID! Please enter a number.");
            }

            var student = _students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "Student not found!");
                return;
            }

            string? name;
            while (true)
            {
                Helper.WriteConsole(ConsoleColor.Yellow, "Enter new name:");
                name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name) && Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                    break;
                Helper.WriteConsole(ConsoleColor.DarkRed, "Invalid name! Please try again.");
            }

            string? surname;
            while (true)
            {
                Helper.WriteConsole(ConsoleColor.Yellow, "Enter new surname:");
                surname = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(surname) && Regex.IsMatch(surname, @"^[a-zA-Z]+$"))
                    break;
                Helper.WriteConsole(ConsoleColor.DarkRed, "Invalid surname! Please try again.");
            }

            string fullName = $"{name} {surname}";

            int age;
            while (true)
            {
                Helper.WriteConsole(ConsoleColor.Yellow, "Enter new age:");
                if (int.TryParse(Console.ReadLine(), out age) && age > 0)
                    break;
                Helper.WriteConsole(ConsoleColor.Red, "Invalid age! Please enter a number greater than 0.");
            }

            int groupId;
            while (true)
            {
                Helper.WriteConsole(ConsoleColor.Yellow, "Enter new group ID:");
                if (int.TryParse(Console.ReadLine(), out groupId))
                {
                    bool exists = GroupService._groups.Any(x => x.Id == groupId);
                    if (exists)
                        break;
                    Helper.WriteConsole(ConsoleColor.DarkRed, "Group with this ID does not exist!");
                }
                else
                {
                    Helper.WriteConsole(ConsoleColor.DarkRed, "Wrong group ID! Please enter a number.");
                }
            }

            student.Name = fullName;
            student.Age = age;
            student.Group.Id = groupId;

            Helper.WriteConsole(ConsoleColor.Green, "Student updated successfully!");
        }


        public void DeleteStudent()
        {
            Helper.WriteConsole(ConsoleColor.Yellow, "Enter student ID to delete:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "Wrong ID!");
                return;
            }

            var student = _students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "Student not found!");
                return;
            }

            _students.Remove(student);
            Helper.WriteConsole(ConsoleColor.DarkRed, $"Student '{student.Name}' deleted successfully!");
        }

        public void GetStudentById()
        {
            Helper.WriteConsole(ConsoleColor.Yellow, "Enter student ID:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "Wrong ID!");
                return;
            }

            var student = _students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "Student not found!");
                return;
            }

            Helper.WriteConsole(ConsoleColor.Cyan, $"ID: {student.Id}| Name: {student.Name}| Age: {student.Age}| Group ID: {student.Group.Id}");
        }

        public void GetAllStudents()
        {
            if (!_students.Any())
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "No students found!");
                return;
            }

            foreach (var student in _students)
            {
                Helper.WriteConsole(ConsoleColor.Cyan, $"ID: {student.Id}| Name: {student.Name}| Age: {student.Age}| Group ID: {student.Group.Id}");
            }
        }

        public List<Student> GetStudentsByAge(int age)
        {
            if (age <= 0)
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "Wrong age!");
                return new List<Student>();
            }

            var students = _students.Where(x => x.Age == age).ToList();
            if (!students.Any())
                Helper.WriteConsole(ConsoleColor.DarkRed, $"No student found with age {age}.");

            foreach (var s in students)
                Helper.WriteConsole(ConsoleColor.Cyan, $"ID: {s.Id}| Name: {s.Name}| Age: {s.Age}| Group ID: {s.Group.Id}");

            return students;
        }

        public List<Student> GetAllStudentsByGroupId(int groupId)
        {
            var students = _students.Where(x => x.Group.Id == groupId).ToList();
            if (!students.Any())
                Helper.WriteConsole(ConsoleColor.DarkRed, $"No student found in group {groupId}");
            else
                foreach (var s in students)
                    Helper.WriteConsole(ConsoleColor.Cyan, $"ID: {s.Id}| Name: {s.Name}| Age: {s.Age}| Group ID: {s.Group.Id}");
            return students;
        }

        public List<Student> SearchMethodForStudentsByNameorSurname(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "Student name cannot be empty!");
                return new List<Student>();
            }

            if (!Regex.IsMatch(keyword, @"^[a-zA-Z\s]+$"))
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "Invalid input! Only letters are allowed.");
                return new List<Student>();
            }

            var students = _students
                .Where(x => x.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!students.Any())
                Helper.WriteConsole(ConsoleColor.Red, $"No student found with name '{keyword}'");
            else
                foreach (var s in students)
                    Helper.WriteConsole(ConsoleColor.Cyan, $"ID: {s.Id}| Name: {s.Name}| Age: {s.Age}| Group ID: {s.Group.Id}");

            return students;
        }

    }
}
