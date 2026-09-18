namespace HMCSEngine
{
    internal static class Commands
    {
        public const string HelpCommandList = "\r\n" + 
            "dumplogstofile - Creates a file in the \\Logs directory and dumps all the logs to it" + "\r\n" +
            "loadlevel [ID integer (0<=x<256)] - Loads the level that has the ID" + "\r\n" +
            "setlogfiling [Value bool] - Sets the debugger to dump logs into a file" + "\r\n" +
            "setwindowscale [Scale real (x>0)] - Sets the window scale" + "\r\n" +
            "spawnentity [ID integer (0<=x<256)] [Position (x,y)]  - Spawns entity that has the ID at position" + "\r\n" +

            "esc - exits command mode" + "\r\n" +
            "esc - exits command mode" + "\r\n" +
            "esc - exits command mode" + "\r\n" +

            "# - exits command mode" + "\r\n" +
            "? - shows a list of all commands and descriptions" + "\r\n";

        public static void CommandMode()
        {
            Debug.CommandLog("Entered command mode. type command \"?\" for help");

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
                object parameter2;

                switch (commandtokens[0])
                {
                    case "#":
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
                    case "setwindowscale":
                        if (commandtokens.Length != 2)
                        {
                            Debug.CommandLog($"The command entered had too many tokens for the command (token count: {commandtokens.Length})");
                            continue;
                        }

                        try
                        {
                            parameter1 = Convert.ToSingle(commandtokens[1]);
                        }
                        catch (FormatException e)
                        {
                            Debug.CommandLog($"Could not parse {commandtokens[1]} into a float because it was the wrong format (Exception: {e})");
                            continue;
                        }
                        catch (OverflowException e)
                        {
                            Debug.CommandLog($"Could not parse {commandtokens[1]} into a float because it was out of bounds for data type 'byte' (Exception: {e})");
                            continue;
                        }
                        
                        Screen.WindowScale = (float)(parameter1);
                        Debug.CommandLog($"Set window scale to {parameter1}");
                        break;
                    case "spawnentity":
                        if (commandtokens.Length != 3)
                        {
                            Debug.CommandLog($"The command entered had too many tokens for the command (token count: {commandtokens.Length})");
                            continue;
                        }

                        parameter1 = new EntityDataID(Convert.ToByte(commandtokens[1]));
                        parameter2 = TilePosition.StringToScreenPosition(commandtokens[2]);

                        Level.SpawnEntity((TilePosition)(parameter2), (EntityDataID)(parameter1));
                        break;
                    case "dumplogstofile":
                        Debug.CreateLogFile(true);
                        Debug.CommandLog($"Dumped all logs to file in directory {Files.LogsDirectory}");
                        break;
                    default:
                        Debug.CommandLog($"Could not identify command \"{commandtokens[0]}\"");
                        break;
                }
            }
        }
    }
}
