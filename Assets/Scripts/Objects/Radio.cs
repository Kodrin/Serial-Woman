using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour, ISubscribe
{
    public List<AudioClip> tracks = new List<AudioClip>();
    private AudioSource bgm;
    public bool useCoroutineMethod = true;
    public bool firstPass = true;
    private int currentTrackNumber = 1;
    private int previousSmallArmPosition = 1;
    // Start is called before the first frame update
    public void StartRadio()
    {
        bgm = GetComponent<AudioSource>();
        bgm.loop = true;
        currentTrackNumber = 1;
        bgm.Play(0); //play with 0 delay
    }

    // Update is called once per frame
    /*private void OnMouseDown()
    {
        if (currentTrackNumber == 1)
        {
            if (useCoroutineMethod)
            {
                StartCoroutine(SwitchTrack(tracks[currentTrackNumber]));
            }
            else
            {
                Switch(tracks[currentTrackNumber]);
            }
            currentTrackNumber++;
        }
        else
        {
            if (useCoroutineMethod)
            {
                StartCoroutine(SwitchTrack(tracks[0]));
            }
            else
            {
                Switch(tracks[0]);
            }
            currentTrackNumber = 1;
        }
    }*/

    void Switch(AudioClip track)
    {
        bgm.loop = false;
        while (bgm.isPlaying) { }
        bgm.clip = track;
        bgm.Play();
        bgm.loop = true;
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
        EventHandler.OnAnyArmMove += PlayGeneral;
        EventHandler.OnSmallArmMove += PlayBlueMoon;
        EventHandler.OnBaronSolve += PlayBaron;
        EventHandler.OnPaintingSolve += PlayPainting ;
        EventHandler.OnCerealSolve += PlayCereal;

    }

    public void Unsubscribe()
    {
        EventHandler.OnIntroComplete -= StartRadio;
        EventHandler.OnAnyArmMove -= PlayGeneral;
        EventHandler.OnSmallArmMove -= PlayBlueMoon;
        EventHandler.OnBaronSolve -= PlayBaron;
        EventHandler.OnPaintingSolve -= PlayPainting;
        EventHandler.OnCerealSolve -= PlayCereal;
    }

    public void PlayGeneral()
    {
        if (firstPass)
        {
            Switch(tracks[7]);
            StartCoroutine(SwitchTrack(tracks[1]));
            firstPass = false;
        }
    }

    public void PlayBlueMoon(int smallArmPosition)
    {
        if (smallArmPosition == 11) 
        {
            Switch(tracks[7]);
            StartCoroutine(SwitchTrack(tracks[3]));
            StartCoroutine(SwitchTrack(tracks[4]));
        }
        else if (previousSmallArmPosition == 11)
        {
            Switch(tracks[7]);
            StartCoroutine(SwitchTrack(tracks[3]));
        }
        previousSmallArmPosition = smallArmPosition;
    }

    public void PlayBaron()
    {
        Switch(tracks[5]);
        StartCoroutine(SwitchTrack(tracks[6]));
        StartCoroutine(SwitchTrack(tracks[8]));
    }

    public void PlayPainting()
    {
        Switch(tracks[5]);
        StartCoroutine(SwitchTrack(tracks[7]));
    }

    public void PlayCereal()
    {
        Switch(tracks[5]);
        StartCoroutine(SwitchTrack(tracks[7]));
        StartCoroutine(SwitchTrack(tracks[0]));
    }
}
