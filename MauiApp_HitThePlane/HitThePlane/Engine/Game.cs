using HitThePlane.Entities;
using Timer = System.Windows.Forms.Timer;

namespace HitThePlane.Engine;
class Game
{
    private GameForm _form;
    private Timer _gameTimer;
    private Level _level;
    public Game(GameForm form, PictureBox canvas)
    {
        _form = form;
        Render.Canvas = canvas;
    }

    public void Run()
    {
        _level = new Level(6, 0.01f);
        BindKeys(_level.PlayerPlane);
        _gameTimer = new Timer();
        _gameTimer.Interval = 50;
        _gameTimer.Tick += UpdateLevel;
        _gameTimer.Start();
    }

    public void Pause()
    {
        _gameTimer.Stop();
    }

    public void Resume()
    {
        if (_gameTimer == null)
            Run();
        else
            _gameTimer.Start();
    }

    private void UpdateLevel(object sender, EventArgs e)
    {
        PlayerInputHandler.Apply();
        _level.PlayerPlane.Move();
        _level.EnemyPlane.Move();
        foreach (var bullet in _level.Bullets)
            bullet.Move();

        Render.DrawLevel(_level);
        //if (_level.PlayerPlane.State == PlaneState.Destroyed)
        //    StopGame();
    }

    private void BindKeys(AirPlane plane)
    {
        PlayerInputHandler.Bind(plane, _form);
        _form.KeyDown += new KeyEventHandler(PlayerInputHandler.KeyPressed);
        _form.KeyUp += new KeyEventHandler(PlayerInputHandler.KeyReleased);
        _form.MouseDown += new MouseEventHandler(PlayerInputHandler.MouseClick);
        _form.KeyPreview = true;
    }
}