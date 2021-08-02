using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Data.Entities;
using Xunit;

namespace Todo.Tests
{
    public class WhenToDoItemIsCreated
    {
        private readonly TodoItem todoItem;

        public WhenToDoItemIsCreated()
        {
            var todoList = new TestTodoListBuilder(new IdentityUser("alice@example.com"), "shopping")
                .WithItem("bread", Importance.High)
                .Build();

            todoItem = todoList.Items.First();
        }

        [Fact]
        public void ShoudlSetDefaultRankWhenObjectIsCreated()
        {
            var defaultRank = 1;

            Assert.Equal(defaultRank, todoItem.Rank);
        }

        [Fact]
        public void ShoudUpdateRankValueOnSetRank()
        {
            var newValue = 15;

            todoItem.SetRank(newValue);

            Assert.Equal(newValue, todoItem.Rank);
        }

        [Fact]
        public void ShoudThrowArgumentOutOfRangeExeptionOnSetRankWhenRankValueIsLessThanOne()
        {
            var newValue = 0;

            Assert.Throws<ArgumentOutOfRangeException>(() => todoItem.SetRank(newValue));
        }

        [Fact]
        public void ShouldThrowArgumentOutOfRangeExceptionOnObjectCreationWhenPassedRankValueIsLessThenOne()
        {
            var rankValue = -1;

            Assert.Throws<ArgumentOutOfRangeException>(() => new TodoItem(1, "testId", "title", Importance.High, rankValue));
        }
    }
}
