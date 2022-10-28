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
            new NewVault("newVault", "create vault", CommandType.Load),
            new LoadVault("loadVault", "load vault by paht", CommandType.Load),
            new NewTask("new", "create new task", CommandType.List),
            new Watch("watch", "show task list", CommandType.List),
            new Open("open", "open task for edit", CommandType.List),
            new Close("close", "close task", CommandType.Task),
            new Delete("delete", "delete task", CommandType.Task),
            new ChangeState("changeState","change state",CommandType.Task),
            new ChangeName("changeName","change name",CommandType.Task),
            new RecentVaults("recent","open recent vault", CommandType.Load),
        };
        public static void runCommand(string userInput) {
            Command curentCommand = null;
            if (commandList.Where(x => x.getName() == userInput).Any()) curentCommand = commandList.Where(x => x.getName() == userInput).First();
            if (curentCommand == null) Console.WriteLine("Unknown command: \"{0}\", use \"help\" to watch list of commands", userInput);
            else curentCommand.Run();
        }
    }
    class Help : Command
    {
        public Help(string name, string discription, CommandType type) : base(name, discription, type)
        {
        }
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
                Console.WriteLine("Command can't run from this app state.");
        }
    }
    class LoadVault : Command
    {
        public LoadVault(string name, string discription, CommandType type) : base(name, discription, type) { }
        public override void Run()
        {
            if (TaskTracker.state == AppState.Load)
            {
                Console.WriteLine("Write directory path: ");
                string path = "", name = "";
                path = Console.ReadLine();
                Console.WriteLine("Write directory name: ");
                name = Console.ReadLine();
                Vault.LoadVault(path, name);
            }
            else
                Console.WriteLine("Command can't run from this app state.");
        }
    }
    class NewTask : Command
    {
        public NewTask(string name, string discription, CommandType type) : base(name, discription, type) { }
        public override void Run()
        {
            if (TaskTracker.state == AppState.List)
            {
                Console.WriteLine("Write task name: ");
                string name = "";
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
            foreach (var item in TaskTracker.taskList)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write($"Id: {item.getId()}");
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write($" Name: {item.Title}");
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write($" Done: {item.State}");
                Console.ResetColor();
                Console.WriteLine();
            }
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
                Commands.index = TaskTracker.findTask(Convert.ToInt32(Console.ReadLine()));
                TaskTracker.state = AppState.Task;
                Console.WriteLine("Name: {0}, done: {1}", TaskTracker.taskList[Commands.index].Title, TaskTracker.taskList[Commands.index].State);
            }
            else
                Console.WriteLine("Command can't run from this app state.");
        }
    }
    class Close : Command
    {
        public Close(string name, string discription, CommandType type) : base(name, discription, type) { }

        public override void Run()
        {
            if (TaskTracker.state == AppState.Task)
            {
                TaskTracker.state = AppState.List;
                Console.WriteLine("Closed");
            }
            else
                Console.WriteLine("Command can't run from this app state.");
        }
    }
    class Delete : Command
    {
        public Delete(string name, string discription, CommandType type) : base(name, discription, type){}
        public override void Run()
        {
            if (TaskTracker.state == AppState.Task)
            {
                TaskTracker.taskList.Remove(TaskTracker.taskList[Commands.index]);
                TaskTracker.state = AppState.List;
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
        public ChangeName(string name, string discription, CommandType type) : base(name, discription, type){}
        public override void Run()
        {
            if (TaskTracker.state == AppState.Task)
            {
                Console.WriteLine("Write new name");
                TaskTracker.taskList[Commands.index].Title = Console.ReadLine();
                Console.WriteLine("Name: {0}, done: {1}", TaskTracker.taskList[Commands.index].Title, TaskTracker.taskList[Commands.index].State);
            }
            else
                Console.WriteLine("Command can't run from this app state.");
        }
    }
    class RecentVaults : Command
    {
        public RecentVaults(string name, string discription, CommandType type) : base(name, discription, type){}
        public override void Run()
        {
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
                id = Convert.ToInt32(Console.ReadLine());
                if (id >= 0 || id > recentVaults.Length) break;
                Console.WriteLine("Wrong id");
            } while (true);
            string[] pathName;
            pathName = recentVaults[id].Split("|");
            Vault.LoadVault(pathName[0], pathName[1]);
        }
    }
}