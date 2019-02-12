using System;
using System.Collections.Generic;
using System.Threading;

namespace TrafficLights
{
    class Program
    {
        private static int SPAWN_DELAY = 5 * 1000;

        public static void StartLogic()
        {

            Joint joint = new Joint();

            foreach (Road road in joint.Roads)
            {
                
                Thread carGenerator = new Thread(() => seedCars(road));
                carGenerator.Start();
            }
        }

        private static void seedCars(Road road)
        {
            while (true)
            {
                Random rnd = new Random();
                Thread.Sleep(rnd.Next(500, 1500));
                road.spawnVehicle();
            }
            
        }

    }
}
