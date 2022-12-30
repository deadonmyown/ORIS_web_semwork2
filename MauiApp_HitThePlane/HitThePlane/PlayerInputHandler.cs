using TCPClient;
using XProtocol.Serializator;
using XProtocol;
using HitThePlane.Game;

namespace HitThePlane
{
    public static class PlayerInputHandler
    {
        private static bool isAdown = false;
        private static bool isDdown = false;
        private static bool isWdown = false;
        private static bool isSdown = false;
        private static bool mustShoot = false;

        public static void SendInput(XClient client, Player player)
        {
            client.QueuePacketSendUpdate(XPacketConverter.Serialize(XPacketType.PlayerInput,
                new XPacketPlayerInput(player.Plane.Speed, client.Id, isAdown, isDdown, isWdown, isSdown))
                .ToPacket());
        }

        public static void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                isAdown = true;
            if (e.KeyCode == Keys.D)
                isDdown = true;
            if (e.KeyCode == Keys.W)
                isWdown = true;
            if (e.KeyCode == Keys.S)
                isSdown = true;
        }

        public static void KeyReleased(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                isAdown = false;
            if (e.KeyCode == Keys.D)
                isDdown = false;
            if (e.KeyCode == Keys.W)
                isWdown = false;
            if (e.KeyCode == Keys.S)
                isSdown = false;
        }

        public static void MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mustShoot = true;
        }

        /*public static void Apply()
        {
            if (isAdown) Plane.Direction = PlaneDirection.Down;
            if (isDdown) Plane.Direction = PlaneDirection.Up;
            if (isWdown) Plane.Speed += Plane.SpeedBoost;
            if (isSdown) Plane.Speed -= Plane.SpeedBoost;
            if (mustShoot)
            {
                Plane.Shoot();
                mustShoot = false;
            }
        }*/

        public static void Apply()
        {
            SendInput(NetworkManager.Instance.Client, NetworkManager.Instance.Player);
        }
    }
}
