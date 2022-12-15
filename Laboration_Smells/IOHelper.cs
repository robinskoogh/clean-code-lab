namespace Laboration_Smells
{
    public interface IOHelper
    {
        string PromptStringInput(string prompt, string defaultValue);
        void OutputMessage(string message);
        void ClearOutput();
    }
}
