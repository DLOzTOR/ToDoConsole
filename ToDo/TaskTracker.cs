using System;
using System.Collections.Generic;
namespace ToDo
{
    public static class TaskTracker{
        private static int taskCount = 0;
        public static List<Task> taskList = new List<Task>();
        public static AppState state = AppState.Load;
        public static void CreateTask(string title)
        {
            taskList.Add(new Task(title, taskCount));
            SaveLoad.SaveTasks(Vault.path + @"\" + Vault.name + @"\" + "tasks.txt");
            taskCount++;
        }
        public static void LoadTask(string title, int id, string datestr, bool done)
        {
            DateTime date;
            DateTime.TryParse(datestr, out date);
            taskList.Add(new Task(title, id, date, done));
            taskCount++;
        }
        public static int findTask(int Id)
        {
            for(int i = 0; i < taskList.Count; i++)
            {
                if (taskList[i].getId() == Id) return i;
            }
            return -1;
        }
    }
}