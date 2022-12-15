using Application.Interfaces;

namespace Application.Helpers
{
    public class ConsoleHelper : IIOHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prompt">Prompt message for required user input</param>
        /// <param name="defaultValue">The default value that will be returned if the input value is null, empty or white space. Default value is an empty string</param>
        /// <returns></returns>
        public string PromptStringInput(string prompt, string defaultValue = "")
        {
            Console.Write(prompt);
            string? userInput = Console.ReadLine();

            //return string.IsNullOrWhiteSpace(userInput = Console.ReadLine()) ? "anonymous" : userInput;

            if (string.IsNullOrWhiteSpace(userInput))
                userInput = defaultValue;

            return userInput;
        }

        public void OutputMessage(string message) => Console.WriteLine(message);

        public void ClearOutput() => Console.Clear();
    }
}
