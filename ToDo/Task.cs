using System;
namespace ToDo
{
    public class Task{
        private int id;
        public DateTime date;
        public string Title;
        public bool State;
        public Task(string Title, int id)
        {
            this.id = id; 
            this.Title = Title;
            this.State = false;
            this.date = DateTime.Now;
        }
        public Task(string Title, int id, DateTime date, bool done)
        {
            this.id = id;
            this.Title = Title;
            this.State = done;
            this.date = date;
        }

        public int getId(){
            return this.id;
        }
    }
}