namespace HMCSEngine
{
    internal static class Commands
    {
        public const string HelpCommandList = "\r\n" + 
            "dumplogstofile - Creates a file in the \\Logs directory and dumps all the logs to it" + "\r\n" +
            "loadlevel [ID integer (0<=x<256)] - Loads the level that has the ID" + "\r\n" +
            "setlogfiling [Value boolean] - Sets the debugger to dump logs into a file" + "\r\n" +
            "setwindowscale [Scale real (x>0)] - Sets the window scale" + "\r\n" +
            "settimescale [Scale real (x>0)] - Sets the time scale" + "\r\n" +
            "showdebugtext [boolean Value] - When set to true it will show the debug text" + "\r\n" +
            "showtilelayer [Layer ID integer (0<=x<3)] [Value boolean]  - Sets the visibility of a specific tile layer" + "\r\n" +
            "spawnentity [ID integer (0<=x<256)] [Position format:\"(x,y)\"]  - Spawns entity that has the ID at position" + "\r\n" +
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

                try
                {
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

                            parameter1 = Convert.ToBoolean(commandtokens[1]);

                            Debug.LogFiling = (bool)(parameter1);
                            Debug.CommandLog($"Set LogFiling to {parameter1}");
                            break;
                        case "loadlevel":
                            if (commandtokens.Length != 2)
                            {
                                Debug.CommandLog($"The command entered had too many tokens for the command (token count: {commandtokens.Length})");
                                continue;
                            }

                            parameter1 = Convert.ToByte(commandtokens[1]);

                            Level.LoadLevel((byte)(parameter1));
                            Debug.CommandLog($"Loaded level {parameter1}");
                            break;
                        case "setwindowscale":
                            if (commandtokens.Length != 2)
                            {
                                Debug.CommandLog($"The command entered had too many tokens for the command (token count: {commandtokens.Length})");
                                continue;
                            }

                            parameter1 = Convert.ToSingle(commandtokens[1]);

                            if ((float)(parameter1) < 0)
                            {
                                Debug.CommandLog($"Parameter1 was out of bounds");
                                continue;
                            }

                            Screen.WindowScale = (float)(parameter1);
                            Debug.CommandLog($"Set window scale to {parameter1}");
                            break;
                        case "settimescale":
                            if (commandtokens.Length != 2)
                            {
                                Debug.CommandLog($"The command entered had too many tokens for the command (token count: {commandtokens.Length})");
                                continue;
                            }

                            parameter1 = Convert.ToSingle(commandtokens[1]);

                            if((float)(parameter1) < 0)
                            {
                                Debug.CommandLog($"Parameter1 was out of bounds");
                                continue;
                            }

                            Time.TimeScale = (float)(parameter1);
                            Debug.CommandLog($"Set time scale to {parameter1}");
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
                        
                            Debug.CommandLog($"Spawned entity {parameter1} at position {parameter2}");
                            break;
                        case "showdebugtext":
                            if (commandtokens.Length != 2)
                            {
                                Debug.CommandLog($"The command entered had too many tokens for the command (token count: {commandtokens.Length})");
                                continue;
                            }

                            parameter1 = Convert.ToBoolean(commandtokens[1]);

                            Debug.DrawDebugText = (bool)(parameter1);
                            Debug.CommandLog($"Set ShowDebugText to {parameter1}");
                            break;
                        case "dumplogstofile":
                            Debug.CreateLogFile(true);
                            Debug.CommandLog($"Dumped all logs to file in directory {Files.LogsDirectory}");
                            break;
                        case "showtilelayer":
                            if (commandtokens.Length != 3)
                            {
                                Debug.CommandLog($"The command entered had too many tokens for the command (token count: {commandtokens.Length})");
                                continue;
                            }

                            parameter1 = Convert.ToInt32(commandtokens[1]);

                            if((int)(parameter1) < 0 || (int)(parameter1) >= 3)
                            {
                                Debug.CommandLog("Tile layer ID was out of range");
                                continue;
                            }

                            parameter2 = Convert.ToBoolean(commandtokens[2]);

                            switch ((TileLayerName)(parameter1))
                            {
                                case TileLayerName.Downer:
                                    Renderer.ToggleDowner = (bool)(parameter2);
                                    break;
                                case TileLayerName.Upper:
                                    Renderer.ToggleUpper = (bool)(parameter2);
                                    break;
                                case TileLayerName.Higher:
                                    Renderer.ToggleHigher = (bool)(parameter2);
                                    break;
                            }

                            Debug.CommandLog($"Set tile layer {(TileLayerName)(parameter1)} visibility to {parameter2}");
                            break;
                        default:
                            Debug.CommandLog($"Could not identify command \"{commandtokens[0]}\"");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Debug.CommandLog("A parameter in the command could not be parsed");
                }
            }
        }
    }
}
