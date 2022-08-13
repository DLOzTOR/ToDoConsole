using System;
using System.Collections.Generic;
using ToDo;

namespace ToDoCSharpConsole
{
    class ConsoleView{
        private static string userInput;
        private static void readCommand(){
            userInput = Console.ReadLine();
            ConsoleCommands.runCommand(userInput);
        }
        public static void StartConsole(){
            Console.WriteLine("DLOzTOR task tracker\nUse \"help\" to watch commands list");
            ConsoleInput();
        }
        private static void ConsoleInput(){
            while(true){
                readCommand();
            }
        }

    }
}