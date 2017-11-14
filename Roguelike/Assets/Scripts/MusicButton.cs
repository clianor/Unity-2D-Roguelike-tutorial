using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour {
    private Text onofftext;
    
    public void MusicOnOff()
    {
        onofftext = GameObject.Find("Text").GetComponent<Text>();
        if (onofftext.text == "Music On") {
            onofftext.text = "Music Off";
            SoundManager.instance.musicSource.Stop();
        }
        else if (onofftext.text == "Music Off")
        {
            onofftext.text = "Music On";
            SoundManager.instance.musicSource.Play();
        }
    }
}
