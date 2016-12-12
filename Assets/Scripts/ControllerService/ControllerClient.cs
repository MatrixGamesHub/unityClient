using UnityEngine;
using System;
using System.Net.Sockets;
using System.Collections.Generic;

using Thrift;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;


public class ControllerClient : Singleton<ControllerClient> {
    public int port;

    private TTransport transport = null;
    private TProtocol protocol = null;
    private ControllerService.ControllerService.Client client = null;
    private short rendererId = -1;

    private bool connected=false;
    public bool Connected { get { return connected; } }

    private void Abort() {
        Debug.LogError("Connection to game server lost...");
        connected = false;
    }

    private void CallClientCommand(Action action) {
        if (!connected) {
            return;
        }

        try {
            action();
        } catch (Exception) {
            Abort();
        }
    }

    private TResult CallClientCommand<TResult>(Func<TResult> func) {
        if (connected) {
            try {
                return func();
            } catch(ControllerService.GameError e) {
                Debug.LogError(e);
            } catch(SocketException) {
                Abort();
            }
        }
        return default(TResult);
    }

    public void Connect() {
        if (connected) {
            return;
        }

        try {
            transport = new TSocket("localhost", port);
            protocol = new TBinaryProtocol(transport);
            client = new ControllerService.ControllerService.Client(protocol);

            transport.Open();

            connected = true;
            rendererId = CallClientCommand<short>(() => client.ConnectRenderer("localhost", RendererServer.Instance.port));

            LoadGame("Sokoban");

        } catch (SocketException) {
            Debug.LogWarning("No game server available.");
        }
    }

    public void Disconnect() {
        CallClientCommand(() => client.DisconnectRenderer(rendererId));
    }

    public List<string> GetGames() {
        return CallClientCommand<List<string>>(() => client.GetGames());
    }

    public ControllerService.GameInfo GetGameInfo(string name) {
        return CallClientCommand<ControllerService.GameInfo>(() => client.GetGameInfo(name));
    }

    public void LoadGame(string name) {
        CallClientCommand(() => client.LoadGame(name));
    }

    public void ReloadGame() {
        CallClientCommand(() => client.ReloadGame());
    }

    public void MovePlayer(sbyte number, ControllerService.Direction direction) {
        CallClientCommand(() => client.MovePlayer(number, direction));
    }

    public void JumpPlayer(sbyte number, ControllerService.Direction direction) {
        CallClientCommand(() => client.JumpPlayer(number, direction));
    }

    public void ResetLevel() {
        CallClientCommand(() => client.ResetLevel());
    }
}