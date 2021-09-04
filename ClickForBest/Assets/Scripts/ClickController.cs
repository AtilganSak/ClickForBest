using UnityEngine;
using UnityEngine.UI;

public class ClickController : MonoBehaviour
{
    [SerializeField] float interval = 0.2F;
    CoinSpawner coin_spawner;

    private bool holding;
    private float counter;

    void Start()
    {
        coin_spawner = FindObjectOfType<CoinSpawner>();
    }
    public void Pressed_Click_Button()
    {
        coin_spawner.SpawnCoin();
    }
    private void Update()
    {
        if (holding)
        {
            counter += Time.deltaTime;
            if (counter >= interval)
            {
                counter = 0;
                coin_spawner.SpawnCoin();
            }
        }
    }
    public void PointerDown()
    {
        counter = interval;
        holding = true;
    }
    public void PointerUp()
    {
        holding = false;
        counter = 0;
    }
}
