using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public CoinStore store_menu;
    public Scoreboard scoreboard;

    public Button pause_button;
    public Button scoreboard_button;

    public Image google_connect_image;

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

        if (ReferenceKeeper.Instance.GooglePlayServices != null && ReferenceKeeper.Instance.GooglePlayServices.LoginState)
            google_connect_image.color = Color.green;
        else
            google_connect_image.color = Color.red;

        if(ReferenceKeeper.Instance.GooglePlayServices != null)
            ReferenceKeeper.Instance.GooglePlayServices.onInternetChanged += ChangedInternetState;
    }

    public void Pressed_Pause_Button()
    {
        if (!active)
        {
            PreProcessBeforeOpen();

            ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.Button);
            ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.Slide);

            active = true;
            doMove.DO();
        }
    }
    public void Pressed_ScoreBoard_Button()
    {
        ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.Button);
        if (scoreboard)
        {
            active = false;
            doMove.DORevert();

            scoreboard.OpenCloseMenu();
        }
    }
    public void Pressed_Resume_Button()
    {
        if (active)
        {
            PreProcessBeforeClose();

            ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.Button);
            ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.Slide);

            active = false;
            doMove.DORevert();
        }
    }
    public void Pressed_Store_Button()
    {
        ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.Button);
        if (store_menu)
        {
            active = false;
            doMove.DORevert();


            store_menu.OpenCloseMenu();
        }
    }
    public void Pressed_SFX_Button()
    {
        ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.Button);
        ReferenceKeeper.Instance.UISound.SFXOnOff();
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
        ReferenceKeeper.Instance.GameManager.ReportScore();
        ReferenceKeeper.Instance.Handle.enabled = false;
        Screen.sleepTimeout = PlayerPrefs.GetInt(PlayerKeys.SCREEN_SLEEP_TIME);
        pause_button_domove.DO();
        DORotates();
        DOScales();
    }
    private void PreProcessBeforeClose()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        ReferenceKeeper.Instance.Handle.enabled = true;
        pause_button_domove.DORevert();
        DORotates(true);
        DOScales(true);
    }
    private void ChangedInternetState(bool _state)
    {
        if (_state)
        {
            google_connect_image.color = Color.green;
        }
        else
        {
            google_connect_image.color = Color.red;
        }
    }

    private void DORotates(bool _stop = false)
    {
        if (rotates != null)
        {
            for (int i = 0; i < rotates.Length; i++)
            {
                if (_stop)
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
