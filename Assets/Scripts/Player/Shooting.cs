using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Shooting : MonoBehaviour
    {
        public Transform spawnPoint;
        public Rigidbody bulletPrefab;

        [FormerlySerializedAs("bullet_speed")] public float bulletSpeed;
    
        void Start()
        {
            bulletSpeed = 50f;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ShootBullet();
            }
        }

        private void ShootBullet()
        {
            var bullet = Instantiate(bulletPrefab, spawnPoint.position, bulletPrefab.rotation);
            bullet.velocity = spawnPoint.TransformDirection(Vector3.forward * bulletSpeed);
        }
    }
}
