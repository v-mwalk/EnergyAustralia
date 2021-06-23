using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;

namespace FestivalsStub
{
    public class Program
    {
        static void Main()
        {
            string baseAddress = "http://localhost:9000/";

            // Start server.  In this example we are self-hosting the Stub so that it is very portable.  We want the Stub to
            // be able to execute on any machine within the test environment and with minimal requirements or setup.  So self-hosting rather
            // than relying on IIS is the easiest solution.

            WebApp.Start<Startup>(url: baseAddress);

            Console.WriteLine("Stub running.  Browse to http://localhost:9000/swagger/ui/index to view APIs.");
            Console.WriteLine("Hit any key to end");
            Console.ReadLine();
        }
    }
}