using UnityEngine;

public class ClickController : MonoBehaviour
{
    RealButton click_button;
    CoinSpawner coin_spawner;

    void Start()
    {
        coin_spawner = FindObjectOfType<CoinSpawner>();
        click_button = FindObjectOfType<RealButton>();
        click_button.onClick.AddListener(Pressed_Click_Button);
    }

    private void Pressed_Click_Button()
    {
        coin_spawner.SpawnCoin();
    }
}
