using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioClip[] music;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playAudioSequentially());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator playAudioSequentially()
    {
        yield return null;

        //1.Loop through each AudioClip
        for (int i = 0; i < music.Length; i++)
        {
            //2.Assign current AudioClip to audiosource
            source.clip = music[i];

            //3.Play Audio
            source.Play();

            //4.Wait for it to finish playing
            while (source.isPlaying)
            {
                yield return null;
            }

            //5. Go back to #2 and play the next audio in the adClips array
        }
        StartCoroutine(playAudioSequentially());
    }
}
