using UnityEngine;
using UnityEngine.Events;

public class RealButton : MonoBehaviour
{
    public UnityEvent onClick;

    public int id { get; private set; }
    
    private Camera camera;
    private RaycastHit hit;

    private void Awake()
    {
        id = Random.Range(0, 999999999);
        camera = Camera.main;
    }
    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                if (Physics.Raycast(camera.ScreenToWorldPoint(touch.position), Vector3.forward, out hit, 9999, LayerMask.GetMask("RealButton")))
                {
                    RealButton realButton = hit.transform.GetComponent<RealButton>();
                    if (realButton)
                    {
                        if (realButton.id == id)
                        {
                            onClick.Invoke();
                        }
                    }

                }
            }
        }
    }
}
