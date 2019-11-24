using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class Models : MonoBehaviour
{
    public class User
    {
        public string username;
        public string userEmail;
        public string userId;
        public int highScore;

        public User(string userId, string username, string userEmail)
        {
            this.userId = userId;
            this.username = username;
            this.userEmail = userEmail;
            highScore = 0;
        }
    }
    
    public class LeaderboardEntry
    {
        public string uid;
        public int score = 0;

        public LeaderboardEntry()
        {
        }

        public LeaderboardEntry(string uid, int score)
        {
            this.uid = uid;
            this.score = score;
        }

        public Dictionary<string, System.Object> ToDictionary()
        {
            Dictionary<string, System.Object> result = new Dictionary<string, System.Object>();
            result["userId"] = uid;
            result["highScore"] = score;
            return result;
        }
    }
}
