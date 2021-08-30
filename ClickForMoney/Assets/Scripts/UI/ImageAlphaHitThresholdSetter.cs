using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAlphaHitThresholdSetter : MonoBehaviour
{
    [Range(0, 1)]
    public float threshold = 0.5F;

    Image image;

    private void OnEnable()
    {
        image = GetComponent<Image>();
    }
    void Start()
    {
        image.alphaHitTestMinimumThreshold = threshold;
    }
}
