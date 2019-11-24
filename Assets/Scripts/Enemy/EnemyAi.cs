using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyAi : MonoBehaviour
    {
//    public Transform target;
        public float speed = 3f;

        private Transform _targetPos;
        // Start is called before the first frame update
        void Start()
        {
            GameObject target = GameObject.FindGameObjectWithTag("Player");
            _targetPos = target.transform;
        }

        // Update is called once per frame
        void Update()
        {
            MoveToTarget();
        }

        private void MoveToTarget()
        {
            transform.LookAt(_targetPos.position);
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
