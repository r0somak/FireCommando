﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;
using Object = System.Object;

namespace Database
{
    public class DbHelper
    {
        private DatabaseReference _reference;
        public DbHelper()
        {
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://fire-commando-f72d9.firebaseio.com/");
            _reference = FirebaseDatabase.DefaultInstance.RootReference;
        }

        
        public async void WriteNewScore(string userId, string email,  int score)
        {
            string key = _reference.Child("scores").Push().Key;
            Models.LeaderboardEntry entry = new Models.LeaderboardEntry(userId, email, score);
            Dictionary<string, Object> entryValues = entry.ToDictionary();

            Dictionary<string, Object> childUpdates = new Dictionary<string, Object>();
            childUpdates["/users/" + userId] = entryValues;

            await _reference.UpdateChildrenAsync(childUpdates);
        }

        public async Task<string> GetLeaderboard()
        {
            DataSnapshot data = await FirebaseDatabase.DefaultInstance.GetReference("users").OrderByChild("highScore").GetValueAsync();
            string scores = data.GetRawJsonValue();
            return scores;
        }
        
        public async Task<string> GetCurrentPlayerHighScore()
        {
            FirebaseUser usr = FirebaseAuth.DefaultInstance.CurrentUser;
            DataSnapshot data = await FirebaseDatabase.DefaultInstance.GetReference("users").GetValueAsync();
            string score = data.Child(usr.UserId).Child("highScore").Value.ToString();
            Debug.LogFormat("Current User: {0}, highScore: {1}", usr.UserId, score);
            return score;
        }
        
        private void SaveNewUser(FirebaseUser user)
        {
            Models.User trueUser = new Models.User(user.UserId, user.DisplayName, user.Email);
            string json = JsonUtility.ToJson(trueUser);
            Debug.Log("JSON: "+json);
            _reference.Child("users").Child(user.UserId).SetRawJsonValueAsync(json).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log(user.Email+" Save to database not working");
                }
                if(task.IsCompleted)
                {
                    Debug.Log(user.Email+" Saved to database");
                }
            });
        }
    
        public async Task<bool> AuthRegisterNewUser(string email, string password)
        {
            var auth = FirebaseAuth.DefaultInstance;
            try
            {
                FirebaseUser newUsr = await auth.CreateUserWithEmailAndPasswordAsync(email, password);
                Debug.LogFormat("Firebase user created successfully: email: {0} userId: ({1}), displayName: {2}",
                    newUsr.Email, newUsr.UserId, newUsr.DisplayName);
                SaveNewUser(newUsr);
                return true;
            }
            catch (Exception e)
            {
                if (e.InnerException != null && e.InnerException.ToString() ==
                    "Firebase.FirebaseException: The email address is already in use by another account.")
                {
                    Debug.Log("UWAGA UWAGA WYJATEK: " + e.InnerException);
                }
                return false;
            }
        }

        public async Task<bool> AuthLoginUser(string email, string pasword)
        {
            var auth = FirebaseAuth.DefaultInstance;
            try
            {
                FirebaseUser x = await auth.SignInWithEmailAndPasswordAsync(email, pasword);
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    x.DisplayName, x.UserId);
                return true;
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return false;
            }
        }
    }
}
