using System;
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

    private (UnityAction<int>, UnityAction<int>) _dmgActions;
    private (UnityAction, UnityAction) _rdActions;

    private (PlayerShooting p1, PlayerShooting p2) GetRefs()
    {
        _p1 = GameObject.FindWithTag("Player");
        _p2 = GameObject.FindWithTag("Enemy");

        if (_p1 == null || !_p1.TryGetComponent<PlayerShooting>(out var p1Comp))
        {
            throw new Exception("Could not get player obj in health script");
        }

        if (_p2 == null || !_p2.TryGetComponent<PlayerShooting>(out var p2Comp))
        {
            throw new Exception("Could not get player obj in health script");
        }
        return (p1Comp, p2Comp);
    }

    public void Init()
    {
        var (p1, p2) = GetRefs();

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
        var (p1, p2) = GetRefs();
        p1.onDamageTaken -= _dmgActions.Item1;
        p2.onDamageTaken -= _dmgActions.Item2;
        GameManager.onNewRound -= _rdActions.Item1;
        GameManager.onNewRound -= _rdActions.Item2;
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
