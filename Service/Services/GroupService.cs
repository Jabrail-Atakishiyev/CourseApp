using Domain.Models;
using Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Group = Domain.Models.Group;

namespace Service.Services.Implementations
{
    public class GroupService
    {
        public static List<Group> _groups = new List<Group>();
        public static int _idCounter = 1;

        public void CreateGroup()
        {
            Helper.WriteConsole(ConsoleColor.Yellow, "Enter group name:");
            string? name = Console.ReadLine();
            if (!Regex.IsMatch(name, @"[a-zA-Z0-9]") || string.IsNullOrWhiteSpace(name))
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "Invalid option for group name!");
                return;
            }

            Helper.WriteConsole(ConsoleColor.DarkYellow, "Enter teacher name:");
            string? teacherName = Console.ReadLine();
            Helper.WriteConsole(ConsoleColor.Yellow, "Enter teacher surname:");
            string? teacherSurname = Console.ReadLine();
            string? teacher = teacherName + " " + teacherSurname;
            if (string.IsNullOrWhiteSpace(teacher) || !Regex.IsMatch(teacher, @"^[a-zA-Z\s]+$"))
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "Invalid teacher name or surname!");
                return;
            }

            Helper.WriteConsole(ConsoleColor.DarkYellow, "Enter room number:");
            string? roomNumber = Console.ReadLine();
            if (!int.TryParse(roomNumber, out int room))
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "Wrong room number");
                return;
            }
            if (_groups.Any(g => g.Name.Trim().ToLower() == name.Trim().ToLower() &&
                         g.Teacher.Trim().ToLower() == teacher.Trim().ToLower()))
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "Group already exists!");
                return;
            }

            var group = new Group
            {
                Id = _idCounter++,
                Name = name,
                Teacher = teacher,
                Room = room
            };

            _groups.Add(group);
            Helper.WriteConsole(ConsoleColor.DarkGreen, $"Group {name} created successfully!");
        }

        public void UpdateGroup()
        {
            int id;
            while (true)
            {
                Helper.WriteConsole(ConsoleColor.DarkYellow, "Enter group ID to update:");
                if (int.TryParse(Console.ReadLine(), out id))
                    break;
                Helper.WriteConsole(ConsoleColor.DarkRed, "Invalid ID! Please enter a number.");
            }

            var group = _groups.FirstOrDefault(x => x.Id == id);
            if (group == null)
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "Group not found!");
                return;
            }

            string? name;
            while (true)
            {
                Helper.WriteConsole(ConsoleColor.DarkYellow, "Enter new group name:");
                name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name) && Regex.IsMatch(name, @"^[a-zA-Z0-9\s]+$"))
                    break;
                Helper.WriteConsole(ConsoleColor.DarkRed, "Invalid group name! Please try again.");
            }

            string? teacherName;
            while (true)
            {
                Helper.WriteConsole(ConsoleColor.DarkYellow, "Enter new teacher name:");
                teacherName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(teacherName) && Regex.IsMatch(teacherName, @"^[a-zA-Z]+$"))
                    break;
                Helper.WriteConsole(ConsoleColor.Red, "Invalid teacher name! Please try again.");
            }

            string? teacherSurname;
            while (true)
            {
                Helper.WriteConsole(ConsoleColor.DarkYellow, "Enter new teacher surname:");
                teacherSurname = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(teacherSurname) && Regex.IsMatch(teacherSurname, @"^[a-zA-Z]+$"))
                    break;
                Helper.WriteConsole(ConsoleColor.DarkRed, "Invalid teacher surname! Please try again.");
            }

            string teacher = $"{teacherName} {teacherSurname}";

            int room;
            while (true)
            {
                Helper.WriteConsole(ConsoleColor.DarkYellow, "Enter new room number:");
                if (int.TryParse(Console.ReadLine(), out room))
                    break;
                Helper.WriteConsole(ConsoleColor.DarkRed, "Invalid room number! Please enter a number.");
            }

            group.Name = name;
            group.Teacher = teacher;
            group.Room = room;

            Helper.WriteConsole(ConsoleColor.DarkGreen, "Group updated successfully!");
        }



        public void DeleteGroup()
        {
            Helper.WriteConsole(ConsoleColor.DarkYellow, "Enter group ID to delete:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "Wrong ID!");
                return;
            }

            var group = _groups.FirstOrDefault(x => x.Id == id);
            if (group == null)
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "Group not found!");
                return;
            }

            _groups.Remove(group);
            Helper.WriteConsole(ConsoleColor.DarkRed, $"Group {group.Name} deleted successfully!");
        }

        public void GetByIdGroup()
        {
            Helper.WriteConsole(ConsoleColor.Yellow, "Enter group ID:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "Wrong ID!");
                return;
            }

            var group = _groups.FirstOrDefault(x => x.Id == id);
            if (group == null)
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "Group not found!");
                return;
            }

            Helper.WriteConsole(ConsoleColor.DarkGray, $"ID: {group.Id}| Name: {group.Name}| Teacher: {group.Teacher}| Room: {group.Room}");
        }

        public void GetAllGroups()
        {
            if (!_groups.Any())
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "No groups found!");
                return;
            }

            foreach (var group in _groups)
            {
                Helper.WriteConsole(ConsoleColor.DarkGray, $"ID: {group.Id}| Name: {group.Name}| Teacher: {group.Teacher}| Room: {group.Room}");
            }
        }

        public List<Group> GetAllGroupsByTeacher()
        {
            string fullName;

            while (true)
            {
                Helper.WriteConsole(ConsoleColor.DarkYellow, "Enter teacher name:");
                string? teacherName = Console.ReadLine()?.Trim();

                Helper.WriteConsole(ConsoleColor.DarkYellow, "Enter teacher surname:");
                string? teacherSurname = Console.ReadLine()?.Trim();

                fullName = $"{teacherName} {teacherSurname}".Trim();

                if (string.IsNullOrWhiteSpace(fullName) || fullName.Any(char.IsDigit))
                {
                    Helper.WriteConsole(ConsoleColor.DarkRed, "Wrong teacher name or surname! Try again.");
                    continue;
                }

                fullName = Regex.Replace(fullName, @"\s+", " ");
                break;
            }

            var exactGroups = _groups
                .Where(g =>Regex.Replace(g.Teacher, @"\s+", " ")
                            .Equals(fullName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (exactGroups.Any())
            {
                Helper.WriteConsole(ConsoleColor.DarkGreen, $"Groups taught by {fullName}:");
                foreach (var g in exactGroups)
                    Helper.WriteConsole(ConsoleColor.DarkGray, $"ID: {g.Id}| Name: {g.Name}| Room: {g.Room}");
                return exactGroups;
            }

            var initialsGroups = _groups
                .Where(g =>
                {
                    string[] inputParts = fullName.Split(' ');
                    string[] teacherParts = g.Teacher.Split(' ');

                    if (inputParts.Length != teacherParts.Length) return false;

                    for (int i = 0; i < inputParts.Length; i++)
                    {
                        if (!teacherParts[i].StartsWith(inputParts[i], StringComparison.OrdinalIgnoreCase))
                            return false;
                    }
                    return true;
                })
                .ToList();

            if (initialsGroups.Any())
            {
                Helper.WriteConsole(ConsoleColor.DarkGreen, $"Groups matching initials '{fullName}':");
                foreach (var g in initialsGroups)
                    Helper.WriteConsole(ConsoleColor.DarkGray, $"ID: {g.Id}| Name: {g.Name}| Room: {g.Room} " +
                        $"Teacher's fullname : {g.Teacher}");
                return initialsGroups;
            }

            Helper.WriteConsole(ConsoleColor.DarkRed, $"No groups found for '{fullName}'.");
            return new List<Group>();
        }


        public List<Group> GetAllGroupsByRoom()
        {
            Helper.WriteConsole(ConsoleColor.DarkYellow, "Enter room number:");
            string? roomInput = Console.ReadLine();

            if (!int.TryParse(roomInput, out int room))
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "Wrong room number!");
                return new List<Group>();
            }

            var groups = _groups.Where(x => x.Room == room).ToList();

            if (!groups.Any())
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, $"No groups found in room {room}");
                return groups;
            }

            Helper.WriteConsole(ConsoleColor.DarkGreen, $"Groups in room {room}:");
            foreach (var g in groups)
            {
                Helper.WriteConsole(ConsoleColor.DarkGray, $"ID: {g.Id}, Name: {g.Name}, Teacher: {g.Teacher}, Room: {g.Room}");
            }

            return groups;
        }

        public List<Group> SearchMethodForGroupsByName()
        {
            Helper.WriteConsole(ConsoleColor.DarkYellow, "Enter group name to search:");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, "Group name cannot be empty!");
                return new List<Group>();
            }

            var groups = _groups
                .Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!groups.Any())
            {
                Helper.WriteConsole(ConsoleColor.DarkRed, $"No group found with name '{name}'.");
                return groups;
            }

            Helper.WriteConsole(ConsoleColor.DarkGreen, $"Search results for '{name}':");
            foreach (var g in groups)
            {
                Helper.WriteConsole(ConsoleColor.DarkGray, $"ID: {g.Id}, Name: {g.Name}, Teacher: {g.Teacher}, Room: {g.Room}");
            }

            return groups;
        }


    }
}

