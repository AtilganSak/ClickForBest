using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RanbowColor : MonoBehaviour
{
    public Gradient color;

    private Image image;
    private float time;

    private void OnEnable()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        time += Time.deltaTime * 0.2F;
        image.color = color.Evaluate(time);
        if (time >= 1)
            time = 0;
    }
}
