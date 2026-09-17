namespace HMCSEngine
{
    internal static class Commands
    {
        public const string HelpCommandList =
            "\r\nsetlogfiling [bool] - sets the debugger to dump logs into a file" + "\r\n" +
            "escape - exits command mode" + "\r\n" +
            "? - shows a list of all commands and descriptions" + "\r\n";

        public static void EnterCommandModeRequest()
        {
            Debug.CommandLog("Enter command mode? (y/n)");

            string? answer = Debug.GetConsoleInput();
            
            if (answer == null || (answer != "y" && answer != "n"))
            {
                Debug.CommandLog("Invalid response");
                return;
            }


            if(answer == "n")
            {
                return;
            }

            Debug.CommandLog("Entered command mode type \"?\" for help");

            CommandModeLoop();
        }

        private static void CommandModeLoop()
        {
            while (true)
            {
                string? command = Debug.GetConsoleInput();

                if(string.IsNullOrEmpty(command))
                {
                    Debug.CommandLog("Command entered was either null or empty");
                    continue;
                }

                command = command.ToLower();

                string[] commandtokens = command.Split(' ');

                object parameter1;

                switch (commandtokens[0])
                {
                    case "escape":
                        Debug.CommandLog("Exiting command mode");                        
                        return;
                    case "?":
                        Debug.CommandLog($"List of commands: \r\n{HelpCommandList}");
                        break;
                    case "setlogfiling":
                        if(commandtokens.Length != 2)
                        {
                            Debug.CommandLog($"The command entered had too many tokens for the command (token count: {commandtokens.Length})");
                            continue;
                        }

                        try
                        {
                            parameter1 = Convert.ToBoolean(commandtokens[1]);
                        }
                        catch (FormatException e)
                        {
                            Debug.CommandLog($"Could not parse {commandtokens[1]} into a boolean (Exception: {e})");
                            continue;
                        }

                        Debug.LogFiling = (bool)(parameter1);
                        Debug.CommandLog($"Set LogFiling to {parameter1}");
                        break;
                    case "loadlevel":
                        if (commandtokens.Length != 2)
                        {
                            Debug.CommandLog($"The command entered had too many tokens for the command (token count: {commandtokens.Length})");
                            continue;
                        }

                        try
                        {
                            parameter1 = Convert.ToByte(commandtokens[1]);
                        }
                        catch (FormatException e)
                        {
                            Debug.CommandLog($"Could not parse {commandtokens[1]} into a byte because it was the wrong format (Exception: {e})");
                            continue;
                        }
                        catch (OverflowException e)
                        {
                            Debug.CommandLog($"Could not parse {commandtokens[1]} into a byte because it was out of bounds for data type 'byte' (Exception: {e})");
                            continue;
                        }

                        Level.LoadLevel((byte)(parameter1));
                        Debug.CommandLog($"Loaded level {parameter1}");
                        break;
                    case "spawnentity":
                        break;
                    default:
                        Debug.CommandLog($"Could not identify command \"{commandtokens[0]}\"");
                        break;
                }
            }
        }


    }
}
