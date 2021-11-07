using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//path "Assets/Prefabs/Warrior.prefab"
public class Warrior : Character
{
    public static string link="Assets/Prefabs/Warrior.prefab";
    
    void Start()
    {
        this.InitCharacteristics();
    }
    public static Character CreateAndInitPosition(UnityEngine.Vector3 position)
    {
        return CreateAndInitPosition(Warrior.link,position);
    }
    //Caractéristiques propre au Warrior ici

    public static Warrior CreateWarrior(UnityEngine.Vector3 position,string name,bool team)
    {
        return (Warrior)CreateAndInitPosition(position).InitName(name,team);
    }

    public  void InitCharacteristics()
    {
        base.InitCharacteristics(5,3,20,5);
    }
 


    
    

    public override void showOnAttackRange(GridManager grille)
    {
        this.attackingRange= grille.getNeighbourList(this.associatedTile);//retourne les cases accessible par le guerrier pour attaquer
    }


 

}
