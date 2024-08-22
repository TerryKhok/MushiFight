using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerNumber
{
    player_01,
    player_02
}

public enum MushiType
{
    Kokusan,
    Serebesu,
    Kanabun
}

public static class GameVariables
{
    static MushiType _p1MushiType = MushiType.Kokusan;
    static MushiType _p2MushiType = MushiType.Kokusan;

    static PlayerNumber _losePlayer = PlayerNumber.player_01;

    public static void ChangeMushiType(MushiType _type, PlayerNumber _number)
    {
        switch (_number)
        {
            case PlayerNumber.player_01:
                _p1MushiType = _type;
                break;

            case PlayerNumber.player_02:
                _p2MushiType = _type;
                break;
        }
    }

    public static MushiType GetPlayerMushiType(PlayerNumber _number)
    {
        MushiType type = MushiType.Kokusan;
        switch (_number)
        {
            case PlayerNumber.player_01:
                type = _p1MushiType;
                break;
            case PlayerNumber.player_02:
                type = _p2MushiType;
                break;
        }

        return type;
    }

    public static void SetLosePlayer(PlayerNumber _number)
    {
        _losePlayer = _number;
    }

    public static PlayerNumber GetLosePlayer()
    {
        return _losePlayer;
    }
}

