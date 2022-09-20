using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    protected float _timeLimit = 10f;

    protected float _thirdOfLimit;
    protected float _countDown;

    [SerializeField]
    protected TMP_Text _countDownText;

    public event UnityAction onCountDownEnd;

    protected virtual void Start()
    {
        _thirdOfLimit = _timeLimit / 3f;
        ResetCountDown();
        _countDownText.gameObject.SetActive(false);
    }

    protected virtual void ResetCountDown()
    {
        _countDownText.color = Color.white;
        SetText(0f);
        _countDownText.gameObject.SetActive(true);
        _countDown = _timeLimit;
    }

    protected void ClearText()
    {
        _countDownText.text = string.Empty;
    }

    protected virtual void Update()
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

    protected virtual void SetText(float timeRemaining)
    {
        _countDownText.text = "Time remaining: " + timeRemaining;
    }

    protected virtual Color GetTextColor()
    {
        if (_countDown > _timeLimit - _thirdOfLimit)
            return Color.green;

        if (_countDown > _thirdOfLimit)
            return Color.yellow;

        return Color.red;
    }

    protected virtual void RegisterEvents()
    {
        GameManager.onNewRound += ResetCountDown;
        GameManager.onPreRound += ClearText;
    }

    protected virtual void UnRegisterEvents()
    {
        GameManager.onNewRound -= ResetCountDown;
        GameManager.onPreRound -= ClearText;
    }

    private void OnEnable()
    {
        RegisterEvents();
    }

    private void OnDisable()
    {
        UnRegisterEvents();
    }

    protected virtual void OnDestroy()
    {
        UnRegisterEvents();
    }
}
