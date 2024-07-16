using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompassBehaviour : MonoBehaviour {
    //public TMPro.TMP_Text m_Text;
    //public GameObject ARSession;
    //public GameObject AROrigin;
    //public ARAnchorManager aRAnchorManager;
    //public GameObject SimpleCamera;

    private bool m_isActive = false;
    private Coroutine _coroutine;

    // Start is called before the first frame update
    private void Start() {
        InitializeCompass();
    }

    // Update is called once per frame
    private void Update() {
        if (m_isActive) {
            float heading = -Input.compass.trueHeading;
            if (heading < 0) {
                heading += 360f;
            }
            transform.rotation = Quaternion.Euler(0, heading, 0);

            //if(NavigationController.instance) {
            //    m_Text.text = heading.ToString("00") + " " + transform.eulerAngles + " acrcy: " + Input.compass.headingAccuracy;
            //} else {
            //    m_Text.text = heading.ToString("00") + " " + transform.eulerAngles ;
            //}
            //m_Text.text = heading.ToString("00") + " " + CurrentCamera.rotation.eulerAngles + " " + Input.compass.trueHeading.ToString("00") + " " + transform.eulerAngles;
        }
    }

    public void InitializeCompass() {
        if (_coroutine != null) {
            StopCoroutine(_coroutine);
        }
        m_isActive = false;
        _coroutine = StartCoroutine(InitializeCompassCoroutine());

        IEnumerator InitializeCompassCoroutine() {
            //EnableAR(false);
            //show loading
            Input.compass.enabled = true;
            Input.location.Start();
            yield return new WaitForSeconds(.5f);
            m_isActive = Input.compass.enabled;
            yield return new WaitForSeconds(2f);
            SetAnchor();
            //hide loading
        }
    }

    public void SetAnchor() {
        m_isActive = Input.compass.enabled;
        Input.compass.enabled = false;
        Input.location.Stop();
    }

    private void EnableAR(bool value) {
        //AROrigin.SetActive(value);
        //ARSession.SetActive(value);
        //if(value)AROrigin.AddComponent<ARAnchorManager>();
        //aRAnchorManager.enabled = value;

        if (value) {
            SceneManager.LoadSceneAsync("ARScene", LoadSceneMode.Additive);
            //SimpleCamera?.SetActive(false);
        }
    }
}