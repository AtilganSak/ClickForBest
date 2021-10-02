using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{
    public AudioClip button_sound;
    public AudioClip slide_sound;
    public AudioClip select_sound;
    public AudioClip time_sound;
    public AudioClip switch_open_sound;
    public AudioClip switch_close_sound;
    public AudioClip rosette_won_sound;
    public AudioClip popup_sound;
    public AudioClip fail_sound;

    private AudioSource source;

    public enum Sound
    {
        Button,
        Slide,
        Select,
        Time,
        SwitchOpen,
        SwitchClose,
        RosetteWon,
        PopUp,
        Fail
    }

    private void OnEnable()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlaySound(Sound _sound, bool _play = false)
    {
        if (source == null) return;

        switch (_sound)
        {
            case Sound.Button:
                if (button_sound)
                {
                    if (_play)
                    {
                        source.clip = button_sound;
                        source.Play();
                    }
                    else
                    {
                        source.PlayOneShot(button_sound);
                    }
                }
                break;
            case Sound.Slide:
                if (slide_sound)
                {
                    if (_play)
                    {
                        source.clip = slide_sound;
                        source.Play();
                    }
                    else
                    {
                        source.PlayOneShot(slide_sound);
                    }
                }
                break;
            case Sound.Select:
                if (select_sound)
                {
                    if (_play)
                    {
                        source.clip = select_sound;
                        source.Play();
                    }
                    else
                    {
                        source.PlayOneShot(select_sound);
                    }
                }
                break;
            case Sound.Time:
                if (time_sound)
                {
                    if (_play)
                    {
                        source.clip = time_sound;
                        source.Play();
                    }
                    else
                    {
                        source.PlayOneShot(time_sound);
                    }
                }
                break;
            case Sound.SwitchOpen:
                if (switch_open_sound)
                {
                    if (_play)
                    {
                        source.clip = switch_open_sound;
                        source.Play();
                    }
                    else
                    {
                        source.PlayOneShot(switch_open_sound);
                    }
                }
                break;
            case Sound.SwitchClose:
                if (switch_close_sound)
                {
                    if (_play)
                    {
                        source.clip = switch_close_sound;
                        source.Play();
                    }
                    else
                    {
                        source.PlayOneShot(switch_close_sound);
                    }
                }
                break;
            case Sound.RosetteWon:
                if (rosette_won_sound)
                {
                    if (_play)
                    {
                        source.clip = rosette_won_sound;
                        source.Play();
                    }
                    else
                    {
                        source.PlayOneShot(rosette_won_sound);
                    }
                }
                break;
            case Sound.PopUp:
                if (popup_sound)
                {
                    if (_play)
                    {
                        source.clip = popup_sound;
                        source.Play();
                    }
                    else
                    {
                        source.PlayOneShot(popup_sound);
                    }
                }
                break;
            case Sound.Fail:
                if (fail_sound)
                {
                    if (_play)
                    {
                        source.clip = fail_sound;
                        source.Play();
                    }
                    else
                    {
                        source.PlayOneShot(fail_sound);
                    }
                }
                break;
        }
    }
    public void StopSound()
    {
        if (source)
        {
            if(source.isPlaying)
                source.Stop();
        }
    }
}
