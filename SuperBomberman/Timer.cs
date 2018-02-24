using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBomberman
{
    public class DelayedAction
    {
        public DelayedAction(Action action, float delay)
        {
            Action = action;
            Delay = delay;
        }

        public Action Action { get; private set; }
        public float Delay { get; private set; }
        bool isStart = false;
        bool isHappens = false;

        public void Start()
        {
            isStart = true;
        }

        public void Update(float deltaTime)
        {
            if (!isStart)
            {
                return;
            }

            Delay -= deltaTime;

            if (!isHappens && Delay <= 0)
            {
                Action();
                isHappens = true;
            }
            
        }
    }
}
