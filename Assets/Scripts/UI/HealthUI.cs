using TMPro;
using UnityEngine;
using UnityEngine.Events;

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

        UnityAction<int> p1DmgAction = h => SetText(h, _p1HealthText, _p1Prefix);
        UnityAction<int> p2DmgAction = h => SetText(h, _p2HealthText, _p2Prefix);
        p1.onDamageTaken += p1DmgAction;
        p2.onDamageTaken += p2DmgAction;
        _dmgActions = (p1DmgAction, p2DmgAction);

        UnityAction p1RdAction = () => SetText(p1.health, _p1HealthText, _p1Prefix);
        UnityAction p2RdAction = () => SetText(p2.health, _p2HealthText, _p2Prefix);
        GameManager.onNewRound += p1RdAction;
        GameManager.onNewRound += p2RdAction;
        _rdActions = (p1RdAction, p2RdAction);

        SetActive(true);
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
    }

    public void SetActive(bool active)
    {
        _p1HealthText.gameObject.SetActive(active);
        _p2HealthText.gameObject.SetActive(active);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetActive(false);
    }

    private void SetText(int health, TMP_Text txt, string prefix)
    {
        txt.text = prefix + health;
    }

    private void OnDestroy()
    {
        TearDown();
    }
}
