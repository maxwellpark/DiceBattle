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
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(MusicState state)
    {
        battleSrc.Stop();
        menuSrc.Stop();

        switch (state)
        {
            case MusicState.Battle:
                battleSrc.Play();
                break;
            case MusicState.Menu:
                menuSrc.Play();
                break;
            case MusicState.None:
                break;
            default:
                break;
        }
    }
}
