using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CollisionDetection3D
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string message =
                "Note: This example is not finished. When it is complete, it will demonstrate how the Omnitree can " +
                "be used to detect collisions in 3D.";
                //"This is a 3D axis-aligned collision detection example using the Omnitree in Towel. " +
                //"It is not a physics example, objects just move away from a collision." +
                //Environment.NewLine + Environment.NewLine +
                //"This is just an example I scrapped together due to a user request. There is plenty " +
                //"of room for improvement." +
                //Environment.NewLine + Environment.NewLine +
                //"This example is built on top of the OpenTK project. Please check out and support " +
                //"OpenTK project. :)";
            MessageBox.Show(message);

            using (var window = new Window(1000, 1000, "Collision Detection 3D Example"))
            {
                window.Run(120.0);
            }
        }
    }
}
