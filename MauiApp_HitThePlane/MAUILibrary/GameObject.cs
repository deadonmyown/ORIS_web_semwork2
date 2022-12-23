using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUILibrary
{
    public class GameObject
    {
        
        public ContentPage Scene { get; }
        public string Tag { get; }
        public string Layer { get; }
        public View Controller { get; }

        public GameObject(ContentPage scene, string tag, string layer, View controller)
        {
            Scene = scene;
            Tag = tag;
            Layer = layer;
            Controller = controller;
        }

        public GameObject() { }
    }
}
