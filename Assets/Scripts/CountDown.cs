using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    private float _timeLimit = 10f;

    private float _thirdOfLimit;
    private float _countDown;

    [SerializeField]
    private TMP_Text _countDownText;

    public static event UnityAction onCountDownEnd;

    private void Start()
    {
        _thirdOfLimit = _timeLimit / 3f;
        ResetCountDown();
        GameManager.onNewRound += ResetCountDown;
        _countDownText.gameObject.SetActive(false);
    }

    private void ResetCountDown()
    {
        _countDownText.color = Color.white;
        SetText(0f);
        _countDownText.gameObject.SetActive(true);
        _countDown = _timeLimit;
    }

    private void Update()
    {
        if (!GameManager.inBattle)
            return;

        _countDown -= Time.deltaTime;
        SetText(_countDown);
        _countDownText.color = GetTextColor();

        if (_countDown <= 0f)
        {
            ResetCountDown();
            onCountDownEnd?.Invoke();
        }
    }

    private void SetText(float timeRemaining)
    {
        _countDownText.text = "Time remaining: " + timeRemaining;
    }

    private Color GetTextColor()
    {
        if (_countDown > _timeLimit - _thirdOfLimit)
            return Color.green;

        if (_countDown > _thirdOfLimit)
            return Color.yellow;

        return Color.red;
    }
}
