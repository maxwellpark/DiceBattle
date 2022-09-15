using System;
using TMPro;
using UnityEngine;

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
        p1.onDamageTaken += h => SetText(h, _p1HealthText, _p1Prefix);
        p2.onDamageTaken += h => SetText(h, _p2HealthText, _p2Prefix);
        GameManager.onNewRound += () => SetText(p1.health, _p1HealthText, _p1Prefix);
        GameManager.onNewRound += () => SetText(p2.health, _p2HealthText, _p2Prefix);
        SetActive(true);
    }

    private void TearDown()
    {
        var (p1, p2) = GetRefs();
        p1.onDamageTaken -= h => SetText(h, _p1HealthText, "P1 health: ");
        p2.onDamageTaken -= h => SetText(h, _p2HealthText, "P2 health: ");
        GameManager.onNewRound -= () => SetText(p1.health, _p1HealthText, _p1Prefix);
        GameManager.onNewRound -= () => SetText(p2.health, _p2HealthText, _p2Prefix);
    }

    public void SetActive(bool active)
    {
        _p1HealthText.gameObject.SetActive(active);
        _p1HealthText.gameObject.SetActive(active);
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
