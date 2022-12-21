using System.Net.Sockets;
using TCPClient;
using HitThePlaneLibrary;
using GameLibrary;
using SharpHook;

namespace MauiApp_HitThePlane;

public partial class MainPage : ContentPage
{
	private GameTimer _timer;

	private Player _player;

    private TaskPoolGlobalHook _hook;

    public string Host { get; } = "127.0.0.1";
    public int Port { get; } = 4910;
    public XClient Client { get; } = new();
    public StreamReader? Reader { get; private set; }
    public StreamWriter? Writer { get; private set; }

    public string PlayerName { get; private set; }

    public MainPage()
	{
		InitializeComponent();
    }

    private async void OnAddPlayerClicked(object sender, EventArgs e)
	{
        //Client.Connect(Host, Port);

        var button = sender as Button;
        button.IsEnabled = false;
        button.IsVisible = false;

        PlayerName = PlayerText.Text;

		Nickname.Text = PlayerName;

		_player = new Player(PlayerName, 100, 10, 30, 1);

        _timer = new GameTimer();
        _timer.Start(100, Update);
        _timer.Start(33, FixedUpdate);

        await CreateHookInstance();
    }

	private void PlayerMoveTowards()
	{
		plane_1.TranslateTo(plane_1.TranslationX + _player.Speed * Math.Cos(plane_1.Rotation * Math.PI / 180), 
			plane_1.TranslationY + _player.Speed * Math.Sin(plane_1.Rotation * Math.PI / 180));
	}

	private void StartTimer(object sender, EventArgs e)
	{
        if (_timer != null && !_timer.Running)
        {
            _timer.Stop();

            _timer.Start(100, Update);
            _timer.Start(33, FixedUpdate);
        }
    }

	private void StopTimer(object sender, EventArgs e)
	{
        if (_timer != null && _timer.Running)
            _timer.Stop();
	}

	private void Update()
	{
        PlayerMoveTowards();
        CheckRotate();
        ChangeSpeed();
        CheckGravity();
    }

	private void FixedUpdate()
	{
        //TODO: timer is lagging with small intervalMs, need to fix and after all physics movement from Update paste here.
    }

	private void CheckRotate()
	{
		if (_player.Rotate == 1)
			plane_1.RotateTo(plane_1.Rotation - 10);
		else if(_player.Rotate == -1)
            plane_1.RotateTo(plane_1.Rotation + 10);
    }

    private void ChangeSpeed()
    {
        var rotation = plane_1.Rotation + Math.Ceiling(-plane_1.Rotation / 360) * 360;
        if (rotation >= 0 && rotation <= 180 && _player.Speed < _player.MaxSpeed)
            _player.Speed += 1;
        else if (rotation > 190 && rotation < 350 && _player.Speed > 0)
            _player.Speed = Math.Max(0, _player.Speed + Math.Sin(rotation * Math.PI / 180) / 2);
    }

    private void CheckGravity()
    {
        if (_player.Speed <= 0)
        {
            plane_1.TranslateTo(plane_1.TranslationX, plane_1.TranslationY + 9.8);

            var rotation = plane_1.Rotation + Math.Ceiling(-plane_1.Rotation / 360) * 360;
            if(rotation > 100 && rotation <= 270)
                plane_1.RotateTo(plane_1.Rotation - 10);
            else if(rotation > 270 || (rotation > 0 && rotation < 70))
                plane_1.RotateTo(plane_1.Rotation + 10);

        }
    }

	private void UpButtonHold(object sender, EventArgs e)
	{
        PlayerRotate(1);
    }

	private void UpButtonReleased(object sender, EventArgs e)
	{
        PlayerRotate(0);
    }

    private void DownButtonHold(object sender, EventArgs e)
    {
        PlayerRotate(-1);
    }

    private void DownButtonReleased(object sender, EventArgs e)
    {
        PlayerRotate(0);
    }

    private void PlayerRotate(int rotate)
    {
        if (_player != null)
            _player.Rotate = rotate;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        StartTimer(this, EventArgs.Empty);
        await CreateHookInstance();
    }

    private async Task CreateHookInstance()
    {
        if (_hook == null)
        {
            _hook = new TaskPoolGlobalHook();

            //_hook.KeyTyped += OnKeyTyped;
            _hook.KeyPressed += OnKeyPressed;
            _hook.KeyReleased += OnKeyReleased;

            /*_hook.MouseClicked += OnMouseClicked;
            _hook.MousePressed += OnMousePressed;
            _hook.MouseReleased += OnMouseReleased;
            _hook.MouseMoved += OnMouseMoved;
            _hook.MouseDragged += OnMouseDragged;*/

            await _hook.RunAsync();
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        StopTimer(this, EventArgs.Empty);
        if (_hook != null)
            _hook.Dispose();
    }

    private void OnKeyReleased(object sender, KeyboardHookEventArgs e)
    {
        PlayerRotate(0);
    }

    private void OnKeyPressed(object sender, KeyboardHookEventArgs e)
    {
        if (_player != null)
        {
            if (e.RawEvent.Keyboard.KeyCode == SharpHook.Native.KeyCode.VcW)
                _player.Rotate = 1;
            else if (e.RawEvent.Keyboard.KeyCode == SharpHook.Native.KeyCode.VcS)
                _player.Rotate = -1;
            else if (e.RawEvent.Keyboard.KeyCode == SharpHook.Native.KeyCode.VcLeftShift)
                _player.Speed = Math.Max(0, _player.Speed - 3);
        }
    }
}

