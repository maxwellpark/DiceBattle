using System;
using UnityEngine;

public enum MusicState
{
    Battle, BattleEnd, Menu, None
}

public class MusicManager : Singleton<MusicManager>
{
    public AudioSource battleSrc;
    public AudioSource battleEndSrc;
    public AudioSource menuSrc;

    protected override void Awake()
    {
        base.Awake();
        menuSrc.enabled = true;
        battleSrc.enabled = true;
        battleEndSrc.enabled = true;
    }

    public void PlayMusic(MusicState state)
    {
        AudioSource src;

        switch (state)
        {
            case MusicState.Battle:
                src = battleSrc;
                break;
            case MusicState.BattleEnd:
                src = battleEndSrc;
                break;
            case MusicState.Menu:
                src = menuSrc;
                break;
            case MusicState.None:
                ResetSrcs();
                return;
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
