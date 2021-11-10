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

    void Switch(AudioClip track, bool loopIt, bool cut)
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

    IEnumerator SwitchTrack(List<AudioClip> clips)
    {
        yield return new WaitForSeconds(bgm.clip.length - bgm.time);
        bgm.clip = clips[0];
        bgm.Play();
        for (int i = 1; i < clips.Count; i++)
        {
            yield return new WaitForSeconds(bgm.clip.length - bgm.time);
            bgm.clip = clips[i];
            bgm.Play();
        }
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
            StopAllCoroutines(); 
            if (smallArmPosition == 11)
            {
                Switch(tracks[7], false, true);
                var BlueMoonTracks = new List<AudioClip> { tracks[3], tracks[4] }; 
                StartCoroutine(SwitchTrack(BlueMoonTracks));
            }
            else if ((smallArmPosition >= 6) && (smallArmPosition != 12))
            {
                if ((previousSmallArmPosition == 11) || (previousSmallArmPosition < 6) || (previousSmallArmPosition == 12))
                {
                    Switch(tracks[7], false, true);
                    StartCoroutine(SwitchTrack(tracks[1]));
                }
            }
            else
            {
                if ((previousSmallArmPosition >= 6) && (previousSmallArmPosition != 12))
                {
                    Switch(tracks[7], false, true);
                    StartCoroutine(SwitchTrack(tracks[0]));
                }
            }
            previousSmallArmPosition = smallArmPosition;
        }
    }
    public void PlayBaron()
    {
        StopAllCoroutines();
        Switch(tracks[5], false, true);
        var BaronTracksA = new List<AudioClip> { tracks[6], tracks[8], tracks[8], tracks[1] };
        var BaronTracksB = new List<AudioClip> { tracks[6], tracks[8], tracks[8], tracks[0] };

        if ((previousSmallArmPosition >= 6) && (previousSmallArmPosition != 12))
        {
            StartCoroutine(SwitchTrack(BaronTracksA));
        }
        else
        {
            StartCoroutine(SwitchTrack(BaronTracksB));
        }
    }

    public void PlayPainting()
    {
        StopAllCoroutines();
        Switch(tracks[5], false, true);
        var PaintingTracks = new List<AudioClip> { tracks[7], tracks[9] };
        StartCoroutine(SwitchTrack(PaintingTracks));
        disableClockRadio = true;
    }

    public void PlayCereal()
    {
        StopAllCoroutines();
        Switch(tracks[5], false, true);
        var CerealTracks = new List<AudioClip> { tracks[7], tracks[2] };
        StartCoroutine(SwitchTrack(CerealTracks));
    }
}
