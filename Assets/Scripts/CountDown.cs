using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CountDown : MonoBehaviour
{
    public float timeLimit = 10;
    private float _countDown;
    public TMP_Text countDownText;

    public static event UnityAction onCountDownEnd;

    private void Start()
    {
        ResetCountDown();
        GameManager.onNewRound += ResetCountDown;
    }

    private void ResetCountDown()
    {
        _countDown = timeLimit;
    }

    private void Update()
    {
        if (!GameManager.inBattle)
            return;

        _countDown -= Time.deltaTime;
        countDownText.text = "Time remaining: " + _countDown;

        if (_countDown <= 0f)
            onCountDownEnd?.Invoke();
    }
}
