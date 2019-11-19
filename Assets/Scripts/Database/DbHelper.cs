using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;

public class DbHelper
{
    private DatabaseReference reference;
    public DbHelper()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://fire-commando-f72d9.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    
    public void SaveNewUser(FirebaseUser user)
    {
        Models.User trueUser = new Models.User(user.UserId, user.DisplayName, user.Email);
        string json = JsonUtility.ToJson(trueUser);
        Debug.Log("JSON: "+json);
        reference.Child("users").Child(user.UserId).SetRawJsonValueAsync(json).ContinueWith(task =>
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
}
