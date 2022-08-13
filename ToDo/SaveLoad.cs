using System;
using System.Collections.Generic;
using System.IO;
namespace ToDo
{
    public class SaveLoad
    {   
        public static void SaveTasks(string path)
        {
            string[] strings = new string[TaskTracker.taskList.Count];
            for(int i = 0; i < TaskTracker.taskList.Count; i++)
            {
                strings[i] = string.Format("id:{0},name:{1},done:{2},date:{3}", TaskTracker.taskList[i].getId(), TaskTracker.taskList[i].Title, TaskTracker.taskList[i].Done, TaskTracker.taskList[i].date.ToString());
            }
            File.WriteAllLines(path, strings);
        }
        public static void LoadTasks(string path)
        {
            string[] strings;
            strings = File.ReadAllLines(path);
            for (int i = 0; i < strings.Length; i++) { 
                var temp = strings[i].Split(",");
                TaskTracker.LoadTask( temp[1].Substring(5), Convert.ToInt32(temp[0].Substring(3)), temp[3].Substring(5), Convert.ToBoolean(temp[2].Substring(5)));
            }
            foreach (var item in TaskTracker.taskList) {
                Console.WriteLine("id: {0}, name: {1}, done: {2}", item.getId(), item.Title, item.Done);
            }
        }

    }
}
