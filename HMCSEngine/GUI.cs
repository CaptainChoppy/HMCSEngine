using Raylib_cs;
using System.Numerics;
using System.Xml.Linq;

namespace HMCSEngine
{
    internal static class GUI
    {
        private static readonly List<GUIElement> Elements = new List<GUIElement>();

        private static MouseGUIEvent CurrentMouseEvents = new MouseGUIEvent(MouseButtons.None, KeyState.Up, ScreenPosition.Zero);

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

            if(CurrentMouseEvents.MouseButton == MouseButtons.None)
            {
                return;
            }

            foreach(GUIElement e in Elements)
            {
                e.CursorEvents(CurrentMouseEvents);
            }
        }

        private static MouseGUIEvent GetMouseEvents()
        {
            MouseButtons button = Inputs.GetMouseButton();

            return new MouseGUIEvent(button, Inputs.GetMouseButtonState(button), Cursor.Position);
        }

        public static void AddElement(GUIElement element)
        {
            Elements.Add(element);
        }

        public static void RemoveElement(GUIElement element)
        {
            Elements.Remove(element);
        }
    }

    internal enum GUIElementHoverState : int
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

        public GUIElementHoverState HoverState { get; private set; }

        public GUIElement(Rectangle rect)
        {
            Rect = rect;
            GUI.AddElement(this);
        }

        public virtual void Draw()
        {
            Raylib.DrawRectanglePro(Rect, Vector2.Zero, 0, Color.White);
        }

        public void CursorEvents(MouseGUIEvent mouseevents)
        {
            if (IsPositionInsideRect(mouseevents.CursorPosition) == true)
            {
                switch(HoverState)
                {
                    case GUIElementHoverState.Out:
                        HoverState = GUIElementHoverState.Enter;
                        MouseEnter(mouseevents);
                        break;
                    case GUIElementHoverState.Enter:
                        HoverState = GUIElementHoverState.Hover;
                        MouseHover(mouseevents);
                        break;
                    case GUIElementHoverState.Hover:
                        HoverState = GUIElementHoverState.Hover;
                        MouseHover(mouseevents);
                        break;
                    case GUIElementHoverState.Exit:
                        HoverState = GUIElementHoverState.Enter;
                        MouseEnter(mouseevents);
                        break;
                    default:
                        Debug.WarningLog($"GUIElement had an invalid GUIElementHoverState : {HoverState}");
                        HoverState = GUIElementHoverState.Out;
                        break;
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
                    MouseExit(mouseevents);
                    break;
                case GUIElementHoverState.Hover:
                    HoverState = GUIElementHoverState.Exit;
                    MouseExit(mouseevents);
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

        public virtual void MouseDown(MouseGUIEvent mouseevents)
        {
            return;
        }
        public virtual void MouseUp(MouseGUIEvent mouseevents)
        {
            return;
        }
        public virtual void MouseEnter(MouseGUIEvent mouseevents)
        {
            return;
        }
        public virtual void MouseExit(MouseGUIEvent mouseevents)
        {
            return;
        }
        public virtual void MouseHover(MouseGUIEvent mouseevents)
        {
            return;
        }

        public bool IsPositionInsideRect(ScreenPosition position)
        {
            ScreenPosition bottomboundary = new ScreenPosition(Rect.x, Rect.y);
            ScreenPosition topboundary = new ScreenPosition(Rect.x + Rect.Width, Rect.y + Rect.Height);

            return position.x >= bottomboundary.x && position.x <= topboundary.x && position.y >= topboundary.y && position.y <= topboundary.y;
        }
    }

    internal class GUIText : GUIElement
    {
        public string Text;
        public int FontSize;
        public int Spacing = 1;

        public int FontIndex;

        public ScreenPosition Position;

        public GUIText(ScreenPosition position, string text, int fontsize, int fontindex) : base(new Rectangle(Vector2.Zero, Vector2.Zero))
        {
            Position = position;
            Text = text;
            FontSize = fontsize;
            FontIndex = fontindex;
        }

        public override void Draw()
        {
            Renderer.DrawText(Text, FontIndex, FontSize, Spacing, ColourTint, Position);
        }
    }

    public abstract class GUIButton : GUIElement
    {
        public GUIButton(Rectangle rect) : base(rect)
        {

        }

        public override void Draw()
        {
            Raylib.DrawRectanglePro(Rect, Vector2.Zero, 0, ColourTint);
        }
    }
}
