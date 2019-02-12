using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorUI
{
    public enum Category
    {
        BlueCar,
        PoliceCar,
        //PurpleCar
        TrafficLightLeft,
        TrafficLightRight,
        TrafficLightUp,
        TrafficLightDown,
    }

    // We need to mentain this order so we can have
    // checks like: if( state >= MovingDown && state < MovingLeft) to find the car's direction

    public enum State
    {
        MovingUp, // 0
        MovingUpSignalRight, // 1
        MovingUpSignalLeft, // 2
        MovingDown, // 3
        MovingDownSignalRight, // 4
        MovingDownSignalLeft, // 5
        MovingLeft, // 6
        MovingLeftSignalRight, // 7
        MovingLeftSignalLeft, // 8
        MovingRight, // 9
        MovingRightSignalRight, // 10 
        MovingRightSignalLeft, // 11
        Green, //12
        Yellow, //13
        Red, //14
    }

    public sealed class ImageManager
    {
        private static readonly ImageManager instance = new ImageManager();
        
        static ImageManager()
        {
            InitializeImageManager();
        }

        private ImageManager()
        {
        }

        private static Dictionary<Category, Dictionary<State, Bitmap>> _images = new Dictionary<Category, Dictionary<State, Bitmap>>();

        public  static int Width
        { get
            {
                if (_width != 70)
                {
                    _width = 70;
                }
                return _width;
            }
        }
        private static int _width = 70;

        public static int Height
        { get
            {
                if(_height != 40)
                {
                    _height = 40;
                }
                return _height;
            }
        }
        private static int _height = 40;


        public static int Y { get; }

        private static void InitializeImageManager()
        {
            string basePath = "C:\\workspace\\fmi\\master\\sem1\\progPar&Conc\\SimulatorUI\\SimulatorUI\\Images\\";
            foreach(Category category in Enum.GetValues(typeof(Category)))
            {
                ImageManager._images[category] = new Dictionary<State, Bitmap>();
                foreach(State state in Enum.GetValues(typeof(State)))
                {

                    if ((category < Category.TrafficLightLeft && state >= State.Green) ||
                        (category >= Category.TrafficLightLeft && state < State.Green))
                    { 
                        continue;
                    }

                    string imagePath = basePath + category.ToString() + state.ToString() + ".png";
                    Image img = Image.FromFile(imagePath);
                    if (img == null)
                        throw new Exception(imagePath);


                    Size size;
                    if (category < Category.TrafficLightLeft)
                    {
                        size = (state == State.MovingUp ||
                                state == State.MovingDown ||
                                state == State.MovingDownSignalLeft ||
                                state == State.MovingDownSignalRight ||
                                state == State.MovingUpSignalLeft ||
                                state == State.MovingUpSignalRight
                                ) ? new Size(40, 70) : new Size(70, 40);
                    }
                    else
                    {
                        size = (category == Category.TrafficLightDown || category == Category.TrafficLightUp) ? new Size(40, 70) : new Size(70, 40);
                    }
                    ImageManager._images[category].Add(state, new Bitmap(img,size ));
                }
            }

        }
        public static Bitmap GetImage(Category category, State state)
        {
            
            return _images[category][state];
        }
        

    }
}
