using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;

public class PipeWork : MonoBehaviour
{
    NamedPipeServerStream server;
    BinaryReader br;
    BinaryWriter bw;
    
    // Use this for initialization
    void Start()
    {
        server = new NamedPipeServerStream("NPtest");


        Console.WriteLine("Waiting for connection...");
        server.WaitForConnection();
        Console.WriteLine("Connected.");
        var br = new BinaryReader(server);
        var bw = new BinaryWriter(server);


    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            var len = (int)br.ReadUInt32();            // Read string length
            var str = new string(br.ReadChars(len));    // Read string

            Console.WriteLine("Read: \"{0}\"", str);

            str = new string(str.Reverse().ToArray());  // Just for fun

            var buf = Encoding.ASCII.GetBytes(str);     // Get ASCII byte array     
            bw.Write((uint)buf.Length);                // Write string length
            bw.Write(buf);                              // Write string
            Console.WriteLine("Wrote: \"{0}\"", str);
        }
        catch (EndOfStreamException)
        {
            //break;                    // When client disconnects
            endstream();
        }
    }





    void endstream()
    {
        Console.WriteLine("Client disconnected.");
        server.Close();
        server.Dispose();
    }

}
}