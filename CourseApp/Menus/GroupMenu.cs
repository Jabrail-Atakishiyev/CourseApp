using Project.Controllers;
using Service.Helpers;

namespace Project.Menus
{
    public class GroupMenu
    {
        private GroupController _groupController;
        private StudentController _studentController;

        public GroupMenu(GroupController groupController)
        {
            _groupController = groupController;
        }

        public void ShowGroupMenu()
        {
            Helper.WriteConsole(ConsoleColor.DarkYellow, "===== GROUP MENU =====");
            Helper.WriteConsole(ConsoleColor.Cyan, "1 -- Create group");
            Helper.WriteConsole(ConsoleColor.Cyan, "2 -- Update group");
            Helper.WriteConsole(ConsoleColor.Cyan, "3 -- Delete group");
            Helper.WriteConsole(ConsoleColor.Cyan, "4 -- Get group by id");
            Helper.WriteConsole(ConsoleColor.Cyan, "5 -- Get all groups by teacher");
            Helper.WriteConsole(ConsoleColor.Cyan, "6 -- Get all groups by room");
            Helper.WriteConsole(ConsoleColor.Cyan, "7 -- Get all groups");
            Helper.WriteConsole(ConsoleColor.Cyan, "8 -- Search groups by name");
            Helper.WriteConsole(ConsoleColor.Cyan, "0 -- Back to main menu");

            while (true)
            {
                Helper.WriteConsole(ConsoleColor.DarkYellow, "Enter your choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": _groupController.CreateGroup(); break;
                    case "2": _groupController.UpdateGroup(); break;
                    case "3": _groupController.DeleteGroup(); break;
                    case "4": _groupController.GetGroupById(); break;
                    case "5": _groupController.GetAllGroupsByTeacher(); break;
                    case "6": _groupController.GetAllGroupsByRoom(); break;
                    case "7": _groupController.GetAllGroups(); break;
                    case "8": _groupController.SearchMethodForGroupsByName(); break;
                    case "0": return;
                    default:
                        Helper.WriteConsole(ConsoleColor.DarkRed, "Wrong choice!");
                        break;
                }
            }
        }
    }
}
