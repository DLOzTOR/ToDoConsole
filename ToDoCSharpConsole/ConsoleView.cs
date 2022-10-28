using System;
using System.Collections.Generic;
using ToDo;

namespace ToDoCSharpConsole
{
    class ConsoleView{
        private static string userInput;
        public static void StartConsole()
        {
            Console.WriteLine("DLOzTOR's task tracker\nUse \"help\" to watch commands list");
            ConsoleInput();
        }
        private static void readCommand(){
            Console.Write("> ");
            userInput = Console.ReadLine();
            Commands.runCommand(userInput);
        }
        private static void ConsoleInput(){
            while(TaskTracker.ShoodClose == false)
            {
                readCommand();
            }
        }

    }
}