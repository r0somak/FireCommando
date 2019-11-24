using System;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyAi : MonoBehaviour
    {
//    public Transform target;
        public float speed = 3f;
        public int healthPoints = 5;

        private Transform _targetPos;
        private GameObject _target;

        private PlayerStats _stats;
        // Start is called before the first frame update
        void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player");
            _targetPos = _target.transform;
            _stats = _target.GetComponent<PlayerStats>();
        }

        // Update is called once per frame
        void Update()
        {
            MoveToTarget();
            CheckHealth();
        }

        private void MoveToTarget()
        {
            transform.LookAt(_targetPos.position);
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }

        private void CheckHealth()
        {
            if (healthPoints > 0) return;
            _stats.currentScore += 3;
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }

            if (other.gameObject.CompareTag("Bullet"))
            {
                healthPoints--;
            }
        }
    }
}
