namespace HMCSEngine
{
    internal abstract class GUIEvent
    {

    }

    internal sealed class MouseGUIEvent : GUIEvent
    {
        public readonly MouseButtons MouseButton;
        public readonly KeyState MouseButtonState;
        public readonly ScreenPosition CursorPosition;

        public MouseGUIEvent(MouseButtons mousebutton, KeyState buttonstate, ScreenPosition cursorposition) : base()
        {
            MouseButton = mousebutton;
            MouseButtonState = buttonstate;
            CursorPosition = cursorposition;
        }
    }

    internal enum MouseButtons
    {
        None = 0,
        Left = 1,
        Right,
        Middle = 4,
        X1,
        X2
    }
}
