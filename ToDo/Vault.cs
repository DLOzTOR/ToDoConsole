using System;
using System.Collections;
using System.IO;
namespace ToDo
{
    public class Vault{
        public static string path;
        public static string name;
        private static bool vaultSet = false;
        public static void setVault(string path, string name){
            if(!vaultSet){
                Vault.path = path;
                Vault.name = name;
                vaultSet = true;
            }
            else 
                Console.WriteLine("Vault already set!\nUse restart(now don`t work) command too unset.");
        }
        public static void CreateVault(string path, string name){
            Vault.setVault(path, name);
            if(Directory.Exists(path))
            {
                Directory.CreateDirectory(path+@"\"+name);
                File.Create(path+@"\"+name+@"\"+"tasks.txt").Close();
                DateTime date = DateTime.Now;
                File.WriteAllText(path+@"\"+name+@"\"+"META.txt",string.Format("Vault create time: {0:hh-mm-ss}, date {0:dd-MM-yyyy}",date));
                TaskTracker.state=AppState.List;
                Console.WriteLine("done");
            }
            else 
                Console.WriteLine("Directory doethn't exist");
        }
        public static void LoadVault(string path, string name) {
            if (Directory.Exists(path + @"\" + name) &&
                File.Exists(path + @"\" + name + @"\" + "tasks.txt") &&
                File.Exists(path + @"\" + name + @"\" + "META.txt"))
            {
                Vault.path = path;
                Vault.name = name;
                SaveLoad.LoadTasks(path + @"\" + name + @"\" + "tasks.txt");
                TaskTracker.state = AppState.List;
                Console.WriteLine("Loaded");
            }
            else
                Console.WriteLine("Vault damaged or doethn't exis");
        }
    }
}