using UnityEngine;
using System.Collections;

public class MainLoop : MonoBehaviour {

    public GameObject WhitePrefab;
    public GameObject Blackprefab;
    public ResultWindow result;

    enum State
    {
        BlackGo,
        WhiteGo,
        Over,
    }

    State _state;

    Board _board;

    BoardModel _model;

    AI _ai;

    bool CanPlace(int gridX, int gridY)
    {

        return _model.Get(gridX, gridY) == ChessType.None;
    }

    bool PlaceChess(Cross cross, bool isblack)
    {

        if (cross == null)
            return false;

        var newChess = GameObject.Instantiate<GameObject>(isblack?Blackprefab:WhitePrefab);

        newChess.transform.SetParent(cross.gameObject.transform, false);

        var ctype = isblack ? ChessType.Black : ChessType.White;
        _model.Set(cross.GridX,cross.GridY,ctype);

        var linkCount = _model.CheckLink(cross.GridX, cross.GridY, ctype);

        return linkCount >= BoardModel.WinChessCount;
    }

    public void Restart()
    {
        _state = State.BlackGo;
        _model = new BoardModel();
        _ai = new AI();
        _board.Reset();
    }

    int _lastPlayerX, _lastPlayerY;

    public void OnClick(Cross cross)
    {
        if(_state != State.BlackGo)
        {
            return;
        }

        if(CanPlace(cross.GridX, cross.GridY))
        {
            _lastPlayerX = cross.GridX;
            _lastPlayerY = cross.GridY;

            if (PlaceChess(cross, true))
            {
                _state = State.Over;
                ShowResult(ChessType.Black);
            }
            else
            {
                _state = State.WhiteGo;
            }
        }
    }

    void ShowResult(ChessType winside)
    {
        result.gameObject.SetActive(true);
        result.Show(winside);
    }

	// Use this for initialization
	void Start () {
        _board = GetComponent<Board>();
        Restart();
	}
	
	// Update is called once per frame
	void Update () {
	    switch(_state)
        {
            case State.WhiteGo:
                {
                    int gridX, gridY;
                    _ai.ComputerDo(_lastPlayerX,_lastPlayerY,out gridX,out gridY);

                    if(PlaceChess(_board.GetCross(gridX,gridY), false))
                    {
                        _state = State.Over;
                        ShowResult(ChessType.White);
                    }
                    else
                    {
                        _state = State.BlackGo;
                    }
                }
                break;

        }
	}
}
