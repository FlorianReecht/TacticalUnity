using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//SCRIPT DU PATHFINDING ET DE GESTION DU TERRAIN
//Ajouter plein de Tiles comme les personnages
//Ce script permet aux personnage d'évoluer dans un terrain quadrillé
public class GridManager : MonoBehaviour
{
    //Concerne l'UI
    public Text texte;//nombre de resets
    public int nbResets=0;
    public Text mousePosition;
    [SerializeField] private Camera mainCamera;
    //Dimensions de la grille
    public  int width;
    public int heigh;
    public float cellSize;
    //concerne le joueur
    public PlayerMovement _player;
    public Tile _tile;
    public Transform cam;//reference à la position de la camera
    public Tile[,] tileTab;//tableau qui contient les Tuiles
    float currentTime=0;//sert d'horloge pour l'instant

    
    //constantes PathFinding
    public const int MOVE_STRAIGHT_COST =10;

    void  Awake()
    {
        GenerateGrid();
        //murAleatoires(0.9f);//0-> que des murs 1-> 0 murs
        /**
        List<Tile> chemin=FindPath(_player.x,_player.y,9,9);
        DrawPath(chemin,new Color(1,1,1,1));
        **/
    }
    void Start()
    {
        Debug.Log("[" +width +";" + heigh+ "]");
    }
    
    void Update()
    {
        /**
        //deplacement du player
        if(!EstArrive(_player.x,_player.y,9,9))
        {
            List<Tile> path=FindPath(_player.x,_player.y,9,9);
            if(currentTime>=1)
            {
                currentTime=0;
                if(CanMooveTo(_player.x,_player.y,9,9))
                {
                    MooveToNextPosition(path);
                }
 

            }
        }
        **/
        
        //Update la position de la souris par rapport au terrain
        UnityEngine.Vector3 currentTile=mainCamera.ScreenToWorldPoint(Input.mousePosition);
        currentTile[0]=currentTile[0]+cellSize/2;
        currentTile[0]=currentTile[0]/cellSize;
        currentTile[1]=currentTile[1]+cellSize/2;
        currentTile[1]=currentTile[1]/cellSize;
        //mousePosition.text=$"{(int)currentTile[0]},{(int)currentTile[1]}";
        //Tile _currentTile=getTileFromWorldPosition();
        currentTime+=Time.deltaTime;
    }
    public void GenerateGrid()
    {
        //texte.text=$"Nombre de Resets :{nbResets}";
        tileTab=new Tile[width,heigh];
        float _currentX=0f;
        float _currentY=0f;
        for(float x=0;x<width;x++)
        {
            for(int y=0;y<heigh;y++)
            {
                //-7.188329 ancien z des tuiles
                bool isOffset=((x%2==0 && y%2!=0)||(y%2==0&&x%2!=0));
                Tile spawnTile= Instantiate(_tile,new UnityEngine.Vector3(_currentX,_currentY,-7.0f),UnityEngine.Quaternion.identity);
                spawnTile.transform.localScale=new UnityEngine.Vector3(cellSize,cellSize);
                spawnTile._renderer.color=_tile.baseColor;
                spawnTile.name=$"Tile {x} {y}";
                _currentY+=cellSize;
                spawnTile.x=x;
                spawnTile.y=y;
                spawnTile.Init();
                spawnTile.isWalkable=true;
                tileTab[(int)x,(int)y]=spawnTile;
                
            }
            _currentX+=cellSize;
            _currentY=0;

        }
        cam.position= new UnityEngine.Vector3((float)width*cellSize/2 -0.5f,(float)heigh*cellSize/2 -0.5f,-10);

    }
    /**
    public voidGenerateGridIsometric()
    {

    }
    **/
    public void murAleatoires(float proba)
    {
        for(int i=0;i<width;i++)
        {
            for(int j=0;j<width;j++)
            {
                float rand=UnityEngine.Random.Range(1,100);
                if(rand>=proba*100)
                {
                    tileTab[i,j].isWalkable=false;
                    tileTab[i,j]._tileType=Tile.TileType.wall;
                    tileTab[i,j].Init();

                }
                else
                {
                    tileTab[i,j]._tileType=Tile.TileType.classic;
                    tileTab[i,j].Init();
                }
            }
        }

    }

    public void regenerateWalls(float proba)
    {
        for(int i=0;i<width;i++)
        {
            for(int j=0;j<width;j++)
            {
                tileTab[i,j].isWalkable=true;
            }
        }
        murAleatoires(proba);
        nbResets++;
        texte.text=$"Nombre de Resets :{nbResets}";
    }
    Tile GetTile(int x,int y)
    {
        return tileTab[x,y];
    }
    public Tile getTileFromWorldPosition()
    {
        UnityEngine.Vector3 currentTile=mainCamera.ScreenToWorldPoint(Input.mousePosition);
        currentTile[0]=currentTile[0]+cellSize/2;
        currentTile[0]=currentTile[0]/cellSize;
        currentTile[1]=currentTile[1]+cellSize/2;
        currentTile[1]=currentTile[1]/cellSize;
        if(currentTile[1]<0||currentTile[1]>heigh||currentTile[0]<0||currentTile[0]>width)
        {
            return null;
        }
        else
        {
            return tileTab[(int)currentTile[0],(int)currentTile[1]];

        }
    }
    public void CreateTile(int startx ,int starty,int endx, int endy,Tile.TileType type)//permet de modifier le terrain en ajoutant des carrés 
    {
        for(int x=startx;x<endx;x++)
        {
            for(int y=starty;y<endy;y++)
            {
                tileTab[x,y]._tileType=type;
                tileTab[x,y].SetSprite(tileTab[x,y].GetSprite());
            }
        }
    }
    
      
    
    
    
    
    //fonction de pathFinding
    public List<Tile> FindPath(int startX,int startY,int endX,int endY)
    {
        Tile start=GetTile(startX,startY);
        Tile end=GetTile(endX,endY);
        List<Tile> openList=new List<Tile>{start};
        List<Tile> closedList=new List<Tile>();
        for(int x=0;x<width;x++)
        {
            for(int y=0;y<heigh;y++)
            {
                Tile currentTile=GetTile(x,y);
                currentTile.gCost=int.MaxValue;
                currentTile.calculateFCost();
                currentTile.previousTile=null;
            }
        }
        start.gCost=0;
        start.hCost=calculateDistance(start,end);
        start.calculateFCost();
        while(openList.Count>0)//tant qu'on a pas tout exploré
        {
            Tile currentTile=GetLowestFCostTile(openList);
            if(currentTile==end)//si on est arrivé
            {
                return calculatePath(end);
            }
            openList.Remove(currentTile);
            closedList.Add(currentTile);
            //on parcour les voisins de la node actuelle
            foreach(Tile voisin in getNeighbourList(currentTile))
            {
                if(closedList.Contains(voisin))continue;//on a deja exploré ce voisin
                int tentativeGcost=currentTile.gCost+calculateDistance(currentTile,voisin);
                if(tentativeGcost<voisin.gCost)
                {
                    voisin.previousTile=currentTile;
                }
                voisin.gCost=tentativeGcost;
                voisin.hCost=calculateDistance(voisin,end);
                voisin.calculateFCost();
                if(!openList.Contains(voisin))
                {
                    openList.Add(voisin);
                }
            }
            //On a tout exploré
        }
        return null;//pas de chemin


   
    }
    public int calculateDistance(Tile a,Tile b)//pas de diagonale donc on se déplace de xb-xa+yb-ya
    {
        int xDistance=Mathf.Abs((int)a.x-(int)b.x);
        int yDistance=Mathf.Abs((int)a.y-(int)b.y);
        int remaning=Mathf.Abs(xDistance+yDistance);
        return MOVE_STRAIGHT_COST*remaning;
    }
    private Tile GetLowestFCostTile(List<Tile> tilelist)
    {
        Tile lowestTile =tilelist[0];
        for(int i=1;i<tilelist.Count;i++)
        {
            if(lowestTile.fCost>tilelist[i].fCost)
            {
                lowestTile=tilelist[i];
            }
        }
        return lowestTile;
    }
    private List<Tile> calculatePath(Tile endtile)
    {
        List<Tile> path= new List<Tile>();
        path.Add(endtile);
        Tile currentTile=endtile;
        while(currentTile.previousTile!=null)
        {
            path.Add(currentTile.previousTile);
            currentTile=currentTile.previousTile;
            
        }
        path.Reverse();
        return path;
    }
    public List<Tile> getNeighbourList(Tile _tile)
    {
        List <Tile> voisins= new List<Tile>();
        if(_tile.x-1>=0 && GetTile((int)_tile.x-1,(int)_tile.y).isWalkable) //left
        {
            voisins.Add(GetTile((int)_tile.x-1,(int)_tile.y));
        }
        if(_tile.x+1<width && GetTile((int)_tile.x+1,(int)_tile.y).isWalkable)//right
        {
            voisins.Add(GetTile((int)_tile.x+1,(int)_tile.y));
        }
        if(_tile.y-1>=0&& GetTile((int)_tile.x,(int)_tile.y-1).isWalkable) //Down
        {
            voisins.Add(GetTile((int)_tile.x,(int)_tile.y-1));
        }
        if(_tile.y+1<heigh&& GetTile((int)_tile.x,(int)_tile.y+1).isWalkable)//Up
        {
            voisins.Add(GetTile((int)_tile.x,(int)_tile.y+1));
        }
        return voisins;
    }
    public void DrawPath(List<Tile> chemin,Color _color)
    {
        if(chemin!=null)
        {
        foreach (Tile tile in chemin)
        {
            tile._renderer.color=_color;
        }

        }

    }
    public bool CanMooveTo(int sx,int sy,int ex,int ey)
    {
        return FindPath(sx,sy,ex,ey)!=null?true:false;
    }
    public bool EstArrive(int sx,int sy,int ex,int ey)
    {
        return sx==ex&&sy==ey?true:false;
    }
    public void  MooveToNextPosition(List<Tile> path)
    {
        _player.transform.position=new UnityEngine.Vector3(path[1].x*cellSize,path[1].y*cellSize,_player.transform.position.z);// 1 1 car 00 représente la position actuelle du personnage
        _player.x=(int)path[1].x;
        _player.y=(int)path[1].y;

    }
    UnityEngine.Vector3 FromTileToWorldPosition(Tile _tile)//retourne le centre de la tuile
    {
        UnityEngine.Vector3 retour= new UnityEngine.Vector3(_tile.x*cellSize,_tile.y*cellSize,_player.transform.position.z);
        return retour;
       
    }
    public void ResetAllColor()
    {
        for(int i=0;i<width;i++)
        {
            for(int j=0;j<heigh;j++)
            {
                tileTab[i,j].SetSprite(tileTab[i,j].GetSprite());
            }
        }
    }
    public bool IsInBounds(int x,int y)//retourne vrai si on peut se déplacer sur la case X,Y(check seulement les bordures)
    {
        if(x<0||x>=width||y<0||y>=heigh)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool isOccuped(int x,int y)
    {
        return tileTab[x,y]._currentCharacterInWorld==null?false:true;
    }

}

