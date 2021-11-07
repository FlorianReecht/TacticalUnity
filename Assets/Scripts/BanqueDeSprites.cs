using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

//Ce script contiendra toute les références aux Sprites de Tiles Personnage

public class BanqueDeSprites : MonoBehaviour
{
    public static BanqueDeSprites Instance {get;private set;}
    private void Awake()
    {
        BanqueDeSprites.Instance=this;
    }
    //Character Sprites
    public Sprite WarriorSprite;
    public Sprite ArcherSprite;
    public Sprite WizardSprite;
    public Sprite SlimeSprite;
    //Tiles Sprites
    public Sprite ForestSprite;
    public Sprite WallSprite;
    public Sprite TileSprite;
    
   //Prefabs
    public CharacterInWorld _baseCharacter;//Unique reference au character de Base
    //UI Prefabs
    public NextLevelUIScript _nextLevelUI;
    public GiftedUIScript _giftedUI;
    
    //Reference au animations de chaque classe :Animation des Sprites
    public RuntimeAnimatorController _wizardIdleAnimation;
    public RuntimeAnimatorController _slimeAnimatorController;
    public RuntimeAnimatorController _warriorAnimatorController;
    
    //Reference aux animations de chaque classe : Animation des Images (UI)
    public RuntimeAnimatorController _SlimeUIAnimation;
    public RuntimeAnimatorController _WarriorUIAnimation;
    public RuntimeAnimatorController _ArcherUIAnimation;
    public RuntimeAnimatorController _WizardUIAnimation;

}
