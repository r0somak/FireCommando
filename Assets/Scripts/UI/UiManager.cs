using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Proyecto26;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;

public class UiManager : MonoBehaviour
{
    private StringBuilder _message = new StringBuilder("");
    private List<string> _messagesList = new List<string>
    {
        "Wrong email format\n",
        "Password is too short, must be at least 6 characters\n",
        "Passwords differ\n",
        "Email is already in use\n",
        "Bad credentials!\n",
    };
    private DbHelper _db;
    
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

    [Header("Ok login panel")] 
    public RectTransform okLoginPanel;
    public Text okLoginPanelText;
    public Button okLoginPanelButton;
    
    [Header("Login Panel")] 
    public RectTransform loginPanel;
    public InputField emailLoginInput;
    public InputField passwordLoginInput;
    public Button doLoginButton;

    
    // Start is called before the first frame update
    void Start()
    {
        _db = new DbHelper();
        
        HideAllPanels();
        
        Button registerBtn = registerButton.GetComponent<Button>();
        registerBtn.onClick.AddListener(delegate { SetPanelVisibility(registerPanel, true); });

        Button sendRegisterBtn = sendData.GetComponent<Button>();
        sendRegisterBtn.onClick.AddListener(CheckCredentialsRegisterUser);

        Button tryAgain = badPanelButton.GetComponent<Button>();
        tryAgain.onClick.AddListener(delegate { SetPanelVisibility(badPanel, false); });

        Button okPanelBackButton = okPanelButton.GetComponent<Button>();
        okPanelBackButton.onClick.AddListener(delegate
        {
            SetPanelVisibility(okPanel, false);
            SetPanelVisibility(registerPanel, false);
        });

        Button logButton = loginButton.GetComponent<Button>();
        logButton.onClick.AddListener(delegate { SetPanelVisibility(loginPanel, true); });

        Button loginAuthUserBtn = doLoginButton.GetComponent<Button>();
        loginAuthUserBtn.onClick.AddListener(LoginAuthUser);
    }

    private async void LoginAuthUser()
    {
        string loginEmailText = emailLoginInput.GetComponent<InputField>().text;
        string loginPasswordText = passwordLoginInput.GetComponent<InputField>().text;
        Text badMessage = panelBadMessage.GetComponent<Text>();
        Text okMessage = okLoginPanelText.GetComponent<Text>();
        
        
        _message.Clear();
        if (await _db.AuthLoginUser(loginEmailText, loginPasswordText))
        {
            okMessage.text = "Hello " + FirebaseAuth.DefaultInstance.CurrentUser.Email +
                             ", click ok to bring hell upon this cursed land!";
            SetPanelVisibility(okLoginPanel, true);
        }
        else
        {
            _message.Append(_messagesList[4]);
            badMessage.text = _message.ToString();
            SetPanelVisibility(badPanel, true);
        }
    }
    
//TODO: Dodanie paska postepu, AsyncOperation ?
    private async void CheckCredentialsRegisterUser()
    {
        string emailText = emailInputField.GetComponent<InputField>().text;
        string passwordText = passwordInputField.GetComponent<InputField>().text;
        string passwordConfirmText = passwordConfirmInputField.GetComponent<InputField>().text;
        Text badMessage = panelBadMessage.GetComponent<Text>();
        
        Regex emailPattern = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                       + "@"
                                       + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
        Match emailMatch = emailPattern.Match(emailText);
        
        _message.Clear();
        
        if (passwordText == passwordConfirmText && passwordText.Length >= 6 && emailMatch.Success)
        {
            if (await _db.AuthRegisterNewUser(emailText, passwordConfirmText))
            {
                SetPanelVisibility(okPanel, true);
            }
            else
            {
                _message.Append(_messagesList[3]);
                badMessage.text = _message.ToString();
                SetPanelVisibility(badPanel, true);
            }
        }
        else
        {
            Debug.Log("Credentials not ok");
            if (passwordText != passwordConfirmText)
            {
                _message.Append(_messagesList[2]);
                Debug.Log("Passwords are different");
            }
            if (passwordConfirmText.Length <= 5)
            {
                _message.Append(_messagesList[1]);
                Debug.Log("Password is too short");
            }
            if (!emailMatch.Success)
            {
                _message.Append(_messagesList[0]);
                Debug.Log("Wrong email format");
            }
            
            badMessage.text = _message.ToString();
            SetPanelVisibility(badPanel, true);
        }
    }

    private static void SetPanelVisibility(RectTransform panel, bool x)
    {
        panel.localScale = x ? new Vector3(1,1) : new Vector3(0, 0);
    }

    private void HideAllPanels()
    {
        SetPanelVisibility(registerPanel, false);
        SetPanelVisibility(okPanel, false);
        SetPanelVisibility(badPanel, false);
        SetPanelVisibility(loginPanel, false);
        SetPanelVisibility(okLoginPanel, false);
    }
}
