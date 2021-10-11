using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UISound : MonoBehaviour
{
    public AudioMixer audio_mixer;

    public AudioClip button_sound;
    public AudioClip slide_sound;
    public AudioClip select_sound;
    public AudioClip time_sound;
    public AudioClip switch_open_sound;
    public AudioClip switch_close_sound;
    public AudioClip rosette_won_sound;
    public AudioClip popup_sound;
    public AudioClip fail_sound;

    public Image sfx_image;
    public Sprite sfx_on;
    public Sprite sfx_off;

    private AudioSource source;

    private bool sfx;

    private AudioMixerSnapshot sfx_on_snapshot;
    private AudioMixerSnapshot sfx_off_snapshot;

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
        sfx = true;

        source = GetComponent<AudioSource>();

        sfx_on_snapshot = audio_mixer.FindSnapshot("SFX_ON");
        sfx_off_snapshot = audio_mixer.FindSnapshot("SFX_OFF");
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey(PlayerKeys.SFX))
        {
            sfx = PlayerPrefs.GetInt(PlayerKeys.SFX) == 0 ? false : true;
        }
        if (sfx)
            _sfxon();
        else
            _sfxoff();
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
    public IEnumerator PlaySoundMainThread(Sound _sound, bool _play = false)
    {
        if (source == null) yield return null;

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
        yield return null;
    }
    public void StopSound()
    {
        if (source)
        {
            if(source.isPlaying)
                source.Stop();
        }
    }
    public void SFXOnOff()
    {
        if (sfx)
        {
            _sfxoff();
        }
        else
        {
            _sfxon();
        }
        PlayerPrefs.SetInt(PlayerKeys.SFX, sfx == true ? 1 : 0);
    }
    private void _sfxon()
    {
        sfx = true;
        if (sfx_image)
        {
            if (sfx_on)
            {
                sfx_image.sprite = sfx_on;
            }
        }
        if (sfx_on_snapshot != null)
        {
            sfx_on_snapshot.TransitionTo(0);
        }
    }
    private void _sfxoff()
    {
        sfx = false;
        if (sfx_image)
        {
            if (sfx_off)
            {
                sfx_image.sprite = sfx_off;
            }
        }
        if (sfx_off_snapshot != null)
        {
            sfx_off_snapshot.TransitionTo(0);
        }
    }
}
