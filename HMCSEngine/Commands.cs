namespace HMCSEngine
{
    internal static class Commands
    {
        public static void EnterCommandModeRequest()
        {
            Debug.CommandLog("Enter command mode? (y/n)");

            string? answer = Debug.GetConsoleInput();

            if (answer == null || answer != "y" || answer != "n")
            {
                Debug.CommandLog("Invalid response");
                return;
            }

            if(answer == "n")
            {
                return;
            }

            Debug.CommandLog("Entered command mode type \"escape\" to exit command mode");
        }        
    }
}
