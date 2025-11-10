using Domain.Models;
using System.Collections.Generic;

namespace Repository.Repository.Interface
{
    public interface IGroupRepository
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
