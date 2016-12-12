using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;


public class ShutdownAct : AtomicAct {

    public override void Perform() {
        Main.Instance.Quit();
    }
}


public class Main : Singleton<Main> {

    private bool finished = false;
    private object finishedLock = new object();

    private ControllerClient ctrlClient = null;
    private RendererServer renderServer = null;

    private void Awake() {
        Application.runInBackground = true;
        renderServer = RendererServer.Instance;
        ctrlClient = ControllerClient.Instance;
    }

    private void Start() {
        renderServer.Run();
        ctrlClient.Connect();
    }

    public void Quit() {
        TearDown();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void TearDown() {
        lock(finishedLock) {
            if(!finished) {
                finished = true;
                ctrlClient.Disconnect();
                renderServer.Stop();
            }
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            ctrlClient.MovePlayer(1, ControllerService.Direction.UP);
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            ctrlClient.MovePlayer(1, ControllerService.Direction.RIGHT);
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            ctrlClient.MovePlayer(1, ControllerService.Direction.DOWN);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            ctrlClient.MovePlayer(1, ControllerService.Direction.LEFT);
        } else if (Input.GetKeyDown(KeyCode.W)) {
            ctrlClient.JumpPlayer(1, ControllerService.Direction.UP);
        } else if (Input.GetKeyDown(KeyCode.D)) {
            ctrlClient.JumpPlayer(1, ControllerService.Direction.RIGHT);
        } else if (Input.GetKeyDown(KeyCode.S)) {
            ctrlClient.JumpPlayer(1, ControllerService.Direction.DOWN);
        } else if (Input.GetKeyDown(KeyCode.A)) {
            ctrlClient.JumpPlayer(1, ControllerService.Direction.LEFT);
        } else if (Input.GetKeyDown(KeyCode.R)) {
            ctrlClient.ResetLevel();
        } else if (Input.GetKeyDown(KeyCode.F5)) {
            ctrlClient.ReloadGame();
        }


    }

    private void OnApplicationQuit() {
        TearDown();
    }

}
