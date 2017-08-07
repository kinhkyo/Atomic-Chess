using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameCTL : MonoBehaviour {
    public static BaseGameCTL Current;
    private EGameState _GameState;
    public EGameState GameState {
        get { return _GameState; }
        set { _GameState = value; }

    }
    private void Awake()
    {
        Current = this;
        GameState = EGameState.PLAYING;
    }

}
