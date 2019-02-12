using System;

namespace TrafficLights
{
    class TrafficLight : Subscribable
    {
        public enum Color
        {
            None = 0,
            Green = 1,
            Yellow = 2,
            Red = 3,
        };

        private Color _currentColor;
        private Color _previousMarginalColor;
        private SimulatorUI.DrawableTrafficLight _drawaTrafficLight;

        public TrafficLight(Joint.RoadLocation position, Color color = Color.Green)
        {
            if ( color == Color.Yellow )
            {
                throw new Exception("Cannot initialize traffic light with the yellow Color. Please use red or green for initialization.");
            }

            this.CurrentColor = color;
            _drawaTrafficLight = SimulatorUI.DrawableTrafficLightsManager.CreateTrafficLight(position, this.CurrentColor);
        }

        public void ChangeToNextColor()
        {
            Color nextColor = this._currentColor == Color.Red ? Color.Green : this._currentColor+=1;
            this.CurrentColor = nextColor;
            this._drawaTrafficLight.ChangeNextColor();
        }

        public bool hasColor(TrafficLight.Color color)
        {
            return color == this.CurrentColor;
        }

        public Color CurrentColor
        {
            get
            {
                return this._currentColor;
            }

            set 
            {
                Color currentColor = this.CurrentColor;

                if (currentColor == Color.None)
                {
                    this._previousMarginalColor = value == Color.Red ? Color.Green : Color.Red;
                }

                if (currentColor != Color.Yellow)
                {
                    this._previousMarginalColor = currentColor;
                }

                this._currentColor = value;
                this.NotifySubscribers();
            }
        }

    }
}