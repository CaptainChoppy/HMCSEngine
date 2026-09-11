using Raylib_cs;
using System.Numerics;

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

            GUIElement? element = GetElementAtPosition(CurrentMouseEvents.CursorPosition);

            if(element == null)
            {
                return;
            }

            element.MouseHover(CurrentMouseEvents);

            switch (CurrentMouseEvents.MouseButtonState)
            {
                case KeyState.Up:
                    element.MouseUp(CurrentMouseEvents);
                    break;
                case KeyState.Down:
                    element.MouseDown(CurrentMouseEvents);
                    break;
                case KeyState.Released:
                    element.MouseUp(CurrentMouseEvents);
                    break;
                case KeyState.Pressed:
                    element.MouseDown(CurrentMouseEvents);
                    break;
                default:
                    break;
            }
        }

        private static GUIElement? GetElementAtPosition(ScreenPosition position)
        {
            foreach(GUIElement e in Elements)
            {
                if(e.IsPositionInsideRect(position) == false)
                {
                    continue;
                }
                
                return e;
            }

            return null;
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

    internal abstract class GUIElement
    {
        public Rectangle Rect;
        public Color ColourTint = Color.White;
        public int DrawOrder = 0;

        public GUIElement(Rectangle rect)
        {
            Rect = rect;
        }

        public virtual void Draw()
        {
            Raylib.DrawRectanglePro(Rect, Vector2.Zero, 0, Color.White);
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

    internal class GUIImage : GUIElement
    {
        public Texture Texture;

        public GUIImage(Rectangle rect, Texture texture) : base(rect)
        {
            Texture = texture;
        }

        public override void Draw()
        {
            Raylib.DrawTexturePro(Texture, new Rectangle(0, 0, Texture.Width, Texture.Height), Rect, Vector2.Zero, 0, ColourTint);
        }
    }

    internal class GUIButton : GUIElement
    {
        public Action ClickEvent;

        public GUIButton(Rectangle rect, Action clickevent) : base(rect)
        {
            ClickEvent = clickevent;
        }

        public override void Draw()
        {
            Raylib.DrawRectanglePro(Rect, Vector2.Zero, 0, ColourTint);
        }

        public override void MouseDown(MouseGUIEvent mouseevents)
        {
            if(mouseevents.MouseButton != MouseButtons.Left)
            {
                return;
            }

            ClickEvent();
        }
    }
}
