using System.Net.Sockets;
using TCPClient;
using MAUILibrary;
using SharpHook;
using Microsoft.Maui.Controls;

namespace MauiApp_HitThePlane;

public partial class MainPage : ContentPage
{
    private GameTimer _timer;

    private Player _player;

    private TaskPoolGlobalHook _hook;

    private PlayerController _playerController;

    public string Host { get; } = "127.0.0.1";
    public int Port { get; } = 4910;
    public XClient Client { get; } = new();

    public string PlayerName { get; private set; }


    public MainPage()
    {
        InitializeComponent();
        canvas.Invalidate();
        //Time.Instance.StartTime.Restart();
    }

    private async void OnAddPlayerClicked(object sender, EventArgs e)
    {
        ////Client.Connect(Host, Port);

        //var button = sender as Button;
        //button.IsEnabled = false;
        //button.IsVisible = false;

        //PlayerName = PlayerText.Text;
        //PlayerText.IsEnabled = false;
        //PlayerText.IsVisible = false;
        ////Nickname.Text = PlayerName;

        //_player = new Player(PlayerName, 100, 700, 1000, 1, new GameObject(this, "player", "foreground", plane_1), 25, 20);
        //_playerController = new PlayerController() { Player = _player, UpdateTime = Time.Instance.IntervalMs };

        //_timer = new GameTimer();
        //_timer.Start(_playerController.UpdateTime, _playerController.Update);

        //await CreateHookInstance();
    }

    //private void StartTimer(object sender, EventArgs e)
    //{
    //    if (_timer != null && !_timer.Running)
    //    {
    //        _timer.Stop();

    //        _timer.Start(_playerController.UpdateTime, _playerController.Update);
    //    }
    //}

    //private void StopTimer(object sender, EventArgs e)
    //{
    //    if (_timer != null && _timer.Running)
    //        _timer.Stop();
    //}

    //private void UpButtonHold(object sender, EventArgs e)
    //{
    //       _player?.Rotate(1);
    //   }

    //private void UpButtonReleased(object sender, EventArgs e)
    //{
    //       _player?.Rotate(0);
    //   }

    //   private void DownButtonHold(object sender, EventArgs e)
    //   {
    //       _player?.Rotate(-1);
    //   }

    //   private void DownButtonReleased(object sender, EventArgs e)
    //   {
    //       _player?.Rotate(0);
    //   }


    //private void OnKeyReleased(object sender, KeyboardHookEventArgs e)
    //{
    //    PlayerInputHandler.KeyboardReleased(_player, sender, e);
    //}

    //private void OnKeyPressed(object sender, KeyboardHookEventArgs e)
    //{
    //    PlayerInputHandler.KeyboardPressed(_player, sender, e);
       
    //    if (e.RawEvent.Keyboard.KeyCode == SharpHook.Native.KeyCode.VcSpace) 
    //        _player.Shoot(new Projectile() { Damage = 20, GameObject = new GameObject(this, "bullet", "foreground", projectile), Speed = 1000 });
    //}

    //private void OnMouseClicked(object sender, MouseHookEventArgs e)
    //{
    //    _player?.Shoot(testTxt, new Projectile() { Damage = 20, GameObject = new GameObject(this, "bullet", "foreground", projectile), Speed = 1000 });
    //}

    //protected override async void OnAppearing()
    //{
    //    base.OnAppearing();

    //    StartTimer(this, EventArgs.Empty);
    //    //await CreateHookInstance();
    //}

    //protected override void OnDisappearing()
    //{
    //    StopTimer(this, EventArgs.Empty);
    //    base.OnDisappearing();
    //    /*if (_hook != null)
    //        _hook.Dispose();*/
    //}

    //private async Task CreateHookInstance()
    //{
    //    _hook = new TaskPoolGlobalHook();

    //    //_hook.KeyTyped += OnKeyTyped;
    //    _hook.KeyPressed += OnKeyPressed;
    //    _hook.KeyReleased += OnKeyReleased;

    //    //_hook.MouseClicked += OnMouseClicked;
    //    /*_hook.MousePressed += OnMousePressed;
    //    _hook.MouseReleased += OnMouseReleased;
    //    _hook.MouseMoved += OnMouseMoved;
    //    _hook.MouseDragged += OnMouseDragged;*/

    //    await _hook.RunAsync();
    //}
}

