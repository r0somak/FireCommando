using System;
using Database;
using Firebase.Auth;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameContollerScript : MonoBehaviour
    {
        private GameObject _player;
        private PlayerStats _stats;
        private DbHelper _db;
        // Start is called before the first frame update
        void Start()
        {
            _db = new DbHelper();
            _player = GameObject.FindGameObjectWithTag("Player");
            _stats = _player.GetComponent<PlayerStats>();
        }

        // Update is called once per frame
        void Update()
        {
            CheckIfPlayerLost();
        }

        void CheckIfPlayerLost()
        {
            if (_stats.healthPoints <= 0)
            {
                if (_stats.currentScore > Convert.ToInt32(_stats.highScore))
                { 
                    _db.WriteNewScore(FirebaseAuth.DefaultInstance.CurrentUser.UserId, FirebaseAuth.DefaultInstance.CurrentUser.Email, _stats.currentScore);    
                }
                SceneManager.LoadScene("Leaderboard");
            }
        }
    }
}
