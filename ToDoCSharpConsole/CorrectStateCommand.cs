using System;
using System.Collections.Generic;
using ToDo;

namespace ToDoCSharpConsole
{    
    class CorrectStateCommand{
        private static Dictionary<AppState,CommandType> StateCommand = new Dictionary<AppState,CommandType> {
            {AppState.Load, CommandType.Load},
            {AppState.List, CommandType.List},
            {AppState.Task, CommandType.Task},
        };
        public static bool CommandAppState(CommandType type){
            return type == CommandType.Any || type == StateCommand[TaskTracker.state];
        }
    }
}
