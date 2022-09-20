using UnityEngine;
using UnityEngine.Events;

public class PreRoundCountDown : CountDown
{
    public event UnityAction onPreCountDownEnd;

    protected override void Start()
    {
        base.Start();
    }

    protected override void ResetCountDown()
    {
        _countDownText.gameObject.SetActive(true);
        _countDownText.text = "3";
        _countDown = _timeLimit;
    }

    protected override void SetText(float timeRemaining)
    {
        if (timeRemaining > _timeLimit - _thirdOfLimit)
            _countDownText.text = "3";
        else if (timeRemaining > _thirdOfLimit)
            _countDownText.text = "2";
        else
            _countDownText.text = "1";
    }

    protected override void Update()
    {
        if (GameManager.inBattle
            || GameManager.waitingForDice
            || !_countDownText.gameObject.activeSelf)
            return;

        _countDown -= Time.deltaTime;
        SetText(_countDown);

        if (_countDown <= 0f)
        {
            _countDownText.gameObject.SetActive(false);
            onPreCountDownEnd?.Invoke();
        }
    }

    protected override void RegisterEvents()
    {
        GameManager.onPreRound += ResetCountDown;
    }

    protected override void UnRegisterEvents()
    {
        GameManager.onPreRound -= ResetCountDown;
    }
}
