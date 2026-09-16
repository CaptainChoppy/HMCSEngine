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

        TestButton button = new TestButton(new Rectangle(10, 10, 10, 10));

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

internal class TestButton : GUIButton
{
    public TestButton(Rectangle rect) : base(rect)
    {
        ColourTint = new Color(0xFF, 0x00, 0xFF, 0xFF);
    }

    public override void MouseEnter(MouseGUIEvent mouseevents)
    {
        ColourTint = new Color(0xFF, 0x00, 0x00, 0xFF);
    }

    public override void MouseHover(MouseGUIEvent mouseevents)
    {
        ColourTint = new Color(0x00, 0xFF, 0x00, 0xFF);
    }

    public override void MouseExit(MouseGUIEvent mouseevents)
    {
        ColourTint = new Color(0x00, 0x00, 0xFF, 0xFF);

    }
}