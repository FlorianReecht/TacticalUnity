using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Ce script correspond au comportement de l'attribut team d'un personnage un personnage sera dans une liste qui contiendra tous ses alliés
public class Team 
{
    public static List<Team> _everyTeams= new List<Team>();
    public List<CharacterInWorld> _team;
    public Color _teamColor;
    public string _teamName;
    public Color _transparantTeamColor;
    public Team(Color color,string name)
    {
        
        _teamColor=color;
        _teamName=name;
        _transparantTeamColor=new Color(color.r,color.g,color.b,0.5f);
        _team=new List<CharacterInWorld>();
        Team._everyTeams.Add(this);
    }
    public void AddInTeam(CharacterInWorld chara)
    {
        this._team.Add(chara);
    }
    public bool AllCharacterPlayed()
    {
        foreach(CharacterInWorld chara in _team)
        {
            if(chara.hasMooved==false)
            {
                return false;
            }
            
        }
        return true;
    }
    public static void GoToNextLevelTeam()
    {
        //on supprime toutes les équipes
        Team._everyTeams=new List<Team>(); 
    }
    public void rebootTeamCharacter()
    {
        foreach(CharacterInWorld chara in _team)
        {
            chara.hasAttacked=false;
            chara.hasMooved=false;
            chara.GetRenderer().color=new UnityEngine.Color(1,1,1,1);
        }
    }

}
