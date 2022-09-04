using UnityEngine;

public class EscapeTransition : MonoBehaviour
{
    [SerializeField]
    private MenuTransitionData _transData;

    private MenuTransitionManager _transManager;

    // Start is called before the first frame update
    void Start()
    {
        _transManager = FindObjectOfType<MenuTransitionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _transManager.Transition(_transData);
    }
}
