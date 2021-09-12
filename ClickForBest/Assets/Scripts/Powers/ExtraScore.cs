using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraScore : Power
{
    public int score_value;
    public TMPro.TMP_Text score_text;

    internal override void VirtualEnable()
    {
        base.VirtualEnable();

        score_text.text = "+" + score_value.ToString();
    }
    internal override void Use()
    {
        if (!used)
        {
            base.Use();

            ReferenceKeeper.Instance.GameManager.AddScore(score_value);

            TimeOver();
        }
    }
}
