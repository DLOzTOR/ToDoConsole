using System;
namespace ToDo
{
    public class Task{
        private int id;
        public DateTime date;
        public string Title;
        public bool Done;
        public Task(string Title, int id)
        {
            this.id = id; 
            this.Title = Title;
            this.Done = false;
            this.date = DateTime.Now;
        }
        public Task(string Title, int id, DateTime date, bool done)
        {
            this.id = id;
            this.Title = Title;
            this.Done = done;
            this.date = date;
        }

        public int getId(){
            return this.id;
        }
    }
}