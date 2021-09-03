using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreButton : MonoBehaviour
{
    public DOAnchorPos arrow_domove;
    public DOFade arrow_dofade;

    private bool showing;

    public void ShowFullScore()
    {
        if (showing)
        {
            showing = false;

            arrow_dofade.DORevert();
            arrow_domove.DORevert();
        }
        else
        {
            showing = true;

            arrow_dofade.DO();
            arrow_domove.DO();
        }
    }
}
