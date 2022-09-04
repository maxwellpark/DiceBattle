using UnityEngine;

public class DiceManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _dicePrefab;

    [SerializeField]
    private GameObject _diceContainer;

    [SerializeField]
    private float _zOffset;

    [SerializeField]
    private Vector3 _diceScale;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(_diceContainer);
    }

    public void CreateDice(int p1Roll, int p2Roll)
    {
        var dice1 = Instantiate(_dicePrefab, _diceContainer.transform);
        var dice2 = Instantiate(_dicePrefab, _diceContainer.transform);

        dice2.transform.position = new Vector3(
            dice2.transform.position.x, dice2.transform.position.y, dice2.transform.position.z + _zOffset);

        dice1.transform.localScale = _diceScale;
        dice2.transform.localScale = _diceScale;

        var diceAnim1 = dice1.GetComponent<DiceAnimController>();
        var diceAnim2 = dice2.GetComponent<DiceAnimController>();
        diceAnim1.diceValue = p1Roll;
        diceAnim2.diceValue = p2Roll;
    }
}
