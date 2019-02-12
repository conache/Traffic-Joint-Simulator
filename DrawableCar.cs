using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorUI
{
    public class DrawableCar : IDrawable, IDisposable
    {
        #region Atrributes
        public Category CarType { get; set; }
        public State CarState {
            get;
            set; }

        public int Speed { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        private State _helperStateForSignal;
        private int _loopCounter = 0;
        #endregion

        #region Initialize
        public DrawableCar(int x, int y, int speed)
        {
            Initialize();
            this.X = x;
            this.Y = y;
            this.Speed = speed;
        }

        public DrawableCar(int x, int y, int speed, Category category, State state)
        {
            //Initialize();
            this.CarState = state;
            this.CarType = category;
            this.X = x;
            this.Y = y;
            this.Speed = speed;
        }

        private void Initialize()
        {
            CarType = Category.PoliceCar;
            CarState = State.MovingRightSignalRight;
        }
        #endregion

        #region Logic

        private void Advance()
        {

            if (CarState < State.MovingDown)
            {
                // Car goes up
                Y -= Speed;
                return;
            }
            if (CarState < State.MovingLeft)
            {
                // Car goes Down
                Y += Speed;
                return;
            }
            if (CarState < State.MovingRight)
            {
                // Car goes left
                X -= Speed;
                return;
            }
            // Car goes Right
            X += Speed;
        }

        public void MoveForward()
        {
            //Signal Flickering related
            _loopCounter = 0;
           _helperStateForSignal = CarState;

            double advancedPixels = 0;
            while (advancedPixels < ImageManager.Width)
            {
                Thread.Sleep(10);
                Advance();
                advancedPixels += Speed;
            }
         
        }

        public void SignalRight()
        {
            // if car is Moving without signaling
            if ((int)CarState % 3 == 0)
            {
                CarState = (State)((int)CarState + 1);
            }
            else
            {
                throw new Exception("Car is already Signaling! Current car state: " + CarState.ToString());
            }
        }

        public void SignalLeft()
        {
            // if car is Moving without signaling
            if ((int)CarState % 3 == 0)
            {
                CarState = (State)((int)CarState + 2);
            }
            else
            {
                throw new Exception("Car is already Signaling! Current car state: " + CarState.ToString());
            }
        }

        public void StopSignal()
        {
            switch ((int)CarState % 3)
            {
                case 0:
                    throw new Exception("Car is not Signaling! Current CarState: " + CarState.ToString());
                case 1:
                    // Signaling Right
                    CarState = (State)((int)CarState - 1);
                    break;
                case 2:
                    // Signaling Left
                    CarState = (State)((int)CarState - 2);
                    break;
            }
        }

        public void TurnLeft()
        {
            switch (CarState)
            {
                case State.MovingDownSignalLeft:
                    CarState = State.MovingRightSignalLeft;
                    break;
                case State.MovingLeftSignalLeft:
                    CarState = State.MovingDownSignalLeft;
                    break;
                case State.MovingUpSignalLeft:
                    CarState = State.MovingLeftSignalLeft;
                    break;
                case State.MovingRightSignalLeft:
                    CarState = State.MovingUpSignalLeft;
                    X += ImageManager.Width;
                    break;
                default:
                    throw new Exception("The car is not able to turn left. It should be in a Signal Left State. Current State: " + CarState.ToString());
            }
            Advance();
        }

        public void TurnRight()
        {
            switch(CarState)
            {
                case State.MovingDownSignalRight:
                    CarState = State.MovingLeftSignalRight;
                    break;
                case State.MovingLeftSignalRight:
                    CarState = State.MovingUpSignalRight;
                    break;
                case State.MovingUpSignalRight:
                    CarState = State.MovingRightSignalRight;
                    break;
                case State.MovingRightSignalRight:
                    CarState = State.MovingDownSignalRight;
                    X += ImageManager.Width;
                    break;
                default:
                    throw new Exception("The car is not able to turn left. It should be in a Signal Right State. Current State: " + CarState.ToString());
            }
            Advance();
        }

        private void UpdateStateForSignalEffect()
        {
            if ((int)CarState % 3 == 0)
            {
                return;   
            }

            _loopCounter++;

            if(_loopCounter % 5 == 0)
            {
                if ((int)_helperStateForSignal % 3 == 1)
                {
                    _helperStateForSignal = (State)((int)CarState - 1);
                }
                else
                if ((int)_helperStateForSignal % 3 == 2)
                {
                    _helperStateForSignal = (State)((int)CarState - 2);
                }
                else
                {
                    _helperStateForSignal = CarState;
                }
            }
        }
        #endregion
        
        public void Draw(System.Drawing.Graphics g)
        {
            if ((int)CarState % 3 == 0)
            {
                g.DrawImage(ImageManager.GetImage(CarType, CarState), X, Y);

            }
            else
            {
                UpdateStateForSignalEffect();
                g.DrawImage(ImageManager.GetImage(CarType, _helperStateForSignal), X, Y);
            }

        }

        public void Dispose()
        {
            DrawableCarManager.DisposeCar(this);
        }
    }
}
