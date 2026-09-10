using HMCSEngine;
using Raylib_cs;

class Program
{
    private static void Main()
    {
        HMCS.SetWindowTitle("HYPNOTISED-MIND CONTROL-SIMULATOR", true);
        HMCS.Initalize();

        HMCS.LoadLevel(0);

        Fonts.LoadFont(new FontInfo("comic", 0));

        //GUIText text = new GUIText(ScreenPosition.One * 10, "Good morning", 32, 0);
        //GUI.AddElement(text);

        while (Raylib.WindowShouldClose() == false)
        {
            HMCS.Update();

            if (Inputs.KeyPressed(VKeyCodes.Function1))
            {
                HMCS.LoadLevel(0);
            }
        }

        HMCS.Quit();
    }
}