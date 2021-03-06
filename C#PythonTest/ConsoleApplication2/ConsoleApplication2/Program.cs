﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;

namespace ConsoleApplication2
{
    class Program
    {
        //private NamedPipeServerStream server1 = null; //UnityPipe
        //private NamedPipeServerStream server2 = null; //PythonPipe
        

        static void Main(string[] args)
        {
            //Start Pipes
            NamedPipeServerStream server1 = new NamedPipeServerStream("UnityPipe"); //UnityPipe
            NamedPipeServerStream server2 = new NamedPipeServerStream("NPtest"); //PythonPipe
            Console.WriteLine("Started server1");
            Console.WriteLine("Started server2");

            //Connect Unity Pipe
           ConnectUnityPipe(server1);

            //Connect Python Pipe
            ConnectPythonPipe(server2);
            //Console.ReadLine();

            //Read Python Pipe

            for (int i = 0; i <5000; i--) //infinite loop
            {
                //char[] receivedData;
                string data = ReadPythonPipe(server2);
                //Console.WriteLine("Main recieved: {0}", data);
                //Console.WriteLine("Press Enter to send to Unity");
                //Console.ReadLine();
                //Console.WriteLine("Raw data = " + data);

                //Clean up [] characters
                //string cleanData = CleanMessage(data);
                //Console.WriteLine("Clean data = " + cleanData);

                //Send to Unity Pipe Continuously
            SendUnityPipe(server1, data);
                //Console.WriteLine("Press Enter read from Python");
                //Console.ReadLine();
            }
        }

        static void ConnectUnityPipe(NamedPipeServerStream server)
        {
            
            Console.WriteLine("Waiting for connection to Unity...");
            server.WaitForConnection();
            Console.WriteLine("Connected to Unity.");
        }

        static void ConnectPythonPipe(NamedPipeServerStream server)
        {

            Console.WriteLine("Waiting for connection to Python...");
            server.WaitForConnection();
            Console.WriteLine("Connected to Python.");
        }

        static string ReadPythonPipe(NamedPipeServerStream server)
        {
            

            //Reading
            var br = new BinaryReader(server);
            var bw = new BinaryWriter(server);

            while (true)
            {
                try
                {
                    var len = (int)br.ReadUInt32();            // Read string length
                    //Console.WriteLine("Length: {0}", len.ToString());
                    var str = new string(br.ReadChars(len));    // Read string

                    Console.WriteLine("Read: \"{0}\"", str);

                    //str = new string(str.Reverse().ToArray());  // Just for fun

                    //var buf = Encoding.ASCII.GetBytes(str);     // Get ASCII byte array     
                    //bw.Write((uint)buf.Length);                // Write string length
                    //bw.Write(buf);                              // Write string
                    //Console.WriteLine("Wrote: \"{0}\"", str);

                    //for (int i=0; i<data.Length; i++)
                    //{
                    //    return data[i];
                    //}
                    //char[] data = str.ToCharArray;
                    string data = str;
                    return data;

                }
                catch (EndOfStreamException)
                {
                    break;                    // When client disconnects
                }
            }

            Console.WriteLine("Client disconnected.");
            server.Close();
            server.Dispose();

            //char[] empty = null;
            string empty = null;
            return empty; 
            


        }
            
        static void SendUnityPipe(NamedPipeServerStream server, string data)
        {
            
            byte[] msg = Encoding.ASCII.GetBytes(data); // convert to byte
  
            //WRITE TO PIPE 
            server.Write(msg, 0, msg.Length);
            
            //foreach (var item in msg)
            //{
            //    Console.WriteLine("Wrote: {0}", item.ToString());

            //}
        }

        static string CleanMessage(string data)
        {
            
            data = data.Substring(2, data.Length - 4);
            var charsToRemove = new string[] { "[","]"};
            foreach (var c in charsToRemove)
            {
                data = data.Replace(c, string.Empty);
            }
            string cleanedString = System.Text.RegularExpressions.Regex.Replace(data, @"\s+", " ");
            cleanedString = cleanedString.Replace(' ', ',');

            if (cleanedString[0] == ',')
            {
                cleanedString = cleanedString.Substring(1, cleanedString.Length - 1);
            }
            char last = cleanedString[cleanedString.Length - 1];
            if (last == ',')
            {
                cleanedString = cleanedString.Substring(0, cleanedString.Length - 1);
            }

        
            //data = data.Replace(' ', ',');
            //data = data.Replace('[', string.Empty).Replace(']', ' ').Replace(' ',',');

            //data = data.Replace(",,,,", ',');
            StringBuilder b = new StringBuilder(data);
            //b.Replace(",,,,", "").Replace(",,,", ",").Replace(",,", ",");
            //data = b.ToString();
            //data = data.Substring(2, data.Length - 2);

            //cleanString = data.Replace('[', ' ').Replace(']', ';');
            return cleanedString;
        }
    }
        
}
