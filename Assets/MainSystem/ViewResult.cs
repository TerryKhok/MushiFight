using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewResult : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;

    // Start is called before the first frame update
    void Start()
    {
        switch(GameVariables.GetLosePlayer())
        {
            case PlayerNumber.player_01:
                _text.text = "Player2 Win!!";
                break;
            case PlayerNumber.player_02:
                _text.text = "Player1 Win!!";
                break;

        }
    }
}
