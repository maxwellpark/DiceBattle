using UnityEngine;
using UnityEngine.Events;

public class DestroyTimer : MonoBehaviour
{
    public float timeToDestroy = 40;
    public float tickTime = 0.1f;
    public static event UnityAction onDestroy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject == null)
            return;

        timeToDestroy -= tickTime;

        if (timeToDestroy <= 0f)
        {
            onDestroy?.Invoke();
            Destroy(gameObject);
        }
    }
}
