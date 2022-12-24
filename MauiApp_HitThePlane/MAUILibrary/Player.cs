using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MAUILibrary
{
    public class Player
    {
        public string PlayerName { get; set; }
        public Plane Plane { get; set; }
        public Player(string playerName, Plane plane)
        {
            PlayerName = playerName;
            Plane = plane;
        }
    }
}
