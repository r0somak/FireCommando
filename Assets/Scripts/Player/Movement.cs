using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        private Rigidbody rb;

        public float moveSpeed = 3f;
        private float smoothMovement = 3f;

        private Vector3 targetFoward;

        private bool canMove;

        private Vector3 dPos;
        private UnityEngine.Camera mainCam;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            targetFoward = transform.forward;
            mainCam = UnityEngine.Camera.main;
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
                canMove = true;
            }else if (Input.GetMouseButtonUp(0))
            {
                canMove = false;
            }
        }

        void UpdateForward()
        {
            transform.forward = Vector3.Slerp(
                transform.forward,
                targetFoward,
                Time.deltaTime * smoothMovement);
        }
    
        void MovePlayer()
        {
            if (canMove)
            {
                dPos = new Vector3(
                    Input.GetAxisRaw("Horizontal"), 
                    0f, 
                    Input.GetAxisRaw("Vertical"));
            
                dPos.Normalize();

                dPos *= moveSpeed * Time.fixedDeltaTime;
                dPos = Quaternion.Euler(
                           0f, 
                           mainCam.transform.eulerAngles.y, 
                           0f) * dPos;
                rb.MovePosition(rb.position + dPos);

                if (dPos != Vector3.zero)
                    targetFoward = Vector3.ProjectOnPlane(dPos, Vector3.up);
            }
        }
    }
}
