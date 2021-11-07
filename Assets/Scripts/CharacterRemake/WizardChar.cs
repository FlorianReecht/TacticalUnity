using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardChar : CharacterCharacteristics
{
    public WizardChar()
    :base(5,3,15,7)
    {  
    }
    public override List<Tile> getOnAttackingRangeTile(GridManager grille,Tile tile)
    {
        return grille.getNeighbourList(tile);
    }
    
}
