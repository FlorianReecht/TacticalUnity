using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorChar : CharacterCharacteristics
{
    public WarriorChar()
    :base(5,3,20,5)
    {  
    }
    public override List<Tile> getOnAttackingRangeTile(GridManager grille,Tile tile)
    {
        return grille.getNeighbourList(tile);
    }
}
