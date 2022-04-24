using UnityEngine;

public class ClickButton : MonoBehaviour
{
    public TimeCircle timeCircle;

    public void PressedDown()
    {
        ReferenceKeeper.Instance.ClickController.PointerDown();
    }
    public void PressedUp()
    {
        ReferenceKeeper.Instance.ClickController.PointerUp();
    }
}
