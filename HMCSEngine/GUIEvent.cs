using System.Numerics;

namespace HMCSEngine
{
    internal abstract class GUIEvent
    {

    }

    internal class MouseGUIEvent : GUIEvent
    {
        public readonly MouseButtons MouseButton;
        public readonly Vector2 MousePosition;
        public readonly bool MouseDown;

        public MouseGUIEvent(MouseButtons mousebutton, Vector2 mouseposition, bool mousedown) : base()
        {
            MouseButton = mousebutton;
            MousePosition = mouseposition;
            MouseDown = mousedown;
        }
    }

    internal enum MouseButtons
    {
        None = 0,
        Left,
        Middle,
        Right,
        X1,
        X2
    }
}
