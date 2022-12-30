using HitThePlane.Utils;

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
}
