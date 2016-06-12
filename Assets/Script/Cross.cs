using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cross : MonoBehaviour {

    //GridX 和GridY作为交叉点的坐标
    public int GridX;
    public int GridY;
    public MainLoop mainLoop;

	// Use this for initialization
	void Start () {
        //获取BUtton空间
        GetComponent<Button>().onClick.AddListener(() =>
        {
            mainLoop.OnClick(this);
        });	
	}
}
