using TCPClient;
using XProtocol.Serializator;
using XProtocol;
using HitThePlane.Entities;


namespace HitThePlane.Engine
{
    public static class PlayerInputHandler
    {
        private static AirPlane _plane;
        private static GameForm _form;

        public static void Bind(AirPlane plane, GameForm form)
        {
            _plane = plane;
            _form = form;
        }

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
            if (e.KeyCode == Keys.K)
                mustShoot = true;

            if (e.KeyCode == Keys.Escape)
                _form.Pause();

            e.SuppressKeyPress = true;
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
            if (e.KeyCode == Keys.K)
                mustShoot = false;
        }

        //не работает
        public static void MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mustShoot = true;
        }

        /*public static void Apply()
        {
            if (isAdowm) _plane.Rotate(PlaneDirection.Up);
            if (isDdown) _plane.Rotate(PlaneDirection.Down);
            if (isWdowm) _plane.Speed += _plane.SpeedBoost;
            if (isSdown) _plane.Speed -= _plane.SpeedBoost;
            //TODO
            if (mustShoot) _plane.Shoot();
        }*/

        public static void Apply()
        {
            SendInput(NetworkManager.Instance.Client, NetworkManager.Instance.Player);
        }
    }
}
