using System;
using Project.Controllers;
using Project.Menus;
using Service.Helpers;

namespace Project.Menus
{
    public class MainMenu
    {
        private GroupController _groupController;
        private StudentController _studentController;

        public MainMenu(GroupController groupController, StudentController studentController)
        {
            _groupController = groupController;
            _studentController = studentController;
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                Helper.WriteConsole(ConsoleColor.DarkYellow, "===== MAIN MENU =====");
                Helper.WriteConsole(ConsoleColor.Cyan, "1. Group menu");
                Helper.WriteConsole(ConsoleColor.Cyan, "2. Student menu");
                Helper.WriteConsole(ConsoleColor.Cyan, "0. Exit");
                Helper.WriteConsole(ConsoleColor.Yellow, "Enter your choice: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        new GroupMenu(_groupController).ShowGroupMenu();
                        break;
                    case "2":
                        new StudentMenu(_studentController).ShowStudentMenu();
                        break;
                    case "0":
                        Console.WriteLine("Exiting program...");
                        return;
                    default:
                        Helper.WriteConsole(ConsoleColor.DarkRed, "Wrong choice!");
                        break;
                }
            }
        }
    }
}
