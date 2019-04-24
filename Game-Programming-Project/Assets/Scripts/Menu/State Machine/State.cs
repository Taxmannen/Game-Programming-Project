using UnityEngine;

public abstract class State<T> : MonoBehaviour
{
    public abstract void EnterState(T type);
    public abstract void ExitState(T type);
    public abstract void UpdateState(T type);
}