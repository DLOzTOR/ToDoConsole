using System;
using System.Collections.Generic;
using ToDo;

namespace ToDoCSharpConsole
{
    class ConsoleCommands{
        private static int index;
        private static void Help(){
            var commandList = new List<Command> {
                    new Command("help", "show command list", CommandType.Any),
                    new Command("clear", "clean console", CommandType.Any),
                    new Command("newVault", "create vault", CommandType.Load),
                    new Command("loadVault", "load vault", CommandType.Load),
                    new Command("new", "create new task", CommandType.List),
                    new Command("watch", "show task list", CommandType.List),
                    new Command("open", "open task for edit", CommandType.List),
                    new Command("close", "close task", CommandType.Task),
                    new Command("delete", "delete task", CommandType.Task),
                    new Command("changeDone","change done state",CommandType.Task),
                    new Command("changeName","change name",CommandType.Task),
            };
            foreach (var item in commandList){
                if(CorrectStateCommand.CommandAppState(item.getType()))
                    Console.WriteLine("{0} - {1}",item.getName(),item.getDiscription());
            }
        }
        private static void Claer(){
            Console.Clear();
        }
        private static void createTask(){
            
        }
        private static void wrongState() {
            Console.WriteLine("Command can't run from this app state.");
        }
        public static void runCommand(string userInput){//Выделить каждый кейс в метод, а методы в класс
            switch(userInput){
                case("help"):
                    Help();
                    break;
                case("clear"):
                    Claer();
                    break;
                case("newVault"):
                    if (TaskTracker.state == AppState.Load)
                    {
                        Console.WriteLine("Write directory path: ");
                        string path = "", name = "";
                        path = Console.ReadLine();
                        Console.WriteLine("Write directory name: ");
                        name = Console.ReadLine();
                        Vault.CreateVault(path, name);
                    }
                    else
                        wrongState();
                    break;
                case ("loadVault"):
                    if (TaskTracker.state == AppState.Load) {
                        Console.WriteLine("Write directory path: ");
                        string path = "", name = "";
                        path = Console.ReadLine();
                        Console.WriteLine("Write directory name: ");
                        name = Console.ReadLine();
                        Vault.LoadVault(path, name);
                    }
                    else
                        wrongState();
                    break;
                case("new"):
                    if (TaskTracker.state == AppState.List)
                    {
                        Console.WriteLine("Write task name: ");
                        string name = "";
                        name = Console.ReadLine();
                        TaskTracker.CreateTask(name);
                    }
                    else
                        wrongState();
                    break;
                case ("watch"):
                    foreach (var item in TaskTracker.taskList)
                        Console.WriteLine("id: {0}, name: {1}, done: {2}", item.getId(), item.Title, item.Done);
                    break;
                case ("open"):
                    if (TaskTracker.state == AppState.List)
                    {
                        Console.WriteLine("Write task id: ");
                        index = TaskTracker.findTask(Convert.ToInt32(Console.ReadLine()));
                        TaskTracker.state = AppState.Task;
                        Console.WriteLine("Name: {0}, done: {1}", TaskTracker.taskList[index].Title, TaskTracker.taskList[index].Done);
                    }
                    else
                        wrongState();
                    break;
                case ("close"):
                    if (TaskTracker.state == AppState.Task)
                    {
                        TaskTracker.state = AppState.List;
                        Console.WriteLine("Closed");
                    }
                    else
                        wrongState();
                    break;
                case ("changeDone"):
                    if (TaskTracker.state == AppState.Task)
                    {
                        TaskTracker.taskList[index].Done = !TaskTracker.taskList[index].Done;
                        Console.WriteLine("Name: {0}, done: {1}", TaskTracker.taskList[index].Title, TaskTracker.taskList[index].Done);
                    }
                    else
                        wrongState();
                    break;
                case ("changeName"):
                    if (TaskTracker.state == AppState.Task)
                    {
                        Console.WriteLine("Write new name");
                        TaskTracker.taskList[index].Title = Console.ReadLine();
                        Console.WriteLine("Name: {0}, done: {1}", TaskTracker.taskList[index].Title, TaskTracker.taskList[index].Done);
                    }
                    else
                        wrongState();
                    break;
                case ("delete"):
                    if (TaskTracker.state == AppState.Task)
                    {
                        TaskTracker.taskList.Remove(TaskTracker.taskList[index]);
                        TaskTracker.state = AppState.List;
                        Console.WriteLine("Deleted");
                    }
                    else
                        wrongState();
                    break;
                default:
                    Console.WriteLine("Unknown command: \"{0}\", use \"help\" to watch list of commands",userInput);
                    break;
            }
        }
    }
}