using System.Runtime.InteropServices;

namespace HMCSEngine
{
    internal static class Inputs
    {
        private static readonly KeyState[] KeyStates = new KeyState[256];

        public static bool MouseDown => KeyDown(VKeyCodes.LeftMouse) || KeyDown(VKeyCodes.RightMouse) || KeyDown(VKeyCodes.MiddleMouse) || KeyDown(VKeyCodes.MouseX1) || KeyDown(VKeyCodes.MouseX2);

        [DllImport("User32.dll")] private static extern int GetAsyncKeyState(Int32 i);

        public static void Update()
        {
            for(int i = 0; i < KeyStates.Length; i++)
            {
                int keystate = GetAsyncKeyState(i);

                KeyState currentstate = KeyStates[i];

                if(keystate == 32768)
                {
                    switch (currentstate)
                    {
                        case KeyState.Down:
                            KeyStates[i] = KeyState.Down;
                            break;
                        case KeyState.Up:
                            KeyStates[i] = KeyState.Pressed;
                            break;
                        case KeyState.Pressed:
                            KeyStates[i] = KeyState.Down;
                            break;
                        case KeyState.Released:
                            KeyStates[i] = KeyState.Pressed;
                            break;
                        default:
                            break;
                    }
                }

                if (keystate == 0)
                {
                    switch (currentstate)
                    {
                        case KeyState.Down:
                            KeyStates[i] = KeyState.Released;
                            break;
                        case KeyState.Up:
                            KeyStates[i] = KeyState.Up;
                            break;
                        case KeyState.Pressed:
                            KeyStates[i] = KeyState.Released;
                            break;
                        case KeyState.Released:
                            KeyStates[i] = KeyState.Up;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        
        public static VKeyCodes GetKey()
        {
            for(int i = 0; i < KeyStates.Length; i++)
            {
                if (KeyStates[i] != KeyState.Down || KeyStates[i] != KeyState.Pressed)
                {
                    continue;
                }

                return (VKeyCodes)(i);
            }

            return VKeyCodes.None;
        }

        public static MouseButtons GetMouseButton()
        {
            switch (GetKey())
            {
                case VKeyCodes.LeftMouse:
                    return MouseButtons.Left;
                case VKeyCodes.RightMouse:
                    return MouseButtons.Right;
                case VKeyCodes.MiddleMouse:
                    return MouseButtons.Middle;
                case VKeyCodes.MouseX1:
                    return MouseButtons.X1;
                case VKeyCodes.MouseX2:
                    return MouseButtons.X2;
                default:
                    return MouseButtons.None;
            }
        }

        public static KeyState GetMouseButtonState(MouseButtons button)
        {
            if((int)(button) > 6 || (int)(button) == 3 || (int)(button) <= 0)
            {
                Debug.WarningLog($"Tried to get keystate for mouse button {button} which does not exist");
                return KeyState.Up;
            }

            return GetKeyState((VKeyCodes)(button));
        }

        public static bool KeyDown(VKeyCodes key)
        {
            return GetKeyState(key) == KeyState.Down || GetKeyState(key) == KeyState.Pressed;
        }

        public static bool KeyUp(VKeyCodes key)
        {
            return GetKeyState(key) == KeyState.Up || GetKeyState(key) == KeyState.Released;
        }

        public static bool KeyPressed(VKeyCodes key)
        {
            return GetKeyState(key) == KeyState.Pressed;
        }

        public static bool KeyReleased(VKeyCodes key)
        {
            return GetKeyState(key) == KeyState.Released;
        }

        public static KeyState GetKeyState(VKeyCodes key)
        {
            return KeyStates[(int)(key)];
        }
    }

    internal enum KeyState : int
    {
        Up = 0,
        Down,
        Pressed,
        Released
    }
}
