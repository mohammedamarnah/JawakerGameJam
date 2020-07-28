using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    [SerializeField]
    Button play, sounds;
    [SerializeField]
    Text soundsText;

    void Start() {
        play.onClick.AddListener(Play);
        sounds.onClick.AddListener(ToggleSound);
    }

    void Play() {
        this.gameObject.SetActive(false);
    }

    void ToggleSound() {
        int audioLevel = 0;
        soundsText.text = $"sounds: {(audioLevel > 0 ? "on" : "off")}";
    }
}
