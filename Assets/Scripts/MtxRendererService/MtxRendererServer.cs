using UnityEngine;
using System.Collections;

using System.Threading;
using Thrift.Server;
using Thrift.Transport;
using Thrift.Protocol;


public class MtxRendererServer : Singleton<MtxRendererServer> {

    public int port;
    public MtxRendererHandler handler;

    private TServerTransport transport = null;
    private TServer server = null;
    private Thread serverThread = null;

    public void Run() {
        var processor = new MtxRendererService.MtxRendererService.Processor(handler);

        transport = new TServerSocket(port);
        server = new TThreadPoolServer(processor, transport);

        serverThread = new Thread(() => {
            Debug.Log ("Starting MtxRenderer-Service...");
            server.Serve();
            transport.Close();
        }) { IsBackground = true };

        serverThread.Start();
    }

    public void PingServer() {
        Thread clientThread = new Thread(() => {
            var transport = new TSocket("localhost", port);
            var protocol = new TBinaryProtocol(transport);
            var client = new MtxRendererService.MtxRendererService.Client(protocol);
            transport.Open();
            client.Ping();
            transport.Close();
        }) { IsBackground = true };

        clientThread.Start();
    }

    public void Stop() {
        Debug.Log ("MtxRenderer-Service shuts down...");

        server.Stop();
/*      PingServer();
        Thread.Sleep(200);
        serverThread.Abort();
        transport.Close();*/
    }
}
