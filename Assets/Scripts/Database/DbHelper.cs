using System;
using System.Collections;
using System.Collections.Generic;
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
    
    public void SaveNewUser(Models.User user)
    {
        string json = JsonUtility.ToJson(user);
        Debug.Log("JSON: "+json);
        reference.Child("users").Child(user.username).SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log(user.userEmail+" Save to database not working");
            }
            else if(task.IsCompleted)
            {
                Debug.Log(user.userEmail+" Saved to database");
            }
        });
    }

    public void CreateAuthUser(string email, string password)
    {
        var auth = FirebaseAuth.DefaultInstance;
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                if (CheckError(task.Exception, (int) Firebase.Auth.AuthError.EmailAlreadyInUse))
                {
                    // do whatever you want in this case
                    Debug.LogError("Email already in use");
                }
                return;
            }
            // Firebase user has been created.
            FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.Email, newUser.UserId);
        });
    }
    
    static bool CheckError(AggregateException exception, int firebaseExceptionCode)
    {
        Firebase.FirebaseException fbEx = null;
        foreach (Exception e in exception.Flatten().InnerExceptions)
        {
            fbEx = e as Firebase.FirebaseException;
            if (fbEx != null)
                break;
        }

        if (fbEx != null)
        {
            if (fbEx.ErrorCode == firebaseExceptionCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
