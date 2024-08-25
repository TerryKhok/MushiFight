using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewResult : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] MushiMesh _mushiMesh;

    [Header("Material")]
    [SerializeField] Material _p1Material;
    [SerializeField] Material _p2Material;

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

        GameObject targetMesh = _mushiMesh.ChangeMesh(GameVariables.GetPlayerMushiType(winNumber));
        var renderer = targetMesh.GetComponentInChildren<SkinnedMeshRenderer>();

        if (GameVariables.GetLosePlayer() == PlayerNumber.player_01)
        {
            renderer.material = _p2Material;
        }
        else
        {
            renderer.material = _p1Material;
        }
    }
}
