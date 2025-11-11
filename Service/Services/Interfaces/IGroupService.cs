using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interface
{
    public interface IGroupService
    {
        void CreateGroup(Group group);
        void UpdateGroup(Group group);
        void DeleteGroup(int id);
        Group GetByIdGroup(int id);
        List<Group> GetAllGroups();
        List<Group> GetAllByTeacher(string teacher);
        List<Group> GetAllByRoom(int room);
        List<Group> SearchGroupByName(string name);
    }
}