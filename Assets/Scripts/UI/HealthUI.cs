using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _p1HealthText;
    [SerializeField]
    private TMP_Text _p2HealthText;

    private GameObject _p1;
    private GameObject _p2;

    [SerializeField]
    private string _p1Prefix = "P1 health: ";
    [SerializeField]
    private string _p2Prefix = "P2 health: ";

    [SerializeField]
    private Image _p1Image;
    [SerializeField]
    private Image _p2Image;
    [SerializeField]
    private Sprite _3HpSprite;
    [SerializeField]
    private Sprite _2HpSprite;
    [SerializeField]
    private Sprite _1HpSprite;
    [SerializeField]
    private Sprite _0HpSprite;

    private (UnityAction<int> p1, UnityAction<int> p2) _dmgActions;
    private (UnityAction p1, UnityAction p2) _rdActions;

    private (PlayerShooting p1, PlayerShooting p2) GetPlayerShootingComps()
    {
        _p1 = GameObject.FindWithTag("Player");
        _p2 = GameObject.FindWithTag("Enemy");

        (PlayerShooting p1, PlayerShooting p2) comps = default;

        if (_p1 == null || !_p1.TryGetComponent<PlayerShooting>(out var p1Comp))
            Debug.LogWarning("Could not get player obj in health script");
        else
            comps.p1 = p1Comp;

        if (_p2 == null || !_p2.TryGetComponent<PlayerShooting>(out var p2Comp))
            Debug.LogWarning("Could not get player obj in health script");
        else
            comps.p2 = p2Comp;

        return comps;
    }

    public void Init()
    {
        var (p1, p2) = GetPlayerShootingComps();

        //UnityAction<int> p1DmgAction = h => SetText(h, _p1HealthText, _p1Prefix);
        //UnityAction<int> p2DmgAction = h => SetText(h, _p2HealthText, _p2Prefix);
        void p1DmgAction(int h) => SetSprite(h, _p1Image);
        void p2DmgAction(int h) => SetSprite(h, _p2Image);
        p1.onDamageTaken += p1DmgAction;
        p2.onDamageTaken += p2DmgAction;
        _dmgActions = (p1DmgAction, p2DmgAction);

        //UnityAction p1RdAction = () => SetText(p1.health, _p1HealthText, _p1Prefix);
        //UnityAction p2RdAction = () => SetText(p2.health, _p2HealthText, _p2Prefix);
        void p1RdAction()
        {
            _p1Image.gameObject.SetActive(true);
            SetSprite(p1.health, _p1Image);
        }

        void p2RdAction()
        {
            _p2Image.gameObject.SetActive(true);
            SetSprite(p2.health, _p2Image);
        }

        GameManager.onNewRound += p1RdAction;
        GameManager.onNewRound += p2RdAction;
        GameManager.onPreRound += p1RdAction;
        GameManager.onPreRound += p2RdAction;
        _rdActions = (p1RdAction, p2RdAction);

        SetTextsActive(true);
    }

    private void TearDown()
    {
        var (p1, p2) = GetPlayerShootingComps();

        if (p1 != null)
            p1.onDamageTaken -= _dmgActions.p1;

        if (p2 != null)
            p2.onDamageTaken -= _dmgActions.p2;

        GameManager.onNewRound -= _rdActions.p1;
        GameManager.onNewRound -= _rdActions.p2;
        GameManager.onPreRound -= _rdActions.p1;
        GameManager.onPreRound += _rdActions.p2;
    }

    public void SetTextsActive(bool active)
    {
        _p1HealthText.gameObject.SetActive(active);
        _p2HealthText.gameObject.SetActive(active);
    }

    private void Start()
    {
        SetTextsActive(false);
        _p1Image.gameObject.SetActive(false);
        _p2Image.gameObject.SetActive(false);
    }

    private void SetText(int health, TMP_Text txt, string prefix)
    {
        txt.text = prefix + health;
    }

    private void SetSprite(int health, Image image)
    {
        if (health > 3 || health < 0)
        {
            Debug.LogError("Unexpected health: " + health);
            return;
        }

        switch (health)
        {
            case 3:
                image.sprite = _3HpSprite;
                break;
            case 2:
                image.sprite = _2HpSprite;
                break;
            case 1:
                image.sprite = _1HpSprite;
                break;
            case 0:
                image.sprite = _0HpSprite;
                break;
        }
    }

    private void OnDestroy()
    {
        TearDown();
    }
}
