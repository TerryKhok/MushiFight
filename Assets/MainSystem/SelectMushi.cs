using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMenuTrigger
{
    void Excute(PlayerNumber _number);
}

public class SelectMushi : MonoBehaviour, IMenuTrigger
{
    [SerializeField] MushiType _type;
    [SerializeField] MushiMesh _p1MushiMesh;
    [SerializeField] MushiMesh _p2MushiMesh;

    public void Excute(PlayerNumber _number)
    {
        GameVariables.ChangeMushiType(_type, _number);

        switch (_number)
        {
            case PlayerNumber.player_01:
                _p1MushiMesh.ChangeMesh(_type);
                break;
            case PlayerNumber.player_02:
                _p2MushiMesh.ChangeMesh(_type);
                break;
        }
    }
}
