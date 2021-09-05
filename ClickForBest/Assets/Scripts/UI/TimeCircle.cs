using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCircle : MonoBehaviour
{
    public Gradient gradient;
    public TMPro.TMP_Text time_text;
    public DOScale stopwatch_do;

    private Image Image;

    private float counter;
    private float time;

    private void Start()
    {
        Image = GetComponent<Image>();
        Image.enabled = false;
        time_text.enabled = false;
        stopwatch_do.gameObject.SetActive(false);
    }
    public void StartTimer(float _time)
    {
        Image.enabled = true;
        time_text.enabled = true;
        stopwatch_do.gameObject.SetActive(true);
        stopwatch_do.DO();
        time = _time;
        counter = time;
        time_text.text = counter.ToString();
    }
    private void Update()
    {
        if (counter > 0)
        {
            counter -= Time.deltaTime;
            if (counter < 0)
            {
                time_text.enabled = false;
                Image.enabled = false;
                stopwatch_do.ResetDO();
                stopwatch_do.gameObject.SetActive(false);
                counter = 0;
                time = 0;
            }
            else
            {
                Image.fillAmount = counter / time;
                Image.color = gradient.Evaluate(counter / time);
            }
            time_text.text = ((int)counter).ToString();
        }
    }
}
