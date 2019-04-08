using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update

    public List<AudioClip> TrackList = new List<AudioClip>();
    public bool RandomizeTracklist;

    //private float _timeToAudioChange;
    private AudioSource _audioSource;
    private int _currentAudioIndex;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (TrackList.Count == 0)
            throw new InvalidOperationException("You must assign tracks to the Audio Source of the main camera");

        _audioSource.clip = TrackList[0];
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        HandleAudio();
    }

    private void HandleAudio()
    {
        //_timeToAudioChange -= Time.deltaTime;

        if (_audioSource.time >= _audioSource.clip.length || !_audioSource.isPlaying)
        {
            if(RandomizeTracklist)
                _currentAudioIndex = UnityEngine.Random.Range(0, TrackList.Count - 1);            // get random _currentAudioIndex value
            else 
                _currentAudioIndex = (_currentAudioIndex + 1) % TrackList.Count; // increase by 1, if _currentAudioIndex == tracklist.count, set it to 0

            // get clip from tracklist
            var clip = TrackList[_currentAudioIndex];

            Debug.Log(clip.name);

            // reset time to audio change
            //_timeToAudioChange = clip.length;

            // play new clip
            //_audioSource.Stop();
            _audioSource.clip = clip;
            _audioSource.Play();
                
        }
    }
}
