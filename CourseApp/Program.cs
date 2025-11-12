using System;
using Service.Services.Implementations;
using Project.Controllers;
using Project.Menus;

namespace Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var groupService = new GroupService();
            var studentService = new StudentService();

            var groupController = new GroupController(groupService);
            var studentController = new StudentController(studentService, groupService);

            var mainMenu = new MainMenu(groupController, studentController);
            mainMenu.ShowMainMenu();
        }
    }
}
