using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {
    [SerializeField] private AudioSource MainAudio;
    [SerializeField] private AudioSource SingleSound;
    [SerializeField] private AudioClip ClickClip;
    [SerializeField] private Text SoundText; 
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ToggleMuteSounds() {
        MainAudio.mute = !MainAudio.mute;
        SoundText.text = !MainAudio.mute? "Sound: ON" : "Sound: Off";
    }

    public void PlayClickSound() {
        SingleSound.clip = ClickClip;
        SingleSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
