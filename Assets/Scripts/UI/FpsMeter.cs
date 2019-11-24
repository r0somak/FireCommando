using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FpsMeter : MonoBehaviour
    {
        /* Public Variables */
        public float frequency = 0.5f;
 
        /* **********************************************************************
     * PROPERTIES
     * *********************************************************************/
        public int FramesPerSec { get; protected set; }
 
        /* **********************************************************************
     * EVENT HANDLERS
     * *********************************************************************/
        /*
     * EVENT: Start
     */
        private Text fps;
        private void Start()
        {
            fps = gameObject.GetComponent<Text>();
            StartCoroutine(Fps());
        }
 
        /*
     * EVENT: FPS
     */
        private IEnumerator Fps() {
            for(;;){
                // Capture frame-per-second
                int lastFrameCount = Time.frameCount;
                float lastTime = Time.realtimeSinceStartup;
                yield return new WaitForSeconds(frequency);
                float timeSpan = Time.realtimeSinceStartup - lastTime;
                int frameCount = Time.frameCount - lastFrameCount;
 
                // Display it
                FramesPerSec = Mathf.RoundToInt(frameCount / timeSpan);
                fps.text = FramesPerSec.ToString() + " fps";
            }
        }
    }
}
