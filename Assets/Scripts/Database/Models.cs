using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Models : MonoBehaviour
{
    public class User
    {
        public string username;
        public string userEmail;
        public string userId;

        public User(string userId, string username, string userEmail)
        {
            this.userId = userId;
            this.username = username;
            this.userEmail = userEmail;
        }
    }
}
