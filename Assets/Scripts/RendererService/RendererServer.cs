using UnityEngine;
using System.Collections;

using System.Threading;
using Thrift.Server;
using Thrift.Transport;
using Thrift.Protocol;


public class RendererServer : Singleton<RendererServer> {

    public int port;
    public RendererHandler handler;

    private TServerTransport transport = null;
    private TServer server = null;
    private Thread serverThread = null;

    public void Run() {
        var processor = new RendererService.RendererService.Processor(handler);

        transport = new TServerSocket(port);
        server = new TThreadPoolServer(processor, transport);

        serverThread = new Thread(() => {
            Debug.Log ("Starting Renderer-Service...");
            server.Serve();
            transport.Close();
        }) { IsBackground = true };

        serverThread.Start();
    }

    public void PingServer() {
        Thread clientThread = new Thread(() => {
            var transport = new TSocket("localhost", port);
            var protocol = new TBinaryProtocol(transport);
            var client = new RendererService.RendererService.Client(protocol);
            transport.Open();
            client.Ping();
            transport.Close();
        }) { IsBackground = true };

        clientThread.Start();
    }

    public void Stop() {
        Debug.Log ("Renderer-Service shuts down...");

        server.Stop();
/*      PingServer();
        Thread.Sleep(200);
        serverThread.Abort();
        transport.Close();*/
    }
}
