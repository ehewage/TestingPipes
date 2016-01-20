using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;

namespace TestServer
{
    public class Server
    {
        private const int BUFFER_SIZE = 1024;
        private NamedPipeServerStream PipelineStream = null;

        public Server()
        {
            OpenPipe();

            while (true)
            {
                if (PipelineStream != null && PipelineStream.IsConnected)
                {
                    Thread.Sleep(1000);
                }
            }

            ClosePipe();
        }

        private void OpenPipe()
        {
            PipelineStream = new NamedPipeServerStream("Pipeline", PipeDirection.Out, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous, BUFFER_SIZE, BUFFER_SIZE);
            PipelineStream.WaitForConnection();
        }

        private void ClosePipe()
        {
            if (PipelineStream != null)
            {
                PipelineStream.Flush();
                PipelineStream.Close();
                PipelineStream.Dispose();
                PipelineStream = null;
            }
        }
    }
}