using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour, ISubscribe
{
    public List<AudioClip> tracks = new List<AudioClip>();
    private AudioSource bgm;
    public bool firstPass = true;
    public int previousSmallArmPosition = 1;
    public bool disableClockRadio = false; 

    // Start is called before the first frame update
    private void Awake()
    {
        bgm = GetComponent<AudioSource>();
    }
    public void StartRadio()
    {
        bgm.loop = true;
        bgm.Play(0); //play with 0 delay
    }

    void Switch(AudioClip track, bool loopIt = true, bool cut = false)
    {
        bgm.loop = loopIt;
        if (cut || !bgm.isPlaying)
        {
            bgm.clip = track;
            bgm.Play();
        }
    }

    IEnumerator SwitchTrack(AudioClip track)
    {
        yield return new WaitForSeconds(bgm.clip.length - bgm.time);
        bgm.clip = track;
        bgm.Play();
        bgm.loop = true;
    }
    protected void OnEnable()
    {
        Subscribe();
    }

    protected void OnDisable()
    {
        Unsubscribe();
    }
    public void Subscribe()
    {
        EventHandler.OnIntroComplete += StartRadio;
        EventHandler.OnSmallArmMove += PlayBlueMoon;
        EventHandler.OnBaronSolve += PlayBaron;
        EventHandler.OnPaintingSolve += PlayPainting ;
        EventHandler.OnCerealSolve += PlayCereal;

    }

    public void Unsubscribe()
    {
        EventHandler.OnIntroComplete -= StartRadio;
        EventHandler.OnSmallArmMove -= PlayBlueMoon;
        EventHandler.OnBaronSolve -= PlayBaron;
        EventHandler.OnPaintingSolve -= PlayPainting;
        EventHandler.OnCerealSolve -= PlayCereal;
    }

    public void PlayBlueMoon(int smallArmPosition)
    {
        if (!disableClockRadio)
        {
            if (smallArmPosition == 11)
            {
                Switch(tracks[7], false, true);
                Switch(tracks[3], false);
                Switch(tracks[4]);
            }
            else if ((smallArmPosition >= 6) && (smallArmPosition != 12))
            {
                if ((previousSmallArmPosition == 11) || (previousSmallArmPosition < 6) || (previousSmallArmPosition == 12))
                {
                    Switch(tracks[7], false, true);
                    Switch(tracks[1]);
                }
            }
            else
            {
                if ((previousSmallArmPosition >= 6) && (previousSmallArmPosition != 12))
                {
                    Switch(tracks[7], false, true);
                    Switch(tracks[0]);
                }
            }
            previousSmallArmPosition = smallArmPosition;
        }
    }

    public void PlayBaron()
    {
        Switch(tracks[5], false, true);
        Switch(tracks[6], false);
        Switch(tracks[8], false);
        Switch(tracks[8], false);
        if ((previousSmallArmPosition >= 6) && (previousSmallArmPosition != 12))
        {
            Switch(tracks[1]);
        }
        else
        {
            Switch(tracks[0]);
        }
    }

    public void PlayPainting()
    {
        Switch(tracks[5], false, true);
        Switch(tracks[7], false);
        Switch(tracks[9]);
        disableClockRadio = true;
    }

    public void PlayCereal()
    {
        Switch(tracks[5], false, true);
        Switch(tracks[7], false);
        Switch(tracks[2]);
    }
}
