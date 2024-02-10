using UnityEngine;

public class MainThreadExecutor : MonoBehaviour
{
    private void Update()
    {
        MainThreadDispatcher.Update();
    }
}
