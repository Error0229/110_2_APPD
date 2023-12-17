namespace WindowPowerPoint
{
    public interface ICommand
    {
        // execute command 
        void Execute();

        // unexecute command
        void Unexecute();
    }
}
