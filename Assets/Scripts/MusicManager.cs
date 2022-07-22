using System;
using UnityEngine;

public enum MusicState
{
    Battle, Menu, None
}

public class MusicManager : MonoBehaviour
{
    public AudioSource battleSrc;
    public AudioSource menuSrc;

    private void Awake()
    {
        menuSrc.enabled = true;
        battleSrc.enabled = true;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(MusicState state)
    {
        AudioSource src = default;

        switch (state)
        {
            case MusicState.Battle:
                src = battleSrc;
                break;
            case MusicState.Menu:
                src = menuSrc;
                break;
            case MusicState.None:
                ResetSrcs();
                break;
            default:
                throw new Exception("Invalid music state");
        }

        if (src.isPlaying)
            return;

        ResetSrcs();
        src.Play();
    }

    private void ResetSrcs()
    {
        battleSrc.Stop();
        menuSrc.Stop();
    }
}
