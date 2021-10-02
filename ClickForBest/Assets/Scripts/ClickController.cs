using UnityEngine;
using UnityEngine.UI;

public class ClickController : MonoBehaviour
{
    [SerializeField] float interval = 0.2F;
    [SerializeField] AudioClip click_sound;

    private CoinSpawner coin_spawner;
    private AudioSource audio_source;

    private bool holding;
    private float counter;

    void Start()
    {
        audio_source = GetComponent<AudioSource>();
        coin_spawner = FindObjectOfType<CoinSpawner>();
    }
    public void Pressed_Click_Button()
    {
        PlayClickSound();

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
        PlayClickSound();

        counter = interval;
        holding = true;
    }
    public void PointerUp()
    {
        holding = false;
        counter = 0;
    }
    private void PlayClickSound()
    {
        if (audio_source)
        {
            if (click_sound)
            {
                audio_source.PlayOneShot(click_sound);
            }
        }
    }
}
