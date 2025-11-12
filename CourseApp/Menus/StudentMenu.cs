using System;
using Project.Controllers;
using Service.Helpers;

namespace Project.Menus
{
    public class StudentMenu
    {
        private StudentController _studentController;

        public StudentMenu(StudentController studentController)
        {
            _studentController = studentController;
        }

        public void ShowStudentMenu()
        {
            Helper.WriteConsole(ConsoleColor.DarkYellow, "===== STUDENT MENU =====");
            Helper.WriteConsole(ConsoleColor.Cyan, "1. Create student");
            Helper.WriteConsole(ConsoleColor.Cyan, "2. Update student");
            Helper.WriteConsole(ConsoleColor.Cyan, "3. Delete student");
            Helper.WriteConsole(ConsoleColor.Cyan, "4. Get student by id");
            Helper.WriteConsole(ConsoleColor.Cyan, "5. Get students by age");
            Helper.WriteConsole(ConsoleColor.Cyan, "6. Get students by group id");
            Helper.WriteConsole(ConsoleColor.Cyan, "7. Search method for students by name or surname");
            Helper.WriteConsole(ConsoleColor.Cyan, "0. Back to main menu");

            while (true)
            {
                Helper.WriteConsole(ConsoleColor.Yellow, "Enter your choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": _studentController.CreateStudent(); break;
                    case "2": _studentController.UpdateStudent(); break;
                    case "3": _studentController.DeleteStudent(); break;
                    case "4": _studentController.GetStudentById(); break;
                    case "5": _studentController.GetStudentsByAge(); break;
                    case "6": _studentController.GetAllStudentsByGroupId(); break;
                    case "7": _studentController.SearchMethodForStudentsByNameOrSurname(); break;
                    case "0": return;
                    default:
                        Helper.WriteConsole(ConsoleColor.DarkRed, "Wrong choice!");
                        break;
                }
            }
        }
    }
}
