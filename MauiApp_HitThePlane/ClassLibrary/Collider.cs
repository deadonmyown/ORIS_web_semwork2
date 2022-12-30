using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public struct Collider
    {
        public Vector2 Position { get; set; }
        public Size Size { get; set; }
        public Collider(Vector2 position, Size size)
        {
            Position = position;
            Size = size;
        }
    }
}
