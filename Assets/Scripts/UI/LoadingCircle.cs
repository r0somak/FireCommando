using UnityEngine;

namespace UI
{
    public class LoadingCircle : MonoBehaviour
    {
        private RectTransform _rectComponent;
        private float rotateSpeed = 200f;

        private void Start()
        {
            _rectComponent = GetComponent<RectTransform>();
        }

        private void Update()
        {
            _rectComponent.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
        }
    }
}
