using Domain.Models;
using Repository.Data;
using Repository.Exceptions;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repository.Implementations
{
    public class GroupRepository : IGroupRepository
    {
        public void CreateGroup(Group group)
        {
            AppDbContext<Group>.datas.Add(group);
        }

        public void UpdateGroup(Group group)
        {
            var existing = AppDbContext<Group>.datas.FirstOrDefault(g => g.Id == group.Id);
            if (existing == null) throw new NotFoundException("Group not found");

            existing.Name = group.Name;
            existing.Teacher = group.Teacher;
            existing.Room = group.Room;
            existing.UpdatedDate = DateTime.Now;
        }

        public void DeleteGroup(int id)
        {
            var group = AppDbContext<Group>.datas.FirstOrDefault(g => g.Id == id);
            if (group == null) throw new NotFoundException("Group not found");

            AppDbContext<Group>.datas.Remove(group);
        }

        public Group GetByIdGroup(int id)
        {
            var group = AppDbContext<Group>.datas.FirstOrDefault(g => g.Id == id);
            if (group == null) throw new NotFoundException("Group not found");

            return group;
        }

        public List<Group> GetAllGroups()
        {
            return AppDbContext<Group>.datas.ToList();
        }

        public List<Group> GetAllByTeacher(string teacher)
        {
            return AppDbContext<Group>.datas
                .Where(g => g.Teacher != null && g.Teacher.ToLower() == teacher.ToLower())
                .ToList();
        }

        public List<Group> GetAllByRoom(int room)
        {
            return AppDbContext<Group>.datas
                .Where(g => g.Room == room)
                .ToList();
        }

        public List<Group> SearchGroupByName(string name)
        {
            return AppDbContext<Group>.datas
                .Where(g => g.Name != null && g.Name.ToLower().Contains(name.ToLower()))
                .ToList();
        }
    }
}
