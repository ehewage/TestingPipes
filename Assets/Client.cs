using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;



using System.Security.Principal;
using System.Diagnostics;
using System.ComponentModel;

// Edit> Project Settings> Player > Other Settings > API Compatibility Level > Net 2.0

public class Client : MonoBehaviour
{
    private NamedPipeClientStream PipelineStream = null;
    BinaryReader br= null;

    private void Start()
    {
        UnityEngine.Debug.Log("Opening pipe");
        OpenPipe();

        //WriteAsync();
        ReadAsync(br);
    }

    private void OnDestroy()
    {
        ClosePipe();
    }

    private void OpenPipe()
    {
        PipelineStream = new NamedPipeClientStream(".", "Pipeline", PipeDirection.In, PipeOptions.Asynchronous);
        
        UnityEngine.Debug.Log("Created NamedPipeClientStream");
        br = new BinaryReader(PipelineStream);

        //try
        //{
        PipelineStream.Connect();
       // }

        //catch (Win32Exception w)
        //{
        //    UnityEngine.Debug.Log(w.Message);
        //    UnityEngine.Debug.Log(w.ErrorCode.ToString());
        //    UnityEngine.Debug.Log(w.NativeErrorCode.ToString());
        //    UnityEngine.Debug.Log(w.StackTrace);
        //    UnityEngine.Debug.Log(w.Source);
        //    Exception e = w.GetBaseException();
        //    UnityEngine.Debug.Log(e.Message);
        //}
        UnityEngine.Debug.Log("Connecting pipe");
    }

    private void ClosePipe()
    {
        if (PipelineStream != null)
        {
            PipelineStream.Close();
            PipelineStream.Dispose();
            PipelineStream = null;
        }
    }

    private void ReadAsync(BinaryReader br)
    {
        if (PipelineStream != null && PipelineStream.IsConnected)
        {
            UnityEngine.Debug.Log("Reading Pipe data");
            //byte[] buffer = new byte[1];
            //PipelineStream.BeginRead(buffer, 0, 1, ReadAsyncCallback, null);

            //var br = new BinaryReader(PipelineStream);
            var len = (int)br.ReadUInt32();            // Read string length
            var str = new string(br.ReadChars(len));
            UnityEngine.Debug.Log(String.Format("Read: {0}", str));
        }
    }

    private void ReadAsyncCallback(IAsyncResult result)
    {
        int amountRead = PipelineStream.EndRead(result);

        if (amountRead > 0)
            ReadAsync(br);
    }

    private void WriteAsync()
    {
        //StreamWriter writer = new StreamWriter(PipelineStream);
        //writer.WriteLine("Hello");
        //writer.Flush();
    }
}
