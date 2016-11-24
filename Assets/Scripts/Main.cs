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

    private MtxControllerClient ctrlClient = null;
    private MtxRendererServer renderServer = null;

    private void Awake() {
        Application.runInBackground = true;
        renderServer = MtxRendererServer.Instance;
        ctrlClient = MtxControllerClient.Instance;
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
            ctrlClient.MovePlayer(1, MtxControllerService.Direction.UP);
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            ctrlClient.MovePlayer(1, MtxControllerService.Direction.RIGHT);
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            ctrlClient.MovePlayer(1, MtxControllerService.Direction.DOWN);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            ctrlClient.MovePlayer(1, MtxControllerService.Direction.LEFT);
        } else if (Input.GetKeyDown(KeyCode.W)) {
            ctrlClient.JumpPlayer(1, MtxControllerService.Direction.UP);
        } else if (Input.GetKeyDown(KeyCode.D)) {
            ctrlClient.JumpPlayer(1, MtxControllerService.Direction.RIGHT);
        } else if (Input.GetKeyDown(KeyCode.S)) {
            ctrlClient.JumpPlayer(1, MtxControllerService.Direction.DOWN);
        } else if (Input.GetKeyDown(KeyCode.A)) {
            ctrlClient.JumpPlayer(1, MtxControllerService.Direction.LEFT);
        } else if (Input.GetKeyDown(KeyCode.R)) {
            ctrlClient.ResetLevel();
        }


    }

    private void OnApplicationQuit() {
        TearDown();
    }

}
