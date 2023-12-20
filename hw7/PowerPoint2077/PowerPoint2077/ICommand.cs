namespace WindowPowerPoint
{
    public interface ICommand : ISlide
    {
        // execute command 
        void Execute();

        // unexecute command
        void Unexecute();
    }
}
