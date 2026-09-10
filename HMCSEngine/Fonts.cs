using Raylib_cs;

namespace HMCSEngine
{
    internal static class Fonts
    {
        public const int FontCount = 4;

        private static FontSlot?[] FontSlots = new FontSlot[FontCount];

        public static void LoadFont(FontInfo info)
        {
            FontSlot? slot = FontSlots[info.Slot];

            if (slot != null)
            {
                slot.Dispose();
                slot = null;
            }

            slot = new FontSlot(info.Name);
        }

        public static Font GetFont(int slotindex)
        {
            if(slotindex >= FontCount)
            {
                Debug.WarningLog($"Font index ({slotindex}) was out of range");
                return Raylib.GetFontDefault();
            }

            FontSlot? slot = FontSlots[slotindex];

            if (slot == null)
            {
                Debug.WarningLog($"No font was loaded into font slot {slotindex}");
                return Raylib.GetFontDefault();
            }

            return slot.Font;
        }
    }

    internal struct FontInfo
    {
        public readonly string Name = "";
        public readonly int Slot = 0;

        public FontInfo(string name, int slot)
        {
            if(string.IsNullOrEmpty(name) == true)
            {
                Debug.ErrorLog("Font name was null or empty so info could not be created");
                return;
            }

            if(slot >= Fonts.FontCount)
            {
                Debug.ErrorLog($"Slot index was out of range 0-{Fonts.FontCount} (inclusive, exclusive). index : {slot}");
                return;
            }

            Name = name;
            Slot = slot;
        }
    }

    internal sealed class FontSlot : IDisposable
    {
        public readonly string Name;
        public readonly Font Font;

        public FontSlot(string name)
        {
            Name = name;

            string fontpath = Files.GetResourcesFontFilePath(Name);

            if (File.Exists(fontpath) == false)
            {
                Debug.WarningLog($"Could not load font {Name} because the file does not exist.");
                Font = Raylib.GetFontDefault();
                return;
            }

            Font = Raylib.LoadFont(fontpath);
        }

        public void Dispose()
        {
            Raylib.UnloadFont(Font);
        }
    }
}
