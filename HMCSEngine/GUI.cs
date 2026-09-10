using Raylib_cs;
using System.Numerics;

namespace HMCSEngine
{
    internal static class GUI
    {
        private static List<GUIElement> Elements = new List<GUIElement>();

        public static void Draw()
        {
            foreach(GUIElement e in Elements)
            {
                e.Draw();
            }
        }

        public static void Update()
        {

        }

        private static MouseGUIEvent GetMouseEvents()
        {
            MouseButtons mousebutton = Inputs.GetMouseButton();

            Vector2 mouseposition = Raylib.GetMousePosition();

            return new MouseGUIEvent(mousebutton, mouseposition, Inputs.MouseDown);
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
        public Texture2D Texture;

        public GUIImage(Rectangle rect, Texture2D texture) : base(rect)
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
