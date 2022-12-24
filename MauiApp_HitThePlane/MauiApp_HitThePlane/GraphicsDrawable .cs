namespace MauiApp_HitThePlane;

public class GraphicsDrawable : IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        DrawBackground(canvas, dirtyRect);

        canvas.StrokeColor = Colors.Red;
        canvas.StrokeSize = 6;
        canvas.DrawLine(10, 10, 100, 100);
    }

    public void DrawBackground(ICanvas canvas, RectF rect)
    {
 
        var bg = new Image() { Source = ImageSource.FromFile("scene.png") };
        canvas.DrawImage(bg, rect.X, rect.Y, rect.Width, rect.Height);
    }
}
