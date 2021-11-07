using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeChar : CharacterCharacteristics
{
    public SlimeChar()
    :base(1,1,2,1)
    {

    }
    public override List<Tile> getOnAttackingRangeTile(GridManager grille,Tile tile)
    {
        return grille.getNeighbourList(tile);
    }
}
