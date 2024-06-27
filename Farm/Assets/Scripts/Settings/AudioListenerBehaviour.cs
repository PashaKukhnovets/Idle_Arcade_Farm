using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerBehaviour : MonoBehaviour
{
    public void SoundsOn() {
        AudioListener.volume = 1.0f;
    }

    public void SoundsOff() {
        AudioListener.volume = 0.0f;
    }
}
