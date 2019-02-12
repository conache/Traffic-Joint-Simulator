using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulatorUI
{
    public partial class MainForm : Form, IDisposable
    {
        static bool shouldExit = false;
        public MainForm()
        {
            InitializeComponent();
            //CreateCars(); - demo
            CreateTrafficLights();
            Thread thread = new Thread(new ThreadStart(Redraw));
            thread.Start();
        }

        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);

        //}
        private void Redraw()
        {
            while (!shouldExit)
            {
                Thread.Sleep(10);
                drawingArea.Invalidate();
            }

        }

        #region Demo
        //Demo Function
        private void CreateCars()
        {
            List<Thread> threads = new List<Thread>();

            DrawableCar policeCar = DrawableCarManager.CreateNewCar(1936 - ImageManager.Width, 400, 4, Category.PoliceCar, State.MovingLeft);
            DrawableCar blueCar = DrawableCarManager.CreateNewCar(0, 500, 4, Category.BlueCar, State.MovingRight);
            DrawableCar blueCar2 = DrawableCarManager.CreateNewCar(this.Width + 2 * ImageManager.Width, 1000, 3, Category.BlueCar, State.MovingUp);


            Thread t1 = new Thread(() => UpdateCar1(blueCar));
            Thread t2 = new Thread(() => UpdateCar2(policeCar));
            Thread t3 = new Thread(() => UpdateCar3(blueCar2));
            t1.Start();
            t2.Start();
            t3.Start();
        }

        //Demo Function
        private void UpdateCar1(DrawableCar car)
        {
            double advancedPixels = 0;
            int imgWidth = ImageManager.Width;
            bool turnedRight = false;
            while (advancedPixels < this.Width / 2 - 3 * imgWidth)
            {
                Thread.Sleep(10);
                car.MoveForward();
                advancedPixels += imgWidth;
                if (!turnedRight && this.Width / 2 - 2 * ImageManager.Width - 3 * imgWidth < advancedPixels)
                {
                    car.SignalRight();
                    turnedRight = true;
                }
            }
            Thread.Sleep(2000);
            car.TurnRight();
            advancedPixels = 0;
            while (advancedPixels < this.Height - 2 * imgWidth)
            {
                Thread.Sleep(10);
                car.MoveForward();
                advancedPixels += imgWidth;
                if (turnedRight && advancedPixels > 2 * imgWidth)
                {
                    car.StopSignal();
                    turnedRight = false;
                }
            }

            car.Dispose();

        }
        private void UpdateCar2(DrawableCar car)
        {
            double advancedPixels = 0;
            int imgWidth = ImageManager.Width;
            //    bool turnedRight = false;
            while (advancedPixels < this.Width)
            {
                car.MoveForward();
                advancedPixels += imgWidth;
            }
            car.Dispose();

        }

        private void UpdateCar3(DrawableCar car)
        {
            double advancedPixels = this.Width;
            int imgWidth = ImageManager.Width;
            //    bool turnedRight = false;
            while (advancedPixels > 0)
            {
                car.MoveForward();
                advancedPixels -= imgWidth;

            }

            car.Dispose();


        }

        private void CreateTrafficLights() { 
        //{
        //    int X = 1900;
        //    int y = 500;

        //    int centreX = X / 2;
        //    int centreY = y / 2;


        //    List<DrawableTrafficLight> tls = new List<DrawableTrafficLight>();

        //    DrawableTrafficLight tl1 = new DrawableTrafficLight(Category.TrafficLightUp, State.Green, centreX - 4 * ImageManager.Height, centreY + ImageManager.Width/2);
        //    DrawableTrafficLight tl2 = new DrawableTrafficLight(Category.TrafficLightLeft, State.Red, centreX - 5 * ImageManager.Height, centreY + 11*ImageManager.Width/2);
        //    DrawableTrafficLight tl3 = new DrawableTrafficLight(Category.TrafficLightDown, State.Green, centreX + 2 * ImageManager.Height, centreY + 11 * ImageManager.Width / 2);
        //    DrawableTrafficLight tl4 = new DrawableTrafficLight(Category.TrafficLightRight, State.Red, centreX + 2 * ImageManager.Height, centreY + ImageManager.Width / 2);
        //    tls.Add(tl1);
        //    tls.Add(tl2);
        //    tls.Add(tl3);
        //    tls.Add(tl4);
        //    foreach(var tl in tls)
        //    {
        //        lock(DrawingArea.ObjectsToDraw)
        //        { 
        //        DrawingArea.ObjectsToDraw.Add(tl);
        //        }

        //        Thread thread = new Thread(() => UpdateTrafficLights(tl));
        //        thread.Start();
        //    }

        }

        private void UpdateTrafficLights(DrawableTrafficLight tl)
        {
            while (!shouldExit)
            {
                
                if(tl.Color == State.Yellow)
                {
                    Thread.Sleep(1000);
                }
                else
                {

                    Thread.Sleep(5000);
                }
                tl.ChangeNextColor();
            }
        }
        #endregion
        public void Dispose()
        {
            shouldExit = true;

        }
    }


}
