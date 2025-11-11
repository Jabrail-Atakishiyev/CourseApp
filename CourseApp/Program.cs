using Domain.Models;
using Service.Helpers;
using Service.Services;
using Service.Services.Implementations;

internal class Program
{
    static void Main(string[] args)
    {
        var groupService = new GroupService();
        var studentService = new StudentService();

        MainMenu(groupService, studentService);
    }

    static void MainMenu(GroupService groupService, StudentService studentService)
    {
        while (true)
        {
            Helper.WriteConsole(ConsoleColor.DarkYellow, "---- SELECT OPTION ----");
            Helper.WriteConsole(ConsoleColor.Cyan, "1 - Group settings");
            Helper.WriteConsole(ConsoleColor.Cyan, "2 - Student settings");
            Helper.WriteConsole(ConsoleColor.Cyan, "0 - Exit");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ShowGroupSettings(groupService);
                    break;
                case "2":
                    ShowStudentSettings(studentService);
                    break;
                case "0":
                    Helper.WriteConsole(ConsoleColor.Red, "Program ended");
                    return;
                default:
                    Helper.WriteConsole(ConsoleColor.Red, "Wrong option, try again!");
                    break;
            }
        }
    }

    static void ShowGroupSettings(GroupService groupService)
    {
        while (true)
        {
            Helper.WriteConsole(ConsoleColor.DarkYellow, "---- GROUP SETTINGS ----");
            Helper.WriteConsole(ConsoleColor.Cyan, "1 - Create Group");
            Helper.WriteConsole(ConsoleColor.Cyan, "2 - Update Group");
            Helper.WriteConsole(ConsoleColor.Cyan, "3 - Delete Group");
            Helper.WriteConsole(ConsoleColor.Cyan, "4 - Get Group by Id");
            Helper.WriteConsole(ConsoleColor.Cyan, "5 - Get All Groups");
            Helper.WriteConsole(ConsoleColor.Cyan, "6 - Get All Groups By Teacher Name");
            Helper.WriteConsole(ConsoleColor.Cyan, "7 - Get All Groups By Room");
            Helper.WriteConsole(ConsoleColor.Cyan, "8 - Search Groups By Name");


            Helper.WriteConsole(ConsoleColor.Cyan, "0 - Back to main menu");

            string groupSettingsInput = Console.ReadLine();
            switch (groupSettingsInput)
            {
                case "1":
                    groupService.CreateGroup();
                    break;
                case "2":
                    groupService.UpdateGroup();
                    break;
                case "3":
                    groupService.DeleteGroup();
                    break;
                case "4":
                    groupService.GetByIdGroup();
                    break;
                case "5":
                    groupService.GetAllGroups();
                    break;
                case "6":
                    Helper.WriteConsole(ConsoleColor.Yellow, "Enter teacher name:");
                    string teacher = Console.ReadLine();
                    var groupsByTeacher = groupService.GetAllGroupsByTeacher(teacher);
                    foreach( var group in groupsByTeacher)
                    {
                        Helper.WriteConsole(ConsoleColor.DarkGray,$"ID : {group.Id} , " +
                            $"Name : {group.Name} , Teacher : {group.Teacher}");
                    }
                    break;
                case "7":
                    Helper.WriteConsole(ConsoleColor.Yellow, "Enter room number");
                    if(int.TryParse(Console.ReadLine(), out int room))
                    {
                        var groupsByRoom = groupService.GetAllGroupsByRoom(room);
                        foreach(var group in groupsByRoom)
                        {
                            Helper.WriteConsole(ConsoleColor.DarkGray,$"ID : {group.Id}," +
                                $"Name : {group.Name} , Teacher : {group.Teacher},Room : {group.Room}");
                        };
                    }
                    else
                    {
                        Helper.WriteConsole(ConsoleColor.Red, "Wrong room number");
                    }
                    break;
                case "8":
                    Helper.WriteConsole(ConsoleColor.Yellow, "Enter group name to search...");
                    string groupName = Console.ReadLine();
                    var groupsByName = groupService.SearchMethodForGroupsByName(groupName);
                    foreach(var group in groupsByName)
                    {
                        Helper.WriteConsole(ConsoleColor.DarkGray,$"ID ; {group.Id},Name : {group.Name}," +
                            $"Teacher : {group.Teacher} , Room : {group.Teacher}");
                    }
                    break;
                case "0":
                    return;
                default:
                    Helper.WriteConsole(ConsoleColor.Red, "Wrong option!");
                    break;
            }
        }
    }

    static void ShowStudentSettings(StudentService studentService)
    {
        while (true)
        {
            Helper.WriteConsole(ConsoleColor.DarkYellow, "---- STUDENT SETTINGS ----");
            Helper.WriteConsole(ConsoleColor.Cyan, "1 - Create Student");
            Helper.WriteConsole(ConsoleColor.Cyan, "2 - Update Student");
            Helper.WriteConsole(ConsoleColor.Cyan, "3 - Delete Student");
            Helper.WriteConsole(ConsoleColor.Cyan, "4 - Get Student by Id");
            Helper.WriteConsole(ConsoleColor.Cyan, "5 - Get Students By Age");
            Helper.WriteConsole(ConsoleColor.Cyan, "6 - Get All Students By Group Id");
            Helper.WriteConsole(ConsoleColor.Cyan, "7 - Search Students By Name or Surname");
            Helper.WriteConsole(ConsoleColor.Cyan, "0 - Back to main menu");

            string studentSettingsInput = Console.ReadLine();
            switch (studentSettingsInput)
            {
                case "1":
                    studentService.CreateStudent();
                    break;
                case "2":
                    studentService.UpdateStudent();
                    break;
                case "3":
                    studentService.DeleteStudent();
                    break;
                case "4":
                    studentService.GetStudentById();
                    break;
                case "5":
                    Helper.WriteConsole(ConsoleColor.Yellow, "Enter age:");
                    string ageInput = Console.ReadLine();

                    if (int.TryParse(ageInput, out int age))
                    {
                        var studentsByAge = studentService.GetStudentsByAge(age);
                        foreach (var student in studentsByAge)
                        {
                            Helper.WriteConsole(ConsoleColor.DarkGray,
                                $"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}" +
                                $", Group ID : {student.Group.Id}");
                        }
                    }
                    else
                    {
                        Helper.WriteConsole(ConsoleColor.Red, "Invalid age input!");
                    }

                    break;
                case "6":
                    Helper.WriteConsole(ConsoleColor.Yellow, "Enter group id:");
                    if (int.TryParse(Console.ReadLine(), out int groupId))
                    {
                        var studentsByGroup = studentService.GetAllStudentsByGroupId(groupId);
                        foreach(var student in studentsByGroup)
                        {
                            Helper.WriteConsole(ConsoleColor.DarkGray, $"ID : {student.Id}, Name : {student.Name}," +
                                $"Age : {student.Age} , Group ID : {student.Group.Id}");
                        }

                    }


                    break;
                case "7":
                    Helper.WriteConsole(ConsoleColor.Yellow, "Enter name or surname");
                    string keyword = Console.ReadLine();
                    var studentsByKeyword = studentService.SearchMethodForStudentsByNameorSurname(keyword);
                    foreach (var student in studentsByKeyword)
                    {
                        Helper.WriteConsole(ConsoleColor.DarkGray, $"ID : {student.Id}, Name : {student.Name}," +
                                $"Age : {student.Age} , Group ID : {student.Group.Id}");
                    }



                    break;

                case "0":
                    return;
                default:
                    Helper.WriteConsole(ConsoleColor.Red, "Wrong option!");
                    break;
            }
        }
    }
}
