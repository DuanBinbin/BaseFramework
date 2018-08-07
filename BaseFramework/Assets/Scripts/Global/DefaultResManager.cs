using UnityEngine;
using System.Collections;

public class DefaultResManager : MonoSingleton<DefaultResManager> {
    
    public Texture2D _DefaultFileIcon;
    public AudioClip _CaptureSound;


    AudioSource _AudioSrc;

    private void Start()
    {
        _AudioSrc = gameObject.GetComponent<AudioSource>();
    }

    public void PlayCaptureSound()
    {
        if(_AudioSrc)
        {
            _AudioSrc.PlayOneShot(_CaptureSound);
        }
    }
}
