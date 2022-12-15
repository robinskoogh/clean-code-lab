namespace Application.Interfaces
{
    public interface IIOHelper
    {
        string PromptStringInput(string prompt, string defaultValue);
        void OutputMessage(string message);
        void ClearOutput();
    }
}
