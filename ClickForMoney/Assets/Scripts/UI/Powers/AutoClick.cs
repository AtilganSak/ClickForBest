using System.Collections;
using UnityEngine;

public class AutoClick : Power
{
    [Range(1, 60)]
    public int minTime = 1;
    [Range(1, 60)]
    public int maxTime = 1;

    [SerializeField] float click_interval = 0.1F;
    [SerializeField] TMPro.TMP_Text time_text;

    int counter;

    WaitForSeconds WaitForSeconds;
    WaitForSeconds WaitForSeconds2;

    internal override void VirtualEnable()
    {
        base.VirtualEnable();

        WaitForSeconds = new WaitForSeconds(1);
        WaitForSeconds2 = new WaitForSeconds(click_interval);

        counter = Random.Range(minTime, maxTime);
        time_text.text = counter.ToString();
    }
    internal override void Use()
    {
        ReferenceKeeper.Instance.TimeCircle.StartTimer(counter);
        StartCoroutine(Timer());
        StartCoroutine(Process());
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
