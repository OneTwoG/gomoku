using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Title : MonoBehaviour {

    public Button StartButton;
    public Button ExitButton;

    public GameObject board;

	// Use this for initialization
	void Start () {
        StartButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            board.SetActive(true);
        });

        ExitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
