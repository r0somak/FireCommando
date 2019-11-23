using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MySceneManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }
        
        public static IEnumerator LoadScene(string sceneName)
        {
            AsyncOperation asyncLoad =  SceneManager.LoadSceneAsync(sceneName);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
        
    }
}
