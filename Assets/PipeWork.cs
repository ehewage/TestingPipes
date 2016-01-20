using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;

public class PipeWork : MonoBehaviour
{
    NamedPipeServerStream server = null;
    BinaryReader br = null;
    BinaryWriter bw = null;
    
    // Use this for initialization
    void Start()
    {
        Debug.Log("Starting Server");
        server = new NamedPipeServerStream("NPtest");

        //Console.WriteLine("Waiting for connection...");
        Debug.Log("Waiting for connection...");
        server.WaitForConnection();

        //Console.WriteLine("Connected.");
        Debug.Log("Connected.");

        br = new BinaryReader(server);
        bw = new BinaryWriter(server);

        while (true)
        {
            try
            {
                var len = (int)br.ReadUInt32();            // Read string length
                var str = new string(br.ReadChars(len));    // Read string

                //Console.WriteLine("Read: \"{0}\"", str);
                Debug.Log(String.Format("Read: {0}", str));
                str = new string(str.Reverse().ToArray());  // Just for fun

                var buf = Encoding.ASCII.GetBytes(str);     // Get ASCII byte array     
                bw.Write((uint)buf.Length);                // Write string length
                bw.Write(buf);                              // Write string
                //Console.WriteLine("Wrote: \"{0}\"", str);
                Debug.Log(String.Format("Wrote: {0}", str));
            }
            catch (EndOfStreamException)
            {
                break;                    // When client disconnects
            }
        }

        //Console.WriteLine("Client disconnected.");
        Debug.Log("Client disconnected.");
        server.Close();
        server.Dispose();


    }

    void Piping()
    {
        //try
        //{
        //    var len = (int)br.ReadUInt32();            // Read string length
        //    var str = new string(br.ReadChars(len));    // Read string

        //    Debug.Log("Read " + str);

        //    str = new string(str.Reverse().ToArray());  // Just for fun

        //    var buf = Encoding.ASCII.GetBytes(str);     // Get ASCII byte array     
        //    bw.Write((uint)buf.Length);                // Write string length
        //    bw.Write(buf);                              // Write string
        //    Debug.Log("Wrote " + str);
        //}
        //catch (EndOfStreamException)
        //{
        //    //break;                    // When client disconnects
        //    Debug.Log("Client disconnected.");
        //    server.Close();
        //    server.Dispose();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //try
        //{
        //    var len = (int)br.ReadUInt32();            // Read string length
        //    var str = new string(br.ReadChars(len));    // Read string

        //    Debug.Log("Read "+ str);

        //    str = new string(str.Reverse().ToArray());  // Just for fun

        //    var buf = Encoding.ASCII.GetBytes(str);     // Get ASCII byte array     
        //    bw.Write((uint)buf.Length);                // Write string length
        //    bw.Write(buf);                              // Write string
        //    Debug.Log("Wrote "+ str);
        //}
        //catch (EndOfStreamException)
        //{
        //    //break;                    // When client disconnects
        //    endstream();
        //}
    }





    


}