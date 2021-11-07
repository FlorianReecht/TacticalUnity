using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ce script permettra de gérer l'interface pour placer les Unitées
public class PrebattleScript : MonoBehaviour
{
    [SerializeField]Transform _playerFirstCharacter,_playerSecondCharacter,_playerThirdCharacter,_playerFourthCharacter; //Ce sont les positions pour placer les persos
    List<Transform> _transformList;
    // Start is called before the first frame update
    void Start()
    {
        GlobalScript._gameState=GlobalScript.GameState.preBattlePhase;
        _transformList=new List<Transform>();
        _transformList.Add(_playerFirstCharacter);
        _transformList.Add(_playerSecondCharacter);
        _transformList.Add(_playerThirdCharacter);
        _transformList.Add(_playerFourthCharacter);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
