using HitThePlane.Utils;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace HitThePlane.Resources;
static class FileLoader
{
    public static Dictionary<string, Image> LoadFrames()
    {
        Dictionary<string, Image> res = new Dictionary<string, Image>();

        var resources = typeof(R).GetAllPublicConstantValues<string>();
        foreach (var resource in resources)
            res.Add(resource, Image.FromFile(resource));
        return res;
    }

    public static void LoadFont(string path, PrivateFontCollection fonts)
    {
        using var fontStream = File.OpenRead(path);
        var data = Marshal.AllocCoTaskMem((int)fontStream.Length);
        byte[] fontdata = new byte[fontStream.Length];
        fontStream.Read(fontdata, 0, (int)fontStream.Length);
        Marshal.Copy(fontdata, 0, data, (int)fontStream.Length);
        fonts.AddMemoryFont(data, (int)fontStream.Length);
        Marshal.FreeCoTaskMem(data);
    }
}
