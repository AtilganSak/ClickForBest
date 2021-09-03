using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickWave : MonoBehaviour
{
    public RectTransform wave_object;

    private Vector2 spawn_pos;
    private Transform canvas_transform;

    private void Start()
    {
        canvas_transform = FindObjectOfType<Canvas>().transform;
        spawn_pos = new Vector3(0, 2, 0);
    }
    public void Generate()
    {
        RectTransform tr = Instantiate(wave_object, canvas_transform);
        tr.anchoredPosition = spawn_pos;
    }
}
