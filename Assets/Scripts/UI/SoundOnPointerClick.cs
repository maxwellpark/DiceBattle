using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class SoundOnPointerClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private AudioClip audioClip;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.Play();
    }
}
