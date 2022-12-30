using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace HitThePlane.Resources;
static class R
{
    public const string MAIN_LEVEL_BG = "Frames\\scene.png";

    public const string GREEN_PLANE = "Frames\\GreenPlane\\green_plane.png";
    public const string GREEN_PLANE2 = "Frames\\GreenPlane\\green_plane2.png";
    public const string GREEN_PLANE3 = "Frames\\GreenPlane\\green_plane3.png";
    public const string GREEN_TURN = "Frames\\GreenPlane\\green_turn.png";

    public const string RED_PLANE = "Frames\\RedPlane\\red_plane.png";
    public const string RED_PLANE2 = "Frames\\RedPlane\\red_plane2.png";
    public const string RED_PLANE3 = "Frames\\RedPlane\\red_plane3.png";
    public const string RED_TURN = "Frames\\RedPlane\\red_turn.png";

    public const string BULLET = "Frames\\bullet.png";
    public const string EXPLOSION1 = "Frames\\BulletExplosion\\explosion1.png";
    public const string EXPLOSION2 = "Frames\\BulletExplosion\\explosion2.png";
    public const string EXPLOSION3 = "Frames\\BulletExplosion\\explosion3.png";
    public const string EXPLOSION4 = "Frames\\BulletExplosion\\explosion4.png";
    public const string EXPLOSION5 = "Frames\\BulletExplosion\\explosion5.png";
    public const string EXPLOSION6 = "Frames\\BulletExplosion\\explosion6.png";
    public const string EXPLOSION7 = "Frames\\BulletExplosion\\explosion7.png";

    public const string MENU_BACKGROUND = "Frames\\menuBg.png";

    public static readonly PrivateFontCollection Fonts = new PrivateFontCollection();

    private static readonly Dictionary<string, Image> images = new Dictionary<string, Image>();
    private static readonly Dictionary<string, Image> flipped = new Dictionary<string, Image>();
    static R()
    {
        images = FileLoader.LoadFrames();
        FileLoader.LoadFont("Fonts\\pixel.ttf", Fonts);
    }

    public static Image GetImage(string resource) =>
        images[resource];

    public static Image GetFlip(string resource)
    {
        if (!flipped.ContainsKey(resource))
        {
            using var ms = new MemoryStream();
            images[resource].Save(ms, ImageFormat.Png);
            var copy = Image.FromStream(ms);
            copy.RotateFlip(RotateFlipType.Rotate180FlipX);
            flipped[resource] = copy;
        }
        return flipped[resource];
    }
}
