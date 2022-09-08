using TMPro;
using UnityEngine;

public class BattleEndUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _battleEndText;

    private ScoreUI _scoreUI;

    void Start()
    {
        _scoreUI = FindObjectOfType<ScoreUI>();

        if (_scoreUI != null)
            _scoreUI.ResetUI();

        UpdateText(GameManager.playerRoundsWon >= 2);
    }

    private void UpdateText(bool player1Wins)
    {
        var text = $"{(player1Wins ? "Player 1 " : "Player 2 ")} wins!";
        _battleEndText.text = text;
    }
}
