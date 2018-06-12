using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneScript : MonoBehaviour {

    public AudioClip audio;
    
    void Start () {
        AudioSource.PlayClipAtPoint(audio, transform.position, 10);
    }
}
