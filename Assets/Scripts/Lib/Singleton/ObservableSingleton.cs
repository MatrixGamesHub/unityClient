using UnityEngine;
using System;
using System.Collections.Generic;


public class ObservableSingleton<T, TObserver1> : Singleton<T> where T : MonoBehaviour  {
    private List<TObserver1> observerList1 = new List<TObserver1>();

    public void ForEachObserver(Action<TObserver1> action) {
        observerList1.ForEach(action);
    }

    public virtual void RegisterObserver(object observer) {
        if(observer is TObserver1) {
            if(!observerList1.Contains((TObserver1)observer)) {
                observerList1.Add((TObserver1)observer);
            }
        }
    }

    public virtual void UnregisterObserver(object observer) {
        if(observer is TObserver1) {
            if(observerList1.Contains((TObserver1)observer)) {
                observerList1.Remove((TObserver1)observer);
            }
        }
    }
}


public class ObservableSingleton<T, TObserver1, TObserver2> :
             ObservableSingleton<T, TObserver1> where T : MonoBehaviour {
    private List<TObserver2> observerList2 = new List<TObserver2>();

    public void ForEachObserver(Action<TObserver2> action) {
        observerList2.ForEach(action);
    }

    public override void RegisterObserver(object observer) {
        base.RegisterObserver(observer);

        if(observer is TObserver2) {
            if(!observerList2.Contains((TObserver2)observer)) {
                observerList2.Add((TObserver2)observer);
            }
        }
    }

    public override void UnregisterObserver(object observer) {
        base.UnregisterObserver(observer);

        if(observer is TObserver2) {
            if(!observerList2.Contains((TObserver2)observer)) {
                observerList2.Remove((TObserver2)observer);
            }
        }
    }
}


public class ObservableSingleton<T, TObserver1, TObserver2, TObserver3> :
             ObservableSingleton<T, TObserver1, TObserver2> where T : MonoBehaviour {
    private List<TObserver3> observerList3 = new List<TObserver3>();

    public void ForEachObserver(Action<TObserver3> action) {
        observerList3.ForEach(action);
    }

    public override void RegisterObserver(object observer) {
        base.RegisterObserver(observer);

        if(observer is TObserver3) {
            if(!observerList3.Contains((TObserver3)observer)) {
                observerList3.Add((TObserver3)observer);
            }
        }
    }

    public override void UnregisterObserver(object observer) {
        base.UnregisterObserver(observer);

        if(observer is TObserver3) {
            if(!observerList3.Contains((TObserver3)observer)) {
                observerList3.Remove((TObserver3)observer);
            }
        }
    }
}


public class ObservableSingleton<T, TObserver1, TObserver2, TObserver3, TObserver4> :
             ObservableSingleton<T, TObserver1, TObserver2, TObserver3> where T : MonoBehaviour {
    private List<TObserver4> observerList4 = new List<TObserver4>();

    public void ForEachObserver(Action<TObserver4> action) {
        observerList4.ForEach(action);
    }

    public override void RegisterObserver(object observer) {
        base.RegisterObserver(observer);

        if(observer is TObserver4) {
            if(!observerList4.Contains((TObserver4)observer)) {
                observerList4.Add((TObserver4)observer);
            }
        }
    }

    public override void UnregisterObserver(object observer) {
        base.UnregisterObserver(observer);

        if(observer is TObserver4) {
            if(!observerList4.Contains((TObserver4)observer)) {
                observerList4.Remove((TObserver4)observer);
            }
        }
    }
}


public class ObservableSingleton<T, TObserver1, TObserver2, TObserver3, TObserver4, TObserver5> :
             ObservableSingleton<T, TObserver1, TObserver2, TObserver3, TObserver4> where T : MonoBehaviour {
    private List<TObserver5> observerList5 = new List<TObserver5>();

    public void ForEachObserver(Action<TObserver5> action) {
        observerList5.ForEach(action);
    }

    public override void RegisterObserver(object observer) {
        base.RegisterObserver(observer);

        if(observer is TObserver5) {
            if(!observerList5.Contains((TObserver5)observer)) {
                observerList5.Add((TObserver5)observer);
            }
        }
    }

    public override void UnregisterObserver(object observer) {
        base.UnregisterObserver(observer);

        if(observer is TObserver5) {
            if(!observerList5.Contains((TObserver5)observer)) {
                observerList5.Remove((TObserver5)observer);
            }
        }
    }
}


public class ObservableSingleton<T, TObserver1, TObserver2, TObserver3, TObserver4, TObserver5, TObserver6> :
             ObservableSingleton<T, TObserver1, TObserver2, TObserver3, TObserver4, TObserver5> where T : MonoBehaviour {
    private List<TObserver6> observerList6 = new List<TObserver6>();

    public void ForEachObserver(Action<TObserver6> action) {
        observerList6.ForEach(action);
    }

    public override void RegisterObserver(object observer) {
        base.RegisterObserver(observer);

        if(observer is TObserver6) {
            if(!observerList6.Contains((TObserver6)observer)) {
                observerList6.Add((TObserver6)observer);
            }
        }
    }

    public override void UnregisterObserver(object observer) {
        base.UnregisterObserver(observer);

        if(observer is TObserver6) {
            if(!observerList6.Contains((TObserver6)observer)) {
                observerList6.Remove((TObserver6)observer);
            }
        }
    }
}


public class ObservableSingleton<T, TObserver1, TObserver2, TObserver3, TObserver4, TObserver5, TObserver6, TObserver7> :
             ObservableSingleton<T, TObserver1, TObserver2, TObserver3, TObserver4, TObserver5, TObserver6> where T : MonoBehaviour {
    private List<TObserver7> observerList7 = new List<TObserver7>();

    public void ForEachObserver(Action<TObserver7> action) {
        observerList7.ForEach(action);
    }

    public override void RegisterObserver(object observer) {
        base.RegisterObserver(observer);

        if(observer is TObserver7) {
            if(!observerList7.Contains((TObserver7)observer)) {
                observerList7.Add((TObserver7)observer);
            }
        }
    }

    public override void UnregisterObserver(object observer) {
        base.UnregisterObserver(observer);

        if(observer is TObserver7) {
            if(!observerList7.Contains((TObserver7)observer)) {
                observerList7.Remove((TObserver7)observer);
            }
        }
    }
}


public class ObservableSingleton<T, TObserver1, TObserver2, TObserver3, TObserver4, TObserver5, TObserver6, TObserver7, TObserver8> :
             ObservableSingleton<T, TObserver1, TObserver2, TObserver3, TObserver4, TObserver5, TObserver6, TObserver7> where T : MonoBehaviour {
    private List<TObserver8> observerList8 = new List<TObserver8>();

    public void ForEachObserver(Action<TObserver8> action) {
        observerList8.ForEach(action);
    }

    public override void RegisterObserver(object observer) {
        base.RegisterObserver(observer);

        if(observer is TObserver8) {
            if(!observerList8.Contains((TObserver8)observer)) {
                observerList8.Add((TObserver8)observer);
            }
        }
    }

    public override void UnregisterObserver(object observer) {
        base.UnregisterObserver(observer);

        if(observer is TObserver8) {
            if(!observerList8.Contains((TObserver8)observer)) {
                observerList8.Remove((TObserver8)observer);
            }
        }
    }
}
