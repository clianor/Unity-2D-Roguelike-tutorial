using UnityEngine;
using System.Collections;

public class InfoButton : MonoBehaviour {
    private GameObject btn;
    private GameObject image;

    void Start()
    {
        btn = GameObject.Find("InfoButton");
        btn.SetActive(false);

        image = GameObject.Find("InfoImage");
        image.SetActive(false);
    }

    public void onClick()
    {
        image.SetActive(true);
    }
}