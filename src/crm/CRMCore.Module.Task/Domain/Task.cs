using CRMCore.Framework.Entities;
using CRMCore.Framework.Entities.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRMCore.Module.Task.Domain
{
    public class Task : EntityBase
    {
        internal Task()
        { }

        internal Task(string name, Guid assignedTo)
            : this(IdHelper.GenerateId(), name, assignedTo)
        { }

        internal Task(Guid id, string name, Guid assignedTo) 
            : base(id)
        {
            Name = name;
            AssignedTo = assignedTo;
            TaskStatus = TaskStatus.NotStarted;
        }

        public static Task CreateInstance(Guid id, string name, Guid assignedTo)
        {
            return new Task(id, name, assignedTo);
        }

        public static Task CreateInstance(string name, Guid assignedTo)
        {
            return Task.CreateInstance(IdHelper.GenerateId(), name, assignedTo);
        }

        public Task ChangeName(string name)
        {
            if (!string.IsNullOrEmpty(name) && Name != name)
            {
                Name = name;
            }
            return this;
        }

        public Task ChangeAssignedTo(Guid id)
        {
            if (id != Guid.Empty && id != AssignedTo)
            {
                AssignedTo = id;
            }
            return this;
        }

        public Task ChangeCategoryType(CategoryType categoryType)
        {
            if (categoryType != CategoryType)
            {
                CategoryType = categoryType;
            }
            return this;
        }

        public Task ChangeTaskStatus(TaskStatus status)
        {
            if (status != TaskStatus)
            {
                TaskStatus = status;
            }
            return this;
        }

        [Required]
        public string Name { get; private set; }

        [Required]
        public Guid AssignedTo { get; private set; }

        [Required]
        public CategoryType CategoryType { get; private set; }

        [Required]
        public TaskStatus TaskStatus { get; private set; }
    }
}
