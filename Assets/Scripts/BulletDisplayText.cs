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

    public void UpdateText(int magSize, int shotsRemaining, bool reloading = false)
    {
        string content;
        if (!reloading)
        {
            content = $"{_playerName} bullets: {shotsRemaining}/{magSize}";
        }
        else
        {
            content = $"{_playerName} reloading...";
        }
        _text.text = content;
    }

    public void ClearText()
    {
        _text.text = string.Empty;
    }
}
