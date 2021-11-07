using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    //Script d'association entre les charactères et le terrain.
    public GridManager _grille;
    public Tile _tile;
    public const float TIME_TO_TRAVEL_BETWEEN_TWO_TILES=0.1f;

    public bool CanMoove(int x,int y)
    {
        if(x>=0&&y>=0&&x<_grille.width&&y<_grille.heigh)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    //retourne les cases sur lesquelles le personnage à le droit de se déplacer.
    public List<Tile> OnRangeMooving(CharacterInWorld moovingChar)
    {
        List<Tile> retour= new List<Tile>();
        List<Tile> _new =new List<Tile>();
        List<Tile> aTraiter= new List<Tile>();

        aTraiter.Add(moovingChar.associatedTile);
        for(int i=0;i<moovingChar._stats._movementPoints;i++)
        {
            foreach(Tile currentTile in aTraiter)
            {
                if(currentTile.isWalkable)
                {
                    _new.AddRange(_grille.getNeighbourList(currentTile));
                }
            }
            retour.AddRange(aTraiter);
            aTraiter.Clear();
            aTraiter.AddRange(_new);
        }
        retour.AddRange(aTraiter);
        moovingChar.moovingRange=retour;
        return retour;
    }
    //Bouge un personnage à une position aléatoire sur le terrain
    public void MooveAtRandomPosition(CharacterInWorld moovingChar)
    {
        int x=(int) UnityEngine.Random.Range(0,_grille.width);
        int y=(int) UnityEngine.Random.Range(0,_grille.heigh);
        MooveCharacter(x,y,moovingChar);
    }
    //Bouge un personnage à une case dont les coordonnées sont connues
    public  void MooveCharacter(int x,int y,CharacterInWorld MoovingChar)
    {
        /**
        if(CanMoove(x,y)&&(EstArrive(MoovingChar,x,y)==false))//si le personnage n'est pas sur la case et si la case est dans les limites
        {
             
            _grille.tileTab[MoovingChar._x,MoovingChar._y]._currentCharacterInWorld=null;//le personnage n'est plus sur la case
            MoovingChar._x=x;
            MoovingChar._y=y;
            MoovingChar.associatedTile=_grille.tileTab[x,y];
            UpdateCurrentTile(MoovingChar);
            MoovingChar.associatedTile.isWalkable=true;
            MoovingChar.associatedTile._tileType=Tile.TileType.classic;
            MoovingChar.associatedTile.Init();
            MoovingChar.hasMooved=true;
            MoovingChar.transform.position= new Vector3(x*_grille.cellSize,y*_grille.cellSize,MoovingChar.transform.position.z);

        }
        **/
        //StartCoroutine(MooveCharacterSmooth(MoovingChar,x,y));
        StartCoroutine(MooveCharacterSmoothTileByTile(MoovingChar,x,y));

    }
    public IEnumerator MooveCharacterSmooth(CharacterInWorld MoovingChar,int x,int y)
    {
        Vector3 startPosition=MoovingChar.transform.position;
        Vector3 directionn=getDirection(MoovingChar.associatedTile,_grille.tileTab[x,y],MoovingChar);
        float elapsedTime=0;
        Vector3 endPos= startPosition+directionn;
        

        while(elapsedTime<TIME_TO_TRAVEL_BETWEEN_TWO_TILES)
        {
            MoovingChar.transform.position=Vector3.Lerp(startPosition,endPos,(elapsedTime/TIME_TO_TRAVEL_BETWEEN_TWO_TILES));
            elapsedTime+=Time.deltaTime;
            Debug.Log("isMooving");
            yield return null;
        }   
        _grille.tileTab[MoovingChar._x,MoovingChar._y]._currentCharacterInWorld=null;//le personnage n'est plus sur la case
        MoovingChar._x=x;
        MoovingChar._y=y;
        MoovingChar.associatedTile=_grille.tileTab[x,y];
        UpdateCurrentTile(MoovingChar);
        MoovingChar.associatedTile.isWalkable=true;
        MoovingChar.associatedTile._tileType=Tile.TileType.classic;
        MoovingChar.associatedTile.Init();
        MoovingChar.hasMooved=true;
        MoovingChar.transform.position=endPos;
        Debug.Log("MovementEnd");
        MoovingChar.transform.position=MoovingChar.associatedTile.transform.position;
    }
    public IEnumerator MooveCharacterSmoothTileByTile(CharacterInWorld MoovingChar,int x,int y)
    {
        List<Tile> path=_grille.FindPath(MoovingChar._x,MoovingChar._y,x,y);
        foreach(Tile t in path )
        {
            StartCoroutine(MooveCharacterSmooth(MoovingChar,(int)t.x,(int)t.y));
            yield return new WaitForSeconds(0.1f);
        }
    }
   
    
    //Ajout d'une fonction qui fait avancer Tile par tile et pas tout d'un coup le déplacement suivant s'enclenche trop tôt
    public Vector3 getDirection(Tile startTile,Tile endTile,CharacterInWorld moovingChar)//Possibilité de faire le changement de sprite pour le personnage ici
    {
        Vector3 retour=Vector3.zero;
        if(startTile.x-endTile.x<0)
        {
            retour.x+=Vector3.right.x*Math.Abs(startTile.x-endTile.x);
            //Sprite right
            moovingChar.GetRenderer().flipX=false;

        }
        if(startTile.x-endTile.x>0)
        {
            retour.x+=Vector3.left.x*Math.Abs(startTile.x-endTile.x);
            moovingChar.GetRenderer().flipX=true;
        }  
        if(startTile.y-endTile.y<0)
        {
            retour.y+=Vector3.up.y*Math.Abs(startTile.y-endTile.y);
        }  
        if(startTile.y-endTile.y>0)
        {
            retour.y+=Vector3.down.y*Math.Abs(startTile.y-endTile.y);
        }
        return retour;
    }

    //Verifie si un personnage est arrivé à destination
    public bool EstArrive(CharacterInWorld moovingChar,int ex,int ey)
    {
        return moovingChar._x==ex&&moovingChar._y==ey?true:false;
    }
    //Update la relation entre la tuile et le personnage
    public void UpdateCurrentTile(CharacterInWorld _char)
    {
        _char.associatedTile=_grille.tileTab[_char._x,_char._y];
        _grille.tileTab[_char._x,_char._y]._currentCharacterInWorld=_char;
    }
    
    
    
}
