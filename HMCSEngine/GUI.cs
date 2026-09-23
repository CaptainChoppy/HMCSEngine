using Raylib_cs;

namespace HMCSEngine
{
    public static class GUI
    {
        private static readonly List<GUIElement> Elements = new List<GUIElement>();

        private static MouseEvent CurrentMouseEvents = new MouseEvent(MouseButtons.None, KeyState.Up, ScreenPosition.Zero);

        public static void Draw()
        {
            foreach(GUIElement e in Elements)
            {
                e.Draw();
            }
        }

        public static void Update()
        {
            CurrentMouseEvents = GetMouseEvents();

            foreach(GUIElement e in Elements)
            {
                e.CursorEvents(CurrentMouseEvents);
            }
        }

        private static MouseEvent GetMouseEvents()
        {
            MouseButtons button = Inputs.GetMouseButton();

            return new MouseEvent(button, Inputs.GetMouseButtonState(button), Cursor.Position);
        }

        public static void AddElement(GUIElement element)
        {
            Elements.Add(element);
            Debug.InfoLog("Added GUIElement");
        }

        public static void RemoveElement(GUIElement element)
        {
            Elements.Remove(element);
            Debug.InfoLog("Removed GUIElement");
        }
    }

    public enum GUIElementHoverState : int
    {
        Out,
        Enter,
        Hover,
        Exit
    }

    public abstract class GUIElement
    {
        public Rectangle Rect;
        public Color ColourTint = Color.White;
        public int DrawOrder = 0;

        public GUIElementHoverState HoverState { get; private set; } = GUIElementHoverState.Out;

        public GUIElement(Rectangle rect)
        {
            Rect = rect;
            GUI.AddElement(this);
        }

        public virtual void Draw()
        {
            Renderer.DrawRectangle(Rect, ColourTint);
        }

        public void CursorEvents(MouseEvent mouseevents)
        {
            if (IsPositionInsideRect(mouseevents.CursorPosition) == true)
            {
                switch(HoverState)
                {
                    case GUIElementHoverState.Out:
                        HoverState = GUIElementHoverState.Enter;
                        CursorEnter(mouseevents);
                        break;
                    case GUIElementHoverState.Enter:
                        HoverState = GUIElementHoverState.Hover;
                        CursorHover(mouseevents);
                        break;
                    case GUIElementHoverState.Hover:
                        HoverState = GUIElementHoverState.Hover;
                        CursorHover(mouseevents);
                        break;
                    case GUIElementHoverState.Exit:
                        HoverState = GUIElementHoverState.Enter;
                        CursorEnter(mouseevents);
                        break;
                    default:
                        Debug.WarningLog($"GUIElement had an invalid GUIElementHoverState : {HoverState}");
                        HoverState = GUIElementHoverState.Out;
                        break;
                }

                if (HoverState != GUIElementHoverState.Hover || HoverState != GUIElementHoverState.Enter)
                {
                    if (mouseevents.MouseDown == true)
                    {
                        MouseDown(mouseevents);
                    }

                    switch (mouseevents.MouseButtonState)
                    {
                        case KeyState.Pressed:
                            MouseClick(mouseevents);
                            break;
                        case KeyState.Released:
                            MouseRelease(mouseevents);
                            break;
                        default:
                            break;
                    }
                }

                return;
            }

            switch (HoverState)
            {
                case GUIElementHoverState.Out:
                    HoverState = GUIElementHoverState.Out;
                    break;
                case GUIElementHoverState.Enter:
                    HoverState = GUIElementHoverState.Exit;
                    CursorExit(mouseevents);
                    break;
                case GUIElementHoverState.Hover:
                    HoverState = GUIElementHoverState.Exit;
                    CursorExit(mouseevents);
                    break;
                case GUIElementHoverState.Exit:
                    HoverState = GUIElementHoverState.Out;
                    break;
                default:
                    Debug.WarningLog($"GUIElement had an invalid GUIElementHoverState : {HoverState}");
                    HoverState = GUIElementHoverState.Out;
                    break;
            }

        }

        public virtual void MouseClick(MouseEvent mouseevents)
        {
            return;
        }

        public virtual void MouseRelease(MouseEvent mouseevents)
        {
            return;
        }

        public virtual void MouseDown(MouseEvent mouseevents)
        {
            return;
        }
        public virtual void MouseUp(MouseEvent mouseevents)
        {
            return;
        }
        public virtual void CursorEnter(MouseEvent mouseevents)
        {
            return;
        }
        public virtual void CursorExit(MouseEvent mouseevents)
        {
            return;
        }
        public virtual void CursorHover(MouseEvent mouseevents)
        {
            return;
        }

        public bool IsPositionInsideRect(ScreenPosition position)
        {
            ScreenPosition topboundary = new ScreenPosition((int)(Rect.X), (int)(Rect.Y));
            ScreenPosition bottomboundary = new ScreenPosition((int)(Rect.X + Rect.Width), (int)(Rect.Y + Rect.Height));

            return (position.x >= bottomboundary.x) && (position.x <= topboundary.x) && (position.y >= topboundary.y) && (position.y <= topboundary.y);
        }
    }
}
