using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleGameNamespace
{
    public class SimpleTimer
    {
        private DateTime _start;
        private float _elasped = -1;

        public TimeSpan Duration { get; private set; }

        public void Start(float elasped)
        {
            _elasped = elasped;
            _start = DateTime.Now;
            Duration = TimeSpan.Zero;

        }

        public void Update()
        {
            if (_elasped > 0)
            {
                Duration = DateTime.Now - _start;

                if (Duration.TotalSeconds > _elasped)
                {
                    _elasped = 0;
                }
            }
            else if (_elasped == 0)
            {
                _elasped = -1;
            }
        }

        public bool IsEvent()
        {
            return _elasped == 0;
        }

        public int TotalSeconds()
        {
            return (int)(_elasped - Duration.TotalSeconds);
        }

    }
    
}
