using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public List<AudioClip> tracks = new List<AudioClip>();
    private AudioSource bgm;
    public bool useCoroutineMethod = true;
    private int currentTrackNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        bgm = GetComponent<AudioSource>();
        bgm.loop = true;
        currentTrackNumber = 1;
        bgm.Play(0); //play with 0 delay
    }

    // Update is called once per frame
    private void OnMouseDown()
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
    }

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
}
