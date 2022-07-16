using UnityEngine;
using UnityEngine.Events;

public class Barrier : MonoBehaviour
{
    public int health;
    public string colliderTag;
    public event UnityAction onDestroy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(colliderTag))
        {
            Destroy(other.gameObject);
            health--;
            if (health <= 0)
                Destroy();
        }
    }

    private void Destroy()
    {
        if (gameObject == null)
            return;

        onDestroy?.Invoke();
        Destroy(gameObject);
    }
}
