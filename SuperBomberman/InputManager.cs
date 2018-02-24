using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

namespace SuperBomberman
{
    class InputManager
    {
        KeyboardState currentKeyState;
        KeyboardState prevKeyState;

        private static InputManager instance;

        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputManager();
                }
                return instance;
            }
        }

        public void Update()
        {
            prevKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
        }

        public bool KeyPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                {
                    return true;
                }
            }
            return false;
        }

        public bool KeyReleased(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                {
                    return true;
                }
            }
            return false;
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (prevKeyState.IsKeyDown(key))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
