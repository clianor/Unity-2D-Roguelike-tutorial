using UnityEngine;
using System.Collections;

public class QuitButton : MonoBehaviour {
    private GameObject btn;
    
	void Start ()
    {
        btn = GameObject.Find("QuitButton");
        btn.SetActive(false);
	}

    public void Quit()
    {
        Application.Quit();
    }
}
