using UnityEngine;

namespace Trash
{
    public class TrashZone : MonoBehaviour
    {
        [SerializeField] private float delayTime = 3f;
        
        bool _inZone;
        float _timer;
        private Ingredient _ingredient;
        private GameObject _obj;
        
        void Start()
        {
            _inZone = false;
            _timer = delayTime;
        }
    
        void OnTriggerEnter(Collider other)
        {
            _obj = other.gameObject;
            _ingredient = _obj.GetComponent<Ingredient>();
            if (other.gameObject.CompareTag("Ingredient"))
            {
                _inZone = true;
            }
        }
        
        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Ingredient"))
            {
                _inZone = false;
            }
        }
        
        private void Update()
        {
            if (_inZone)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    if (_ingredient.resetPoint != null)
                    {
                        _ingredient.ResetPosition();
                    }
                    _timer = delayTime;
                    _inZone = false;
                }
            }
            else
            {
                _timer = delayTime;
            }
        }
    }
}
