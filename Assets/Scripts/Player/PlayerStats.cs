using System;
using Database;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        public int healthPoints = 100;
        public string score;
        public int currentScore;

        public Text highScoreText;
        public Text scoreText;
        public Text hpText;
        
        private DbHelper _db;
        // Start is called before the first frame update
        private void Awake()
        {
            _db = new DbHelper();
            currentScore = 0;
        }

        void Start()
        {
            DisplayCurrentUserHighScore();
        }

        // Update is called once per frame
        void Update()
        {
            DisplayCurrentScore();
            DisplayCurrentHealth();
        }

        private async void DisplayCurrentUserHighScore()
        {
            score = await _db.GetCurrentPlayerHighScore();
            highScoreText.text += " "+score;
        }

        private void DisplayCurrentScore()
        {
            scoreText.text = "Score: "+currentScore;
        }

        private void DisplayCurrentHealth()
        {
            hpText.text = "Health: "+healthPoints;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                healthPoints -= 10;
            }
        }
    }
}
