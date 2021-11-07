using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public abstract class Character : MonoBehaviour
{
    public enum Team
    {
        blueTeam,
        redTeam,
        greenTeam,
    }
    public Team _ekip;
    [SerializeField] public abstract void showOnAttackRange(GridManager manager);
    [SerializeField] public Tile associatedTile;
    [SerializeField] public SpriteRenderer _renderer;
    public int _x,_y;
    public List<Tile> currentPath;
    public List<Tile> moovingRange;
    public List<Tile> attackingRange;
    public string _name;
    public bool _asMooved,_asAttacked;
    [SerializeField]public  int _atqpts,_movementPoints,_currentlifePoints,_maxLifePoints,_speedPts;
    Character _closerTarger;
    public bool _team;//Vrai si équipe bleu faux sinon
    
    public static Character _selectedCharacter;//Le character séléctioné dans l'UI gamestate pour savoir quoi faire sur le click d'un perso si c'est une cible ou une séléction...

    public static Character CreateAndInitPosition(string path,Vector3 position)
    {
        return Instantiate(PrefabUtility.LoadPrefabContents(path).GetComponent<Character>(),position,UnityEngine.Quaternion.identity);
    }
    public Character InitName(string name,bool team)
    {
        _team=team;
        _name=name;
        return this;
    }
    public virtual void InitCharacteristics(int atq,int mouvement,int life,int vitesse)
    {
        _atqpts=atq;
        _movementPoints=mouvement;
        _asMooved=false;
        _asAttacked=false;
        _maxLifePoints=life;
        _currentlifePoints=_maxLifePoints/2;
        _speedPts=vitesse;
    }

    //permet de savoir si deux personnages ne sont pas dans la même équipe remplacer par un enum type?
    public bool InSameTeam(Character otherChar)
    {
    
        return otherChar._team==this._team?true:false;
    }
    

    void  OnMouseDown()
    {
        if(Character._selectedCharacter==this)
        {
            Character._selectedCharacter=null;//si on clique deux fois sur le même personnage, on annule la séléction
        }
        else
        {
            Character._selectedCharacter=this;
            //this._currentlifePoints-=5;
            Debug.Log(this + "name :" +this._name);
            CharacterUI.clickOnCharacter=true;
            GlobalScript.clickOnCharacter=true;
            //permet de dire aux autres script que l'on clique sur un personnage a remplacer par un static unique dans la classe character
        }
        Debug.Log("Click on old character");
       
    }
    public void Attack(Character target)
    {
        target._currentlifePoints-=this._atqpts;
    }
 
    



}
