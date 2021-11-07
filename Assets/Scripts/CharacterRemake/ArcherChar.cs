using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherChar : CharacterCharacteristics
{
    // Start is called before the first frame update
    public ArcherChar()
    :base(3,4,10,10)
    {

    }
    public override List<Tile> getOnAttackingRangeTile(GridManager grille,Tile tile)
    {
        List<Tile> retour= new List<Tile>();
        if(grille.IsInBounds((int)tile.x+2,(int)tile.y))
        {
            retour.Add(grille.tileTab[(int)tile.x+2,(int)tile.y]);
        }
        if(grille.IsInBounds((int)tile.x-2,(int)tile.y))
        {
            retour.Add(grille.tileTab[(int)tile.x-2,(int)tile.y]);
        }
        if(grille.IsInBounds((int)tile.x,(int)tile.y-2))
        {
            retour.Add(grille.tileTab[(int)tile.x,(int)tile.y-2]);
        }
        if(grille.IsInBounds((int)tile.x,(int)tile.y+2))
        {
            retour.Add(grille.tileTab[(int)tile.x,(int)tile.y+2]);
        }
        //return grille.getNeighbourList(tile);
        return retour;
    }

}
