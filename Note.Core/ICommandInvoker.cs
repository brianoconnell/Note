namespace Note.Core
{
    public interface ICommandInvoker
    {
        void Execute<T>(T command);
    }
}