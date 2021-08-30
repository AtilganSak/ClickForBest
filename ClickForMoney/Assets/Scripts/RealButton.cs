using UnityEngine;
using UnityEngine.Events;

public class RealButton : MonoBehaviour
{
    public UnityEvent onClick;

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                onClick.Invoke();
            }
        }
    }
}
