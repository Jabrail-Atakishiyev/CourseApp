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
        private static List<Group> _groups = new List<Group>();
        private static int _idCounter = 1;

        public void CreateGroup()
        {
            Helper.WriteConsole(ConsoleColor.Yellow, "Enter group name:");
            string name = Console.ReadLine();
            if (!Regex.IsMatch(name, @"[a-zA-Z0-9]") || string.IsNullOrWhiteSpace(name))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Wrong option for group name!");
                return;
            }

            Helper.WriteConsole(ConsoleColor.Yellow, "Enter teacher name:");
            string teacher = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(teacher) || !Regex.IsMatch(teacher, @"^[a-zA-Z\s]+$"))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Wrong option for teacher name!");
                return;
            }

            Helper.WriteConsole(ConsoleColor.Yellow, "Enter room number:");
            string roomNumber = Console.ReadLine();
            if (!int.TryParse(roomNumber, out int room))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Wrong room number");
                return;
            }
            if (_groups.Any(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                 && g.Teacher.Equals(teacher, StringComparison.OrdinalIgnoreCase)
                 && g.Room == room))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Group already exists!");
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
            Helper.WriteConsole(ConsoleColor.Green, $"Group {name} created successfully!");
        }

        public void UpdateGroup()
        {
            Helper.WriteConsole(ConsoleColor.Yellow, "Enter group ID to update:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Wrong ID!");
                return;
            }

            var group = _groups.FirstOrDefault(x => x.Id == id);
            if (group == null)
            {
                Helper.WriteConsole(ConsoleColor.Red, "Group not found!");
                return;
            }

            Helper.WriteConsole(ConsoleColor.Yellow, "Enter new name:");
            string newName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(newName))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Name cannot be empty!");
                return;
            }
            group.Name = newName;

            Helper.WriteConsole(ConsoleColor.Yellow, "Enter new teacher name:");
            string teacher = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(teacher) || teacher.Any(char.IsDigit))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Wrong teacher name!");
                return;
            }
            group.Teacher = teacher;

            Helper.WriteConsole(ConsoleColor.Yellow, "Enter new room number:");
            string roomInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(roomInput))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Room number cannot be empty!");
                return;
            }

            if (int.TryParse(roomInput, out int room))
            {
                group.Room = room;
            }
            else
            {
                Helper.WriteConsole(ConsoleColor.Red, "Wrong room number!");
                return;
            }

            Helper.WriteConsole(ConsoleColor.Green, "Group updated successfully!");

        }


        public void DeleteGroup()
        {
            Helper.WriteConsole(ConsoleColor.Yellow, "Enter group ID to delete:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Wrong ID!");
                return;
            }

            var group = _groups.FirstOrDefault(x => x.Id == id);
            if (group == null)
            {
                Helper.WriteConsole(ConsoleColor.Red, "Group not found!");
                return;
            }

            _groups.Remove(group);
            Helper.WriteConsole(ConsoleColor.Red, $"Group {group.Name} deleted successfully!");
        }

        public void GetByIdGroup()
        {
            Helper.WriteConsole(ConsoleColor.Yellow, "Enter group ID:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Wrong ID!");
                return;
            }

            var group = _groups.FirstOrDefault(x => x.Id == id);
            if (group == null)
            {
                Helper.WriteConsole(ConsoleColor.Red, "Group not found!");
                return;
            }

            Helper.WriteConsole(ConsoleColor.DarkGray, $"ID: {group.Id}, Name: {group.Name}, Teacher: {group.Teacher}, Room: {group.Room}");
        }

        public void GetAllGroups()
        {
            if (!_groups.Any())
            {
                Helper.WriteConsole(ConsoleColor.Red, "No groups found!");
                return;
            }

            foreach (var group in _groups)
            {
                Helper.WriteConsole(ConsoleColor.DarkGray, $"ID: {group.Id}, Name: {group.Name}, Teacher: {group.Teacher}, Room: {group.Room}");
            }
        }

        public List<Group> GetAllGroupsByTeacher(string teacher)
        {
            var groups = _groups
                .Where(x => !string.IsNullOrEmpty(x.Teacher) &&
                            x.Teacher.ToLower().Contains(teacher.ToLower()))
                .ToList();

            if (!groups.Any())
            {
                Helper.WriteConsole(ConsoleColor.Red, $"No groups found for teacher '{teacher}'.");
            }

            return groups;
        }

        public List<Group> GetAllGroupsByRoom(int room)
        {
            var groups = _groups.Where(x => x.Room == room).ToList();
            if (!groups.Any())
                Helper.WriteConsole(ConsoleColor.Red, $"No groups found in room {room}");
            return groups;
        }

        public List<Group> SearchMethodForGroupsByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Helper.WriteConsole(ConsoleColor.Red, "Group name cannot be empty!");
                return new List<Group>();
            }

            var groups = _groups
                .Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!groups.Any())
                Helper.WriteConsole(ConsoleColor.Red, $"No group found with name '{name}'.");

            return groups;
        }

    }

    internal class UpdatedDate
    {
    }
}

