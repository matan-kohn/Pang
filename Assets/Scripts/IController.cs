
    using DefaultNamespace;

    public interface IController
    {
        void ProcessCommand(CommandType commandType, params object[] data);
    }