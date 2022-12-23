using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpHook;

namespace MAUILibrary
{
    public class PlayerInputHandler
    {
        public static void KeyboardPressed(Player player, object sender, KeyboardHookEventArgs keyboardInput)
        {
            if (player != null)
            {
                if (keyboardInput.RawEvent.Keyboard.KeyCode == SharpHook.Native.KeyCode.VcW)
                    player.Rotation = 1;
                else if (keyboardInput.RawEvent.Keyboard.KeyCode == SharpHook.Native.KeyCode.VcS)
                    player.Rotation = -1;
                else if (keyboardInput.RawEvent.Keyboard.KeyCode == SharpHook.Native.KeyCode.VcLeftShift)
                {
                    player.Speed = Math.Max(0, player.Speed - player.SpeedBoost);
                    player.IsFreezing = true;
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

