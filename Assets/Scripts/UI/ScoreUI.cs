using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _p1ScoreText;
    [SerializeField]
    private TMP_Text _p2ScoreText;
    [SerializeField]
    private TMP_Text _currentRoundText;

    private void Start()
    {
        ResetUI();
    }

    public void UpdateUI(int p1Score, int p2Score, int roundNum)
    {
        UpdateScoreTexts(p1Score, p2Score);
        UpdateRoundText(roundNum);
    }

    public void UpdateScoreTexts(int p1Score, int p2Score)
    {
        _p1ScoreText.text = "P1 rounds won: " + p1Score;
        _p2ScoreText.text = "P2 rounds won: " + p2Score;
    }

    public void UpdateRoundText(int roundNum)
    {
        _currentRoundText.text = "Round " + roundNum;
    }

    public void UpdateRoundText(string text)
    {
        _currentRoundText.text = text;
    }

    public void UpdateRoundTextForEndOfRound(bool wasDraw = false)
    {
        var text = wasDraw
            ? "Time limit reached. The round was a draw!"
            : "Player 1 wins round " + GameManager.currentRound + "!";

        Debug.Log(text);
        UpdateRoundText(text);
    }

    public void ResetUI()
    {
        _p1ScoreText.text = string.Empty;
        _p2ScoreText.text = string.Empty;
        _currentRoundText.text = string.Empty;
    }
}