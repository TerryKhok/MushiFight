using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewResult : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] MushiMesh _mushiMesh;

    // Start is called before the first frame update
    void Start()
    {
        PlayerNumber winNumber = PlayerNumber.player_01;

        switch(GameVariables.GetLosePlayer())
        {
            case PlayerNumber.player_01:
                _text.text = "Player2";
                winNumber = PlayerNumber.player_02;
                break;
            case PlayerNumber.player_02:
                _text.text = "Player1";
                break;
        }

        _mushiMesh.ChangeMesh(GameVariables.GetPlayerMushiType(winNumber));
    }
}
