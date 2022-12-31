using System;
using TCPServer.Game;

namespace TCPServer
{
    internal class Program
    {
        private static void Main()
        {
            GameManager.SpeedBoost = 0.5f;
            GameManager.MaxSpeed = 15;
            GameManager.AngleChange = 8;
            GameManager.ReloadTime = 8;
            Console.Title = "XServer";
            Console.ForegroundColor = ConsoleColor.White;

            NetworkManager.Instance.Start(2, 4910);
        }
    }
}
