using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIClick : MonoBehaviour
{
    private AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindAnyObjectByType<AudioManager>();
    }

    public void OnClick()
    {
        int randomValue = Random.Range(1, 3);
        string soundName = $"Uiselection{randomValue}";
        audioManager.PlayOneShot(soundName);
    }
}
