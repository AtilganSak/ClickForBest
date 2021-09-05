using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button pause_button;
    public Button scoreboard_button;

    private DOMove DOAnchorPos;

    private bool active;

    private void Start()
    {
        pause_button.onClick.AddListener(Pressed_Pause_Button);
        DOAnchorPos = GetComponent<DOMove>();
    }
    private void Pressed_Pause_Button()
    {
        if (!active)
        {
            PreProcessBeforeOpen();

            active = true;
            DOAnchorPos.DO();
        }
    }
    public void Pressed_ScoreBoard_Button()
    {
        ReferenceKeeper.Instance.GooglePlayServices.ShowLeaderboard();
    }
    public void Pressed_Resume_Button()
    {
        if (active)
        {
            PreProcessBeforeClose();

            active = false;
            DOAnchorPos.DORevert();
        }
    }
    private void PreProcessBeforeOpen()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            scoreboard_button.interactable = false;
        }
        else
        {
            scoreboard_button.interactable = true;
        }
        ReferenceKeeper.Instance.Handle.enabled = false;
    }
    private void PreProcessBeforeClose()
    {
        ReferenceKeeper.Instance.Handle.enabled = true;
    }
}
