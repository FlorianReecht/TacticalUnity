using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tile : MonoBehaviour
{
    public enum TileType
    {
        forest,
        wall,
        classic,
    }
    public TileType _tileType;
    
    public static bool clickOnTile=false;
    public static Tile _selectedTile;
    public Color baseColor;
    public Character _currentCharacter;
    public CharacterInWorld _currentCharacterInWorld;
    [SerializeField] public SpriteRenderer _renderer;
    public bool isaSpawnTile;


    public Color offsetColor;
    public Color wallColor= new Color(0.17f,0.65f,0.85f,1f);//A supprimer car toujours utilisée dans le pathFinding ceci est remplacé par le sprite du mur

    public float x,y;
    

    
    bool _offset;
    public bool isWalkable;
    public Color onHoverColor;
    //Ce qui concerne le A*
    public int gCost,fCost,hCost;
    public Tile previousTile;
    
    //set le sprite ici
    public void Init()
    {
        this._tileType=TileType.classic;
        this.SetSprite(this.GetSprite());//on ajuste le sprite de la tuile en fonction de son type
        //Le type est update lors de l'instanciation dans le grid manager
 
    }
    
    
    void OnMouseEnter()
    {
        _renderer.color=onHoverColor;
    }
    
    
    
    
    void OnMouseExit()
    {
        SetColor();
    }
    
    

    void  OnMouseDown()
    {
        Tile.clickOnTile=true;
        if(GlobalScript._gameState!=GlobalScript.GameState.attackPhase)
        {
            Tile._selectedTile=this;
            Debug.Log("[ "+Tile._selectedTile.x+" ;"+Tile._selectedTile.y+" ]");
            //si je clique sur une tuile vide je la transforme en mur ou en terrain cela dépend de son état
            if(CharacterInWorld._clickedCharacter==null)
            {    
                isWalkable=!isWalkable;
                this._tileType=isWalkable?TileType.classic:TileType.wall;
                this.SetSprite(this.GetSprite());
            }
        }
     
     
    
    }
    
    
  
    
    
    public void calculateFCost()
    {
        fCost=gCost+hCost;
    } 
    public Sprite GetSprite()
    {
        switch(_tileType)
        {
            default:
            case TileType.wall: return BanqueDeSprites.Instance.WallSprite;
            
            case TileType.forest: return BanqueDeSprites.Instance.ForestSprite;
            case TileType.classic:return BanqueDeSprites.Instance.TileSprite; 
        }
    }
    public void SetColor()
    {
        if(_tileType==TileType.classic)
        {
            //Les cases classiques sont transparantes 
            //La tile map est déssinée en dessous des cases classique
            _renderer.color= new Color(0,0,0,0);
        }
        else
        {
            _renderer.color=new Color(1,1,1,1);
        }

    }
    public void SetSprite(Sprite sprite)
    {
        this._renderer.sprite=sprite;
        this.SetColor();
    } 

}

