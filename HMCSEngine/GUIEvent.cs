namespace HMCSEngine
{
    public abstract class GUIEvent
    {

    }

    public sealed class MouseEvent : GUIEvent
    {
        public readonly MouseButtons MouseButton;
        public readonly KeyState MouseButtonState;
        public readonly ScreenPosition CursorPosition;

        public bool MouseDown => (MouseButtonState == KeyState.Down) || (MouseButtonState == KeyState.Pressed);

        public MouseEvent(MouseButtons mousebutton, KeyState buttonstate, ScreenPosition cursorposition) : base()
        {
            MouseButton = mousebutton;
            MouseButtonState = buttonstate;
            CursorPosition = cursorposition;
        }

        public override string ToString()
        {
            return $"MouseEvents (MouseButton:{MouseButton}, MouseButtonState:{MouseButtonState}, CursorPosition:{CursorPosition})";
        }
    }

    public enum MouseButtons
    {
        None = 0,
        Left = 1,
        Right,
        Middle = 4,
        X1,
        X2
    }
}
