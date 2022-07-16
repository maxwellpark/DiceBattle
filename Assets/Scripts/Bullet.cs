using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _maxDistance;
    [SerializeField]
    private Direction _direction;

    private Vector3 _startPos;
    private Vector3 _vDirection;

    private void Start()
    {
        _startPos = transform.position;
        _vDirection = transform.up;
        if (_direction == Direction.Right)
            _vDirection *= -1;
    }

    protected virtual void FixedUpdate()
    {
        transform.position += _speed * Time.deltaTime * _vDirection;

        if (Vector3.Distance(_startPos, transform.position) > _maxDistance)
            Destroy(gameObject);
    }
}
