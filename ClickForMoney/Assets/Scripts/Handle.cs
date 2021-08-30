using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Handle : MonoBehaviour
{
    public UnityEvent onActive;
    public UnityEvent onDeactive;

    public Transform pivot;

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    [SerializeField]
    private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float minDistanceForSwipe = 20f;

    private bool touched_head;
    private Camera camera;
    private Ray ray;
    private RaycastHit2D raycastHit;
    private DOMove doMove;

    private void OnEnable()
    {
        camera = Camera.main;

        doMove = GetComponent<DOMove>();

        Vector3 pos = camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        pos.y = transform.parent.position.y;
        pos.z = transform.parent.position.z;
        transform.parent.position = pos;

        doMove.DO();
    }
    private void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                ray = camera.ScreenPointToRay(Input.mousePosition);
                raycastHit = Physics2D.Raycast(ray.origin, Input.mousePosition, Mathf.Infinity, LayerMask.GetMask("Handle"));
                if (raycastHit)
                {
                    touched_head = true;
                    fingerUpPosition = Input.touches[0].position;
                    fingerDownPosition = Input.touches[0].position;
                }
            }
            if (touched_head)
            {
                if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    touched_head = false;
                    fingerUpPosition = Input.touches[0].position;
                    DetectSwipe();
                }
            }
        }
    }
    public void ResetUp()
    {
        touched_head = false;
        PivotUp();
    }
    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            if (fingerUpPosition.y > fingerDownPosition.y)//UP
            {
                onDeactive.Invoke();

                PivotUp();
            }
            else//Down
            {
                onActive.Invoke();

                PivotDown();
            }
        }
    }
    private void PivotUp()
    {
        pivot.localEulerAngles = new Vector3(0, 0, -45);
    }
    public void PivotDown()
    {
        pivot.localEulerAngles = new Vector3(0, 0, 45);
    }
    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > minDistanceForSwipe;
    }
    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }
}
