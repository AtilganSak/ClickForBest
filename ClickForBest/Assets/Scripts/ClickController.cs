using UnityEngine;
using UnityEngine.UI;

public class ClickController : MonoBehaviour
{
    [SerializeField] Button click_button;
    CoinSpawner coin_spawner;

    void Start()
    {
        coin_spawner = FindObjectOfType<CoinSpawner>();
        click_button.onClick.AddListener(Pressed_Click_Button);
    }

    public void Pressed_Click_Button()
    {
        coin_spawner.SpawnCoin();
    }
}
