using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        private Rigidbody _rb;

        public float moveSpeed = 5f;
        private float smoothMovement = 3f;

        private Vector3 _targetFoward;

        private bool _canMove;

        private Vector3 _dPos;
        private UnityEngine.Camera _mainCam;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _targetFoward = transform.forward;
            _mainCam = UnityEngine.Camera.main;
        }
    

        // Update is called once per frame
        void Update()
        {
            GetInput();
        }

        void FixedUpdate()
        {
            UpdateForward();
            MovePlayer();
        }

        void GetInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _canMove = true;
            }else if (Input.GetMouseButtonUp(0))
            {
                _canMove = false;
            }
        }

        void UpdateForward()
        {
            transform.forward = Vector3.Slerp(
                transform.forward,
                _targetFoward,
                Time.deltaTime * smoothMovement);
        }
    
        void MovePlayer()
        {
            if (_canMove)
            {
                _dPos = new Vector3(
                    Input.GetAxisRaw("Horizontal"), 
                    0f, 
                    Input.GetAxisRaw("Vertical"));
            
                _dPos.Normalize();

                _dPos *= moveSpeed * Time.fixedDeltaTime;
                _dPos = Quaternion.Euler(
                           0f, 
                           _mainCam.transform.eulerAngles.y, 
                           0f) * _dPos;
                _rb.MovePosition(_rb.position + _dPos);

                if (_dPos != Vector3.zero)
                    _targetFoward = Vector3.ProjectOnPlane(_dPos, Vector3.up);
            }
        }
    }
}
