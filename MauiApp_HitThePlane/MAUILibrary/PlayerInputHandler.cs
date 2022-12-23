using SharpHook;

namespace MAUILibrary
{
    public class PlayerInputHandler
    {
        public static void KeyboardPressed(Player player, object sender, KeyboardHookEventArgs keyboardInput)
        {
            if (player != null)
            {
                switch (keyboardInput.RawEvent.Keyboard.KeyCode)
                {
                    case SharpHook.Native.KeyCode.VcW:
                        player.Rotation = 1;
                        break;

                    case SharpHook.Native.KeyCode.VcS:
                        player.Rotation = -1;
                        break;

                    case SharpHook.Native.KeyCode.VcLeftShift:
                        player.Speed = Math.Max(0, player.Speed - player.SpeedBoost);
                        player.IsFreezing = true;
                        break;
                }
            }
        }

        public static void KeyboardReleased(Player player, object sender, KeyboardHookEventArgs keyboardInput)
        {
            if (player != null)
            {
                player.Rotate(0);
                player.IsFreezing = false;
            }
        }
    }
}

