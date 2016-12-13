using UnityEngine;

public class Exit : MonoBehaviour {
    private bool locked = false;
    public bool Locked {
      get {
        return locked;
      }

      set {
        locked = value;
      }
    }

}
