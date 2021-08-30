using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCircle : MonoBehaviour
{
    private Image Image;

    private float counter;
    private float time;

    private void Start()
    {
        Image = GetComponent<Image>();
        Image.enabled = false;
    }
    public void StartTimer(float _time)
    {
        Image.enabled = true;

        time = _time;
        counter = time;
    }
    private void Update()
    {
        if (counter > 0)
        {
            counter -= Time.deltaTime;
            if (counter < 0)
            {
                Image.enabled = false;
                counter = 0;
                time = 0;
            }
            else
            {
                Image.fillAmount = counter / time;
            }
        }
    }
}
