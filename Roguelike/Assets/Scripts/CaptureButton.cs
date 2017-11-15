using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CaptureButton : MonoBehaviour {
    private Text capture;

    public void onClick()
    {
        Application.CaptureScreenshot("ScreenShot.png");
    }
}
