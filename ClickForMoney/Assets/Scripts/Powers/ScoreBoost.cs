using System.Collections;
using UnityEngine;

public class ScoreBoost : Power
{
    [SerializeField] TMPro.TMP_Text boost_text;

    public int[] boost_options;

    private int selected_option;

    internal override void VirtualEnable()
    {
        base.VirtualEnable();

        selected_option = boost_options[Random.Range(0, boost_options.Length)];
        boost_text.text = selected_option.ToString() + "x";
    }
    internal override void Use()
    {
        if (!used)
        {
            base.Use();

            ReferenceKeeper.Instance.GameManager.SetBoost(selected_option);
            ReferenceKeeper.Instance.TimeCircle.StartTimer(time);
            Invoke("DeactivePower", time);
        }
    }
    private void DeactivePower()
    {
        ReferenceKeeper.Instance.GameManager.SetBoost(1);
    }
}
