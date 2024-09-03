using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class ButtonOnPointerClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private AudioClip _audioClip;

    private AudioSource _audioSource;
    private Animator _animator;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClip;
        _animator = GetComponent<Animator>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // TODO: Get reference on scene change 
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        _audioSource.Play();
        _animator.SetTrigger("Press");
    }
}
