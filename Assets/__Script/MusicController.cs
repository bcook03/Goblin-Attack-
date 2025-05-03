using System.Collections;
using System.Collections.Generic;

using UnityEngine;



public class MusicController : MonoBehaviour
{
    public AudioSource a1;
    public AudioSource a2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        a1 = GameObject.Find("Kingdom Saved!").GetComponent<AudioSource>();
        a2 = GameObject.Find("Village Celebration").GetComponent<AudioSource>();
        a1.Play();
    }

    // Update is called once per frame
    void Update()
    {
        while (a1.isPlaying || a2.isPlaying) return;

        StartA2();
    }

    public void StartA2() {
        a2.Play();
        a1.Stop();
    }
}
