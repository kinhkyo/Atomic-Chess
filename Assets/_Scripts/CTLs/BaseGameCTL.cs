using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameCTL : MonoBehaviour {
    public static BaseGameCTL Current;
    private EGameState _GameState;
    private EPlayer _currentPlayer;

    public EPlayer CurrentPlayer { get; private set; }

    public EGameState GameState {
        get { return _GameState; }
        set { _GameState = value; }

    }
    private void Awake()
    {
        Current = this;
        CurrentPlayer = EPlayer.WHITE;
        GameState = EGameState.PLAYING;
    }

    public void SwichTurn(){
        
        CurrentPlayer = CurrentPlayer == EPlayer.WHITE ? EPlayer.BLACK : EPlayer.WHITE;
    }
}
