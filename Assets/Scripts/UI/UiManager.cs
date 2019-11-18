using System;
using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class UiManager : MonoBehaviour
{
    [Header("Main start panel")] 
    public Button loginButton;
    public Button registerButton;

    [Header("Register Panel")] 
    public RectTransform registerPanel;
    public InputField emailInputField;
    public InputField passwordInputField;
    public InputField passwordConfirmInputField;
    public Button sendData;

    [Header("Ok Panel")] 
    public RectTransform okPanel;
    public Button okPanelButton;

    [Header("Bad Panel")] 
    public RectTransform badPanel;
    public Text panelBadMessage;
    public Button badPanelButton;

    [Header("Login Panel")] 
    public RectTransform loginPanel;
    public InputField emailLoginInput;
    public InputField passwordLoginInput;
    public Button doLoginButton;
    
    // Start is called before the first frame update
    void Start()
    {
        SetPanelVisibility(registerPanel, false);
        SetPanelVisibility(okPanel, false);
        SetPanelVisibility(badPanel, false);
        SetPanelVisibility(loginPanel, false);
        
        Button logBtn = loginButton.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static void SetPanelVisibility(RectTransform panel, bool x)
    {
        panel.localScale = x ? new Vector3(1,1) : new Vector3(0, 0);
    }
}
