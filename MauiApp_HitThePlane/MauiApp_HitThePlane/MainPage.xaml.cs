using System.Net.Sockets;
using TCPClient;
using HitThePlaneLibrary;

namespace MauiApp_HitThePlane;

public partial class MainPage : ContentPage
{
	int count = 0;

    string Host { get; } = "127.0.0.1";
    int Port { get; } = 8888;
    XClient Client { get; } = new();
    StreamReader? Reader { get; set; }
    StreamWriter? Writer { get; set; }

    public string PlayerName { get; set; }

    public MainPage()
	{
		InitializeComponent();
	}

	/*private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}*/

	private async void OnAddPlayerClicked(object sender, EventArgs e)
	{
        //Client.Connect("127.0.0.1", 4910);

        var button = sender as Button;
        button.IsEnabled = false;
        button.IsVisible = false;

        PlayerName = PlayerText.Text;

		Nickname.Text = PlayerName;

		Task.Run(() =>PlayerProcess());
    }

	private async Task PlayerProcess()
	{
		Player player = new Player() { Health = 100, PlayerName = Nickname.Text, Speed = 0.5f };
		while (true)
		{
			await plane_1.TranslateTo(player.Speed, player.Speed);
		}
	}
}

