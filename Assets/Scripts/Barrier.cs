using UnityEngine;
using UnityEngine.Events;

public class Barrier : MonoBehaviour
{
    public int health;
    public string colliderTag;
    public Cell cell;
    public event UnityAction onDamageTaken;
    public event UnityAction onDestroy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(colliderTag))
        {
            Destroy(other.gameObject);
            health--;
            onDamageTaken?.Invoke();
            if (health <= 0)
                Destroy();
        }
    }

    private void Destroy()
    {
        if (gameObject == null)
            return;

        cell.hasBarrier = false;
        onDestroy?.Invoke();
        Destroy(gameObject);
    }
}
