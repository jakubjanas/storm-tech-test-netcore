using Microsoft.AspNetCore.Identity;
using System;

namespace Todo.Data.Entities
{
    public class TodoItem
    {
        public int TodoItemId { get; set; }
        public string Title { get; set; }
        public string ResponsiblePartyId { get; set; }
        public IdentityUser ResponsibleParty { get; set; }
        public bool IsDone { get; set; }
        public Importance Importance { get; set; }
        public int Rank { get; private set; }

        public int TodoListId { get; set; }
        public TodoList TodoList { get; set; }

        protected TodoItem() { }

        public TodoItem(int todoListId, string responsiblePartyId, string title, Importance importance, int rank = 1)
        {
            TodoListId = todoListId;
            ResponsiblePartyId = responsiblePartyId;
            Title = title;
            Importance = importance;
            SetRank(rank);
        }

        public void SetRank(int rank)
        {
            if (rank < 1)
                throw new ArgumentOutOfRangeException("Wrong rank value! Rank value should be grater then 0!");

            Rank = rank;
        }
    }
}