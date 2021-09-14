using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class TextAnimation : MonoBehaviour
{

    public string text;
    public float interval = 0.1F;

    private TMP_Text text_object;
    private WaitForSeconds wait;
    private char[] chars;

    private void OnEnable()
    {
        wait = new WaitForSeconds(interval);
        text_object = GetComponent<TMP_Text>();
        if (text_object)
            text_object.text = "";
        if (text != "")
        {
            chars = text.ToCharArray();
            if (chars != null && chars.Length > 0)
            {
                StartCoroutine(Animation());
            }
        }
    }
    private IEnumerator Animation()
    {
        bool done = false;
        while (true)
        {
            if (done)
            {
                yield return new WaitForSeconds(0.5F);
                text_object.text = "";
            }
            for (int i = 0; i < chars.Length; i++)
            {
                text_object.text += chars[i];

                yield return wait;
            }
            done = true;
        }
    }
}
