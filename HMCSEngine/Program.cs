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

        GUIButton button = new GUIButton(new Rectangle(10, 10, 30, 30), ClickTest);
        GUI.AddElement(button);

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

    public static void ClickTest()
    {
        Debug.InfoLog("Click (:");
    }
}