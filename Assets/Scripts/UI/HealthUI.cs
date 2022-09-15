using System;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private TMP_Text _p1HealthText;
    private TMP_Text _p2HealthText;

    private GameObject _p1;
    private GameObject _p2;

    private void Init()
    {
        var p1 = GameObject.FindWithTag("Player");
        var p2 = GameObject.FindWithTag("Enemy");

        if (p1 == null || !p1.TryGetComponent<PlayerShooting>(out var p1Comp))
        {
            throw new Exception("Could not get player obj in health script");
        }

        if (p2 == null || !p2.TryGetComponent<PlayerShooting>(out var p2Comp))
        {
            throw new Exception("Could not get player obj in health script");
        }

        p1Comp.onDamageTaken += h => SetText(h, _p1HealthText, "P1 health: ");
        p2Comp.onDamageTaken += h => SetText(h, _p2HealthText, "P2 health: ");
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void SetText(int health, TMP_Text txt, string prefix)
    {
        txt.text = prefix + health;
    }
}
