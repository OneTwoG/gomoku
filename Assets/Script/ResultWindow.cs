using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultWindow : MonoBehaviour {

    public Button ReplayButton;
    public Text Message;
    public MainLoop mainLoop;


	// Use this for initialization
	void Start () {
        ReplayButton.onClick.AddListener(() =>
        {
            //把界面隐藏
            gameObject.SetActive(false);
            //调用mainLoop的Restart方法
            mainLoop.Restart();
        });
	}
	
    public void Show(ChessType wintype)
    {
        switch(wintype)
        {
            case ChessType.Black:
                {
                    Message.text = string.Format("恭喜，你战胜了电脑");
                }
                break;
            case ChessType.White:
                {
                    Message.text = string.Format("傻逼，被电脑杀成狗");
                }
                break;
        }
    }
}
