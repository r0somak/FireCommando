using System.Text;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuUiManager : MonoBehaviour
    {
        private FirebaseUser _currentUsr;

        [Header("Main Menu Screen")] 
        public Text helloMessage;
        public Button logoutButton;
        public Button startGameButton;
        public Button optionsButton;
        
        [Header("Ok Panel")] 
        public RectTransform okPanel;

        [Header("Bad panel")] 
        public RectTransform badPanel;
        
        void Start()
        {
            UiManager.SetPanelVisibility(okPanel, false);
            UiManager.SetPanelVisibility(badPanel, false);
            _currentUsr = FirebaseAuth.DefaultInstance.CurrentUser;
            Debug.LogFormat("CURRENT USER DATA: EMAIL: {0}, DISPLAY_NAME: {1}, UUID: {2}", _currentUsr.Email, _currentUsr.DisplayName, _currentUsr.UserId);
            Button logoutBtn = logoutButton.GetComponent<Button>();
            logoutBtn.onClick.AddListener(LogoutUser);
            
            DisplayHelloMessage();
        }

        private void LogoutUser()
        {
            FirebaseAuth.DefaultInstance.SignOut();
            StartCoroutine(MySceneManager.LoadScene("StartScreen"));
        }
        
        
        private void DisplayHelloMessage()
        {
            Text messageText = helloMessage.GetComponent<Text>();
            StringBuilder message = new StringBuilder("Hello ");
            message.Append(_currentUsr.DisplayName == "" ? _currentUsr.Email : _currentUsr.DisplayName);
            Debug.Log("PLAYER NAME: "+message);
            messageText.text = message.ToString();
        }
    }
}
