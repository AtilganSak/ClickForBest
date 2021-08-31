using UnityEngine;

public class AdjustScreenCollider : MonoBehaviour
{
    public GameObject collider_prefab;
    public bool top = true;
    public bool bottom = true;
    public bool right = true;
    public bool left = true;

    void Start()
    {
        Vector3 screen_right = new Vector3(Screen.width, Screen.height / 2);
        Vector3 screen_left = new Vector3(0, Screen.height / 2);
        Vector3 screen_top = new Vector3(Screen.width / 2, Screen.height);
        Vector3 screen_bottom = new Vector3(Screen.width / 2, 0);

        Camera camera = Camera.main;

        Vector3 world_right = camera.ScreenToWorldPoint(screen_right);
        world_right.z = 0;
        Vector3 world_left = camera.ScreenToWorldPoint(screen_left);
        world_left.z = 0;
        Vector3 world_top = camera.ScreenToWorldPoint(screen_top);
        world_top.z = 0;
        Vector3 world_bottom = camera.ScreenToWorldPoint(screen_bottom);
        world_bottom.z = 0;

        if (right)
        {
            GameObject instance_right = Instantiate(collider_prefab,transform);
            instance_right.transform.position = world_right;
        }
        if (left)
        {
            GameObject instance_left = Instantiate(collider_prefab, transform);
            instance_left.transform.position = world_left;
        }
        if (top)
        {
            GameObject instance_top = Instantiate(collider_prefab, transform);
            instance_top.transform.position = world_top;
            instance_top.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        if (bottom)
        {
            GameObject instance_bottom = Instantiate(collider_prefab, transform);
            instance_bottom.transform.position = world_bottom;
            instance_bottom.transform.eulerAngles = new Vector3(0, 0, 90);
            ReferenceKeeper.Instance.BottomCollider = instance_bottom;
        }
    }
}
