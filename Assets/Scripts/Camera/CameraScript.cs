using UnityEngine;
using UnityEngine.Serialization;

namespace Camera
{
    public class CameraScript : MonoBehaviour
    {
        public Transform target;
        [FormerlySerializedAs("target_Offset")] public Vector3 targetOffset;
        private void Start()
        {
            targetOffset = transform.position - target.position;
        }
        void Update()
        {
            if (target)
            {
                transform.position = Vector3.Lerp(transform.position, target.position+targetOffset, 0.1f);
            }
        }
    }
}
