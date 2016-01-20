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

public class Client : MonoBehaviour
{
    private NamedPipeClientStream PipelineStream = null;

    private void Start()
    {
        Debug.Log("Opening pipe");
        OpenPipe();

        ReadAsync();
    }

    private void OnDestroy()
    {
        ClosePipe();
    }

    private void OpenPipe()
    {
        Debug.Log("Inside Opening Pipe");
        PipelineStream = new NamedPipeClientStream(".", "Pipeline", PipeDirection.In, PipeOptions.Asynchronous);
        Debug.Log("Created NamedPipeClientStream");
        PipelineStream.Connect();
        Debug.Log("Connecting pipe");
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

    private void ReadAsync()
    {
        if (PipelineStream != null && PipelineStream.IsConnected)
        {
            Debug.Log("Reading Pipe data");
            byte[] buffer = new byte[1];
            PipelineStream.BeginRead(buffer, 0, 1, ReadAsyncCallback, null);
        }
    }

    private void ReadAsyncCallback(IAsyncResult result)
    {
        int amountRead = PipelineStream.EndRead(result);

        if (amountRead > 0)
            ReadAsync();
    }
}
