using System.Collections;
using UnityEngine;

public class AutoClick : Power
{
    [SerializeField] float click_interval = 0.1F;

    int counter;

    WaitForSeconds WaitForSeconds;
    WaitForSeconds WaitForSeconds2;

    internal override void VirtualEnable()
    {
        base.VirtualEnable();

        WaitForSeconds = new WaitForSeconds(1);
        WaitForSeconds2 = new WaitForSeconds(click_interval);

        counter = time;
    }
    internal override void Use()
    {
        if (!used)
        {
            base.Use();

            ReferenceKeeper.Instance.TimeCircle.StartTimer(counter);
            StartCoroutine(Timer());
            StartCoroutine(Process());
        }
    }
    private IEnumerator Timer()
    {
        while (counter > 0)
        {
            counter--;

            yield return WaitForSeconds;
        }
        counter = 0;
    }
    private IEnumerator Process()
    {
        while (counter > 0)
        {
            ReferenceKeeper.Instance.ClickController.Pressed_Click_Button();

            yield return WaitForSeconds2;
        }
    }
}
