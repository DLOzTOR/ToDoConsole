using System;
using System.Linq;
using System.Collections.Generic;
using ToDo;

namespace ToDoCSharpConsole
{
    class Commands {
        public static int index;
        public static List<Command> commandList = new List<Command> {
            new Help("help", "show command list", CommandType.Any),
            new Clear("clear", "clean console", CommandType.Any),
            new NewVault("create", "create vault", CommandType.Load),
            new LoadVault("load", "load vault by path", CommandType.Load),
            new RecentVaults("recent","open recent vault", CommandType.Load),
            new Exit("exit","close app", CommandType.Load),
            new NewTask("new", "create new task", CommandType.List),
            new Watch("watch", "show task list", CommandType.List),
            new Open("open", "open task for edit", CommandType.List),
            new Exit("exit","go to previus app level", CommandType.List),
            new Delete("delete", "delete task", CommandType.Task),
            new ChangeState("state","change state",CommandType.Task),
            new ChangeName("name","change name",CommandType.Task),
            new Exit("exit", "close task", CommandType.Task),
        };
        public static void runCommand(string userInput) {
            Command curentCommand = null;
            if (commandList.Where(x => x.getName() == userInput && CorrectStateCommand.CommandAppState(x.getType())).Any()) curentCommand = commandList.Where(x => x.getName() == userInput && CorrectStateCommand.CommandAppState(x.getType())).First();
            if (curentCommand == null) Console.WriteLine("Unknown command: \"{0}\", use \"help\" to watch list of commands", userInput);
            else curentCommand.Run();
        }
    }
    class Help : Command
    {
        public Help(string name, string discription, CommandType type) : base(name, discription, type) { }
        public override void Run()
        {
            foreach (var item in Commands.commandList)
            {
                if (CorrectStateCommand.CommandAppState(item.getType()))
                    Console.WriteLine("{0} - {1}", item.getName(), item.getDiscription());
            }
        }
    }
    class Clear : Command
    {
        public Clear(string name, string discription, CommandType type) : base(name, discription, type) { }
        public override void Run()
        {
            Console.Clear();
        }
    }
    class NewVault : Command
    {
        public NewVault(string name, string discription, CommandType type) : base(name, discription, type) { }
        public override void Run()
        {
            Console.Clear();
            if (TaskTracker.state == AppState.Load)
            {

                Console.WriteLine("Write directory path: ");
                string path = "", name = "";
                Console.Write("> ");
                path = Console.ReadLine();
                Console.WriteLine("Write directory name: ");
                Console.Write("> ");
                name = Console.ReadLine();
                Vault.CreateVault(path, name);
            }
            else
                Console.WriteLine("Command can't run from this app state.");

        }
    }
    class LoadVault : Command
    {
        public LoadVault(string name, string discription, CommandType type) : base(name, discription, type) { }
        public override void Run()
        {
            Console.Clear();
            if (TaskTracker.state == AppState.Load)
            {
                Console.WriteLine("Write directory path: ");
                string path = "", name = "";
                Console.Write("> ");
                path = Console.ReadLine();
                Console.WriteLine("Write directory name: ");
                Console.Write("> ");
                name = Console.ReadLine();
                Vault.LoadVault(path, name);
            }
            else
                Console.WriteLine("Command can't run from this app state.");

        }
    }
    class RecentVaults : Command
    {
        public RecentVaults(string name, string discription, CommandType type) : base(name, discription, type) { }
        public override void Run()
        {
            Console.Clear();
            string[] recentVaults = Vault.RecentVaults();
            if (recentVaults.Length == 0) {
                Console.WriteLine("Recent vaults doethn't find.\nUse LoadVault to load by path.");
                return;
            }
            for (int i = 0; i < recentVaults.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write($"id:{i} ");
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine(recentVaults[i]);
                Console.ResetColor();
            }
            Console.WriteLine("write vault id:");
            int id;
            do
            {

                try {
                    Console.Write("> ");
                    id = Convert.ToInt32(Console.ReadLine());
                    if (id >= 0 || id > recentVaults.Length) break;
                    Console.WriteLine("Wrong id");
                }
                catch { Console.WriteLine("This not a number."); }
            } while (true);
            string[] pathName;
            pathName = recentVaults[id].Split("|");
            Vault.LoadVault(pathName[0], pathName[1]);
        }
    }
    class NewTask : Command
    {
        public NewTask(string name, string discription, CommandType type) : base(name, discription, type) { }
        public override void Run()
        {
            Console.Clear();
            if (TaskTracker.state == AppState.List)
            {
                Console.WriteLine("Write task name: ");
                string name = "";
                Console.Write("> ");
                name = Console.ReadLine();
                TaskTracker.CreateTask(name);
                Console.WriteLine("Created");
            }
            else
                Console.WriteLine("Command can't run from this app state.");
        }
    }
    class Watch : Command
    {
        public Watch(string name, string discription, CommandType type) : base(name, discription, type) { }
        public override void Run()
        {
            Console.Clear();
            foreach (var item in TaskTracker.taskList)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write($"Id: {item.getId()}");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Write($" Name: {item.Title}");
                if (item.State == false) Console.BackgroundColor = ConsoleColor.Red;
                else Console.BackgroundColor = ConsoleColor.Green;
                Console.Write($" Done: {item.State}");
                Console.ResetColor();
                Console.Write(string.Format(" Time: {0:hh-mm-ss} Date {0:dd-MM-yyyy}", item.date));
                Console.WriteLine();
            }
            if (TaskTracker.taskList.Count == 0) Console.WriteLine("Task list is empty.");
        }
    }
    class Open : Command
    {
        public Open(string name, string discription, CommandType type) : base(name, discription, type) { }
        public override void Run()
        {
            if (TaskTracker.state == AppState.List)
            {
                Console.WriteLine("Write task id: ");
                Console.Write("> ");
                Commands.index = TaskTracker.findTask(Convert.ToInt32(Console.ReadLine()));
                TaskTracker.state = AppState.Task;
                Console.WriteLine("Name: {0}, done: {1}", TaskTracker.taskList[Commands.index].Title, TaskTracker.taskList[Commands.index].State);
            }
            else
                Console.WriteLine("Command can't run from this app state.");
        }
    }
    class Delete : Command
    {
        public Delete(string name, string discription, CommandType type) : base(name, discription, type) { }
        public override void Run()
        {
            Console.Clear();
            if (TaskTracker.state == AppState.Task)
            {
                TaskTracker.taskList.Remove(TaskTracker.taskList[Commands.index]);
                TaskTracker.state = AppState.List;
                SaveLoad.SaveTasks(Vault.path + @"\" + Vault.name + @"\" + "tasks.txt");
                Console.WriteLine("Deleted");
            }
            else
                Console.WriteLine("Command can't run from this app state.");
        }
    }
    class ChangeState : Command
    {
        public ChangeState(string name, string discription, CommandType type) : base(name, discription, type) { }

        public override void Run()
        {
            Console.Clear();
            if (TaskTracker.state == AppState.Task)
            {
                TaskTracker.taskList[Commands.index].State = !TaskTracker.taskList[Commands.index].State;
                Console.WriteLine("Name: {0}, done: {1}", TaskTracker.taskList[Commands.index].Title, TaskTracker.taskList[Commands.index].State);
            }
            else
                Console.WriteLine("Command can't run from this app state.");
        }
    }
    class ChangeName : Command
    {
        public ChangeName(string name, string discription, CommandType type) : base(name, discription, type) { }
        public override void Run()
        {
            Console.Clear();
            if (TaskTracker.state == AppState.Task)
            {
                Console.WriteLine("Write new name");
                Console.Write("> ");
                TaskTracker.taskList[Commands.index].Title = Console.ReadLine();
                Console.WriteLine("Name: {0}, done: {1}", TaskTracker.taskList[Commands.index].Title, TaskTracker.taskList[Commands.index].State);
            }
            else
                Console.WriteLine("Command can't run from this app state.");
        }
    }
    class Exit : Command
    {
        public Exit(string name, string discription, CommandType type) : base(name, discription, type){}
        public override void Run()
        {
            Console.Clear();
            if (TaskTracker.state == AppState.Load) { TaskTracker.ShoodClose = true; return;}
            Console.Clear();
            if (TaskTracker.state == AppState.Task)
            {
                TaskTracker.state = AppState.List;
                Commands.runCommand("watch");
                return;
            }
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("You return to vault select screen");
            Console.ResetColor();
            TaskTracker.taskList = new List<Task>();
            TaskTracker.taskCount = 0;
            TaskTracker.state = AppState.Load;
        }
    }
}