using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using XProtocol;
using XProtocol.Serializator;
using TCPServer;

namespace TCPClient
{
    internal class Program
    {
        private static void Main()
        {
            var test1 = new TestClient();
            test1.Start();
            var test2 = new TestClient();
            test2.Start();
        }
    }

    internal class TestClient
    {
        public XClient client = new XClient();
        public double posX = 0;
        public double posY = 0;
        public Player _player = new Player("igrok", 100, 2, 5, 1, 10);
        public double _rotation = 10;

        internal void Start()
        {
            Time.Instance.StartTime.Restart();

            Console.Title = "XClient";
            Console.ForegroundColor = ConsoleColor.White;

            client.OnPacketRecieve += OnPacketRecieve;
            client.Connect("127.0.0.1", 4910);

            var timer = new GameTimer();
            timer.Start(Time.Instance.IntervalMs * 100, Update);
        }

        private void OnPacketRecieve(byte[] packet)
        {
            var parsed = XPacket.Parse(packet);

            if (parsed != null)
            {
                ProcessIncomingPacket(parsed);
            }
        }

        private void ProcessIncomingPacket(XPacket packet)
        {
            var type = XPacketTypeManager.GetTypeFromPacket(packet);

            switch (type)
            {
                case XPacketType.Handshake:
                    ProcessHandshake(packet);
                    break;
                case XPacketType.PlayerTest:
                    ProcessPlayerTest(packet);
                    break;
                case XPacketType.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ProcessHandshake(XPacket packet)
        {
            var playerConnection = XPacketConverter.Deserialize<XPacketHandshake>(packet);

            _player.PlayerId = playerConnection.PlayerId;
            Console.WriteLine($"player with id = {playerConnection.PlayerId} connected");

        }

        private void ProcessPlayerTest(XPacket packet)
        {
            var player = XPacketConverter.Deserialize<XPacketPlayerTest>(packet);
            if(_player.PlayerId == player.PlayerId)
            {
                posX = player.PosX;
                posY = player.PosY;
                _rotation = player.Rotation;
            }
            Console.WriteLine($"player with id = {_player.PlayerId} get position({Math.Round(player.PosX, 3)} {Math.Round(player.PosY, 3)}) of player with id = {player.PlayerId}");
        }

        private void Update()
        {
            //Console.WriteLine("Update Function");
            client.QueuePacketSendUpdate(XPacketConverter.Serialize(XPacketType.PlayerTest,
                new XPacketPlayerTest { PosX = posX, PosY = posY, Speed = _player.Speed, Rotation = _rotation, DeltaTime = Time.Instance.DeltaTime, PlayerId = _player.PlayerId })
                .ToPacket());
        }
    }

    internal class GameTimer
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern void NtSetTimerResolution(uint DesiredResolution, bool SetResolution, ref uint CurrentResolution);

        [DllImport("ntdll.dll")]
        private static extern bool NtDelayExecution(bool Alertable, ref long DelayInterval);

        private Thread _thread;
        public bool Running { get; private set; }

        public void Start(int intervalMs, Action callback)
        {
            var res = (uint)(intervalMs * 10000);
            NtSetTimerResolution(res, true, ref res);

            _thread = new Thread(() =>
            {
                Running = true;

                while (Running)
                {
                    var interval = -intervalMs * 10000L;
                    NtDelayExecution(false, ref interval);
                    callback();
                }
            })
            { Priority = ThreadPriority.Highest };

            _thread.Start();
        }

        public void Stop()
        {
            Running = false;
        }
    }

    internal class Time
    {
        private static readonly object Locker = new object();

        private static Lazy<Time> _lazy = new Lazy<Time>(() => new Time());

        private static volatile bool _isInitialized = false;
        public static Time Instance => _lazy.Value;

        public Stopwatch StartTime { get; private set; }
        public int FPS { get; private set; }
        public int IntervalMs { get; private set; }
        public double DeltaTime { get; private set; }

        public const int DefaultFPS = 20;

        private Time(int fps = DefaultFPS)
        {
            StartTime = new Stopwatch();
            StartTime.Start();
            FPS = fps;
            DeltaTime = (double)1 / FPS;
            IntervalMs = (int)Math.Truncate(DeltaTime * 1000);
        }

        public static void Reset()
        {
            lock (Locker)
            {
                _lazy = new Lazy<Time>(() => new Time());
                _isInitialized = false;
            }
        }

        public static void Initialize(int fps)
        {
            if (!_isInitialized)
                lock (Locker)
                    if (!_isInitialized)
                    {
                        _lazy = new Lazy<Time>(() => new Time(fps));
                        _isInitialized = true;
                        return;
                    }
            throw new InvalidOperationException();
        }
    }

    internal class Player
    {
        public string PlayerName { get; set; }
        public int Health { get; set; }
        public double Speed { get; set; }
        public double MaxSpeed { get; set; }
        public int PlayerId { get; set; }
        public int Rotation { get; set; }

        public double SpeedBoost { get; set; }
        public double AngleChange { get; set; }

        public bool IsFreezing { get; set; }

        public Player(string playerName, int health, float speed, float maxSpeed, int playerId, double speedBoost, double angleChange)
        {
            PlayerName = playerName;
            Health = health;
            Speed = speed;
            MaxSpeed = maxSpeed;
            PlayerId = playerId;
            SpeedBoost = speedBoost;
            AngleChange = angleChange;
        }

        public Player(string playerName, int health, float speed, float maxSpeed, double speedBoost, double angleChange)
        {
            PlayerName = playerName;
            Health = health;
            Speed = speed;
            MaxSpeed = maxSpeed;
            SpeedBoost = speedBoost;
            AngleChange = angleChange;
        }

        public void Rotate(int rotation) => Rotation = rotation;
    }
}