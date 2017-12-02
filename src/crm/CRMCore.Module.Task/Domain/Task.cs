using CRMCore.Framework.Entities;
using CRMCore.Framework.Entities.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRMCore.Module.Task.Domain
{
    public class Task : EntityBase
    {
        internal Task()
        {
        }

        internal Task(string name, DueType dueType, Guid assignedTo, CategoryType categoryType)
            : this(IdHelper.GenerateId(), name, dueType, assignedTo, categoryType)
        { }

        internal Task(Guid id, string name, DueType dueType, Guid assignedTo, CategoryType categoryType) 
            : base(id)
        {
            Name = name;
            DueType = dueType;
            AssignedTo = assignedTo;
            CategoryType = categoryType;
            TaskStatus = TaskStatus.Pending;
        }

        public static Task CreateInstance(Guid id, string name, DueType dueType, Guid assignedTo, CategoryType categoryType)
        {
            return new Task(id, name, dueType, assignedTo, categoryType);
        }

        public static Task CreateInstance(string name, DueType dueType, Guid assignedTo, CategoryType categoryType)
        {
            return Task.CreateInstance(IdHelper.GenerateId(), name, dueType, assignedTo, categoryType);
        }

        public Task ChangeName(string name)
        {
            if (!string.IsNullOrEmpty(name) && Name != name)
            {
                Name = name;
            }
            return this;
        }

        public Task ChangeDueType(DueType dueType)
        {
            if (dueType != DueType)
            {
                DueType = dueType;
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
        public DueType DueType { get; private set; }

        [Required]
        public Guid AssignedTo { get; private set; }

        [Required]
        public CategoryType CategoryType { get; private set; }

        [Required]
        public TaskStatus TaskStatus { get; private set; }
    }
}
