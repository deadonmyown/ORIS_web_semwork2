using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUILibrary
{
    public class Time
    {
        private static readonly object Locker = new();

        private static  Lazy<Time> _lazy = new Lazy<Time>(() => new Time());

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
}
