using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    [SerializeField]
    Button play, sounds;
    [SerializeField]
    Text soundsText;

    [SerializeField] private GameObject MatachMaking,Controller;

    void Start() {
        play.onClick.AddListener(Play);
        sounds.onClick.AddListener(ToggleSound);
    }

    void Play() {
        Controller.SetActive(false);
        MatachMaking.SetActive(true);
    }

    void ToggleSound() {
        FindObjectOfType<SoundManager>().ToggleMuteSounds();
    }
}
