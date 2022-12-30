using System;
using TCPServer.Game;

namespace TCPServer
{
    internal class Program
    {
        private static void Main()
        {
            GameManager.SpeedBoost = 0.5f;
            GameManager.MaxSpeed = 20;
            GameManager.AngleChange = 10;
            Console.Title = "XServer";
            Console.ForegroundColor = ConsoleColor.White;

            NetworkManager.Instance.Start(2, 4910);
        }
    }
}
