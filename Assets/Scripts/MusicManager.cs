using System.Collections.Generic;
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
    private AudioSource _currentSrc;

    private Dictionary<MusicState, AudioSource> _srcMap;
    private Dictionary<string, MusicState> _sceneMap;

    protected override void Awake()
    {
        base.Awake();
        menuSrc.enabled = true;
        battleSrc.enabled = true;
        battleEndSrc.enabled = true;

        _srcMap = new Dictionary<MusicState, AudioSource>
        {
            { MusicState.Battle, battleSrc },
            { MusicState.BattleEnd, battleEndSrc },
            { MusicState.Menu, menuSrc },
            { MusicState.None, null }
        };

        _sceneMap = new Dictionary<string, MusicState>
        {
            { "BattleScene", MusicState.Battle },
            { "BattleEndScene", MusicState.BattleEnd },
            { "BattleMenuScene", MusicState.Menu },
            { "TitleScreenScene", MusicState.Menu },
            { "CharacterSelectScene", MusicState.Menu },
            { "CreditsScene", MusicState.Menu },
            { "TutorialScene", MusicState.Menu }
        };
        _currentSrc = null;
    }

    public void PlayMusic(MusicState state)
    {
        var src = GetSrcByState(state);

        if (src == null || src.isPlaying)
            return;

        ResetSrcs();
        src.Play();
        _currentSrc = src;
    }

    private void ResetSrcs()
    {
        battleSrc.Stop();
        battleEndSrc.Stop();
        menuSrc.Stop();
    }

    private AudioSource GetSrcByState(MusicState state)
    {
        if (_srcMap.ContainsKey(state))
            return _srcMap[state];

        Debug.LogWarning("No item exists in src map with state key: " + state);
        return null;
        //return state switch
        //{
        //    MusicState.Battle => battleSrc,
        //    MusicState.BattleEnd => battleEndSrc,
        //    MusicState.Menu => menuSrc,
        //    MusicState.None => null,
        //    _ => throw new Exception("Invalid music state"),
        //};
    }

    public MusicState GetStateBySrc(AudioSource src)
    {
        foreach (var item in _srcMap)
        {
            if (item.Value == src)
                return item.Key;
        }
        Debug.LogWarning("No key found in src map for audio src: " + src);
        return MusicState.None;

        //if (src == battleSrc)
        //    return MusicState.Battle;

        //if (src == battleEndSrc)
        //    return MusicState.BattleEnd;

        //if (src == menuSrc)
        //    return MusicState.Menu;

        //Debug.LogError("Audio src is either null or nothing is playing");
        //return MusicState.None;
    }

    public MusicState GetStateByScene(string sceneName)
    {
        if (_sceneMap.ContainsKey(sceneName))
            return _sceneMap[sceneName];

        Debug.LogWarning("No key found in scene map with key: " + sceneName);
        return MusicState.None;
    }

    public MusicState GetCurrentState()
    {
        var state = GetStateBySrc(_currentSrc);
        return state;
    }
}
