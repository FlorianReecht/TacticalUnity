using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  abstract class CharacterCharacteristics 
{
    public static int nbClass =4;
    public static List<string> CharName = new List<string>()
    {
            "Florian","Second","Third",
            "First",
            "Come",
            "explorer","Saroul"
    };
    public abstract List<Tile> getOnAttackingRangeTile(GridManager grille,Tile tile);
    public int _currentLifePoints,_maxLifePoints,_force,_vitesse,_movementPoints;
    public string _name;
    //public abstract List<Tile> showAttackRange(); Ici c'est chiant parce que j'ai pas la ref au CharacterInWorld je peux en faire un attribut
    public CharacterCharacteristics(int force,int mouvement,int life,int vitesse)
    {
        _currentLifePoints=life;
        _maxLifePoints=life;
        _vitesse=vitesse;
        _movementPoints=mouvement;
        _force=force;
        _name=CharacterCharacteristics.SetRandomName();

    }
    public static string SetRandomName()
    {
        int random=UnityEngine.Random.Range(0,CharacterCharacteristics.CharName.Count);
        return CharacterCharacteristics.CharName[random];
    }
  
    
}
