using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Character
{
    public static string link="Assets/Prefabs/Archer.prefab";
    public static Color archColor=Color.green;

    // Start is called before the first frame update
    void Start()//Called on Instanciate Instanciate est call dans CreateAndInitPosition
    {
        this.InitCharacteristics();
    }
    public void InitCharacteristics()
    {
        base.InitCharacteristics(3,4,10,10);
    }
    public static Character  CreateAndInitPosition(UnityEngine.Vector3 position)
    {
        return CreateAndInitPosition(link,position);
    }
    //Caractéristiques propre à l'archer ici

    public static Archer CreateArcher(UnityEngine.Vector3 position,string name,bool team)
    {
        return (Archer)CreateAndInitPosition(position).InitName(name,team);
    }
  
    public override void showOnAttackRange(GridManager grille)
    {
        //si il y a un personnage et qu'il n'est pas de la même équipe
        //On vérifie aussi que la case est bien présente sur le terrain (pour éviter les Out of bounds)

        this.attackingRange=grille.getNeighbourList(this.associatedTile);//retourne les cases accessible par l'archer pour attaquer
        if(_x+2<grille.width)
        {
            attackingRange.Add(grille.tileTab[this._x+2,this._y]);
        }
        

    }
}
