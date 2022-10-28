namespace ToDoCSharpConsole
{
    class Command{
        private string name { get; set; }
        private string discription { get; set; }
        private CommandType type { get; set; }
        public Command(string name, string discription,CommandType type){
            this.name = name; 
            this.discription = discription;
            this.type = type;
        }
        public string getName(){
            return name;
        }
        public string getDiscription(){
            return discription;
        }
        public CommandType getType(){
            return type;
        }
        public virtual void Run() { 
        }
    }
}