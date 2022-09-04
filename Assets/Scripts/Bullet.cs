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

    public GameObject _hitBarrier;
    public GameObject _hitEntity;
    public GameObject _hitFromPlayer;




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


        private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Enemy")
        {
            Debug.Log("Bullet Hit Enemy");
            Instantiate(_hitEntity, transform.position, Quaternion.identity);
            
        }

          if (other.tag=="Player")
        {
           
            Instantiate(_hitFromPlayer, transform.position, Quaternion.identity);
            
        }

         if (other.tag=="Barrier")
        {
            Debug.Log("Bullet Hit Barrier");
            Instantiate(_hitBarrier, transform.position, Quaternion.identity);
            
        }

        
        
    }
}
