namespace Infrastructure.ScenesServices.MainMenuPart.Signals
{
    public class WindowAction
    {
        public Action MakeType { get; }
        public WindowType Type { get; }

        public WindowAction(Action makeType, WindowType type)
        {
            MakeType = makeType;
            Type = type;
        }
        
        public enum Action
        {
            Close, Open
        }
        
        public enum WindowType
        {
            Setting, About, Profile
        }
    }
}