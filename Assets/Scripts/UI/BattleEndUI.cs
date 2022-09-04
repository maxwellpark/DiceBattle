using TMPro;
using UnityEngine;

public class BattleEndUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _battleEndText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.onBattleCompleteFlag += p1Wins => UpdateText(p1Wins);
        UpdateText(GameManager.playerRoundsWon >= 2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateText(bool player1Wins)
    {
        var text = $"{(player1Wins ? "Player 1 " : "Player 2 ")} wins!";
        _battleEndText.text = text;
    }
}
