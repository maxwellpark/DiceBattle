using TMPro;
using UnityEngine;

public class BulletDisplayText : MonoBehaviour
{
    [SerializeField]
    private string _playerName;

    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void UpdateText(int magSize, int shotsRemaining)
    {
        var content = $"{_playerName} bullets: {shotsRemaining}/{magSize}";
        _text.text = content;
    }
}
