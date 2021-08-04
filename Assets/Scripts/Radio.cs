using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public List<AudioClip> tracks = new List<AudioClip>();
    private AudioSource bgm;
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
            StartCoroutine(switchTrack(tracks[currentTrackNumber]));
            currentTrackNumber++;
        }
        else
        {
            StartCoroutine(switchTrack(tracks[0]));
            currentTrackNumber = 1;
        }
    }

    IEnumerator switchTrack(AudioClip track)
    {
        yield return new WaitForSeconds(bgm.clip.length - bgm.time);
        bgm.clip = track;
        bgm.Play();
        bgm.loop = true;
    }
}
