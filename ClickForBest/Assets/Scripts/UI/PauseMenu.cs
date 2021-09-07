using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Store store_menu;

    public Button pause_button;
    public Button scoreboard_button;

    private DOMove doMove;

    private bool active;
    private DOMove pause_button_domove;

    private DORotate[] rotates;
    private DOScale[] scales;

    private void Start()
    {
        rotates = GetComponentsInChildren<DORotate>();
        scales = GetComponentsInChildren<DOScale>();
        pause_button_domove = pause_button.GetComponent<DOMove>();
        pause_button.onClick.AddListener(Pressed_Pause_Button);
        doMove = GetComponent<DOMove>();
    }
    public void Pressed_Pause_Button()
    {
        if (!active)
        {
            PreProcessBeforeOpen();

            active = true;
            doMove.DO();
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
            doMove.DORevert();
        }
    }
    public void Pressed_Store_Button()
    {
        if (store_menu)
        {
            active = false;
            doMove.DORevert();

            store_menu.OpenCloseMenu();
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
        pause_button_domove.DO();
        DORotates();
        DOScales();
    }
    private void PreProcessBeforeClose()
    {
        ReferenceKeeper.Instance.Handle.enabled = true;
        pause_button_domove.DORevert();
        DORotates(true);
        DOScales(true);
    }
    private void DORotates(bool _stop = false)
    {
        if (rotates != null)
        {
            for (int i = 0; i < rotates.Length; i++)
            {
                if(_stop)
                    rotates[i].ResetDO();
                else
                    rotates[i].DOLoop();
            }
        }
    }
    private void DOScales(bool _stop = false)
    {
        if (scales != null)
        {
            for (int i = 0; i < scales.Length; i++)
            {
                if (_stop)
                    scales[i].ResetDO();
                else
                    scales[i].DOLoop();
            }
        }
    }
}
