using System;
using Service.Services.Implementations;

namespace Project.Controllers
{
    public class GroupController
    {
        private GroupService _groupService;

        public GroupController(GroupService groupService)
        {
            _groupService = new GroupService();
        }

        public void CreateGroup() => _groupService.CreateGroup();
        public void UpdateGroup() => _groupService.UpdateGroup();
        public void DeleteGroup() => _groupService.DeleteGroup();
        public void GetGroupById() => _groupService.GetByIdGroup();
        public void GetAllGroupsByTeacher() => _groupService.GetAllGroupsByTeacher();
        public void GetAllGroupsByRoom() => _groupService.GetAllGroupsByRoom();
        public void GetAllGroups() => _groupService.GetAllGroups();
        public void SearchMethodForGroupsByName() => _groupService.SearchMethodForGroupsByName();

        
    }
}
