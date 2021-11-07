using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerPosition;
    public GridManager _manager;
    [SerializeField] public SpriteRenderer playerRenderer;
    public int x,y;
    void spawn()
    {
        playerPosition.position= new Vector3(0f,0f,-8f);
        playerPosition.localScale= new Vector3(_manager.cellSize,_manager.cellSize,_manager.cellSize);
        x=0;
        y=0;
        

    }
    bool canMoove(float x,float y)//retourne vrai si on peut se déplacer sur la case X,Y(check seulement les bordures)
    {
        if(x<-_manager.cellSize/2||x>_manager.width*_manager.cellSize-_manager.cellSize/2||y<-_manager.cellSize/2||y>_manager.heigh*_manager.cellSize-_manager.cellSize/2)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            Vector3 Nposition=new Vector3(playerPosition.position.x+_manager.cellSize,playerPosition.position.y,playerPosition.position.z);
            if(canMoove(Nposition.x,Nposition.y))
            {
                if(_manager.tileTab[x+1,y].isWalkable)
                {
                    playerPosition.position=Nposition;
                    x+=1;
                    playerRenderer.flipX=false;
               
                     for(int i=0;i<_manager.heigh;i++)
                    {
                        for(int j=0;j<_manager.width;j++)
                        {
                            _manager.tileTab[i,j]._renderer.color=_manager.tileTab[i,j].isWalkable?_manager.tileTab[i,j].baseColor:_manager.tileTab[i,j].wallColor;
                        }
                    }
                    List<Tile> chemin=_manager.FindPath(x,y,9,9);//on recalcule le pathFinding
                    _manager.DrawPath(chemin,new Color(1,1,1,1));
                }
         
            }
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Vector3 Nposition=new Vector3(playerPosition.position.x,playerPosition.position.y+_manager.cellSize,playerPosition.position.z);
            if(canMoove(Nposition.x,Nposition.y))
            {
                if(_manager.tileTab[x,y+1].isWalkable)
                {
                    playerPosition.position=Nposition;
                    y+=1;
                
                     for(int i=0;i<_manager.heigh;i++)
                    {
                        for(int j=0;j<_manager.width;j++)
                        {
                            _manager.tileTab[i,j]._renderer.color=_manager.tileTab[i,j].isWalkable?_manager.tileTab[i,j].baseColor:_manager.tileTab[i,j].wallColor;
                        }
                    }
                    List<Tile> chemin=_manager.FindPath(x,y,9,9);//on recalcule le pathFinding
                    _manager.DrawPath(chemin,new Color(1,1,1,1));
                    
                }
                
                       
            }        
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 Nposition=new Vector3(playerPosition.position.x-_manager.cellSize,playerPosition.position.y,playerPosition.position.z);
            if(canMoove(Nposition.x,Nposition.y))
            {
                if(_manager.tileTab[x-1,y].isWalkable)
                {
                    playerPosition.position=Nposition;
                    x-=1;
                    playerRenderer.flipX=true;
                    for(int i=0;i<_manager.heigh;i++)
                    {
                        for(int j=0;j<_manager.width;j++)
                        {
                            _manager.tileTab[i,j]._renderer.color=_manager.tileTab[i,j].isWalkable?_manager.tileTab[i,j].baseColor:_manager.tileTab[i,j].wallColor;
                        }
                    }
                    List<Tile> chemin=_manager.FindPath(x,y,9,9);//on recalcule le pathFinding
                    _manager.DrawPath(chemin,new Color(1,1,1,1));
                }        
            }
                   
            }        
        
        if(Input.GetKeyDown(KeyCode.S))
        {
            Vector3 Nposition=new Vector3(playerPosition.position.x,playerPosition.position.y-_manager.cellSize,playerPosition.position.z);
            if(canMoove(Nposition.x,Nposition.y))
            {
                if(_manager.tileTab[x,y-1].isWalkable)
                {
         
                    y-=1;
                    for(int i=0;i<_manager.heigh;i++)
                    {
                        for(int j=0;j<_manager.width;j++)
                        {
                            _manager.tileTab[i,j]._renderer.color=_manager.tileTab[i,j].isWalkable?_manager.tileTab[i,j].baseColor:_manager.tileTab[i,j].wallColor;
                        }
                    }
                    
                    List<Tile> chemin=_manager.FindPath(x,y,9,9);//on recalcule le pathFinding
                    playerPosition.position=Nposition;
                    _manager.DrawPath(chemin,new Color(1,1,1,1));
                    
                }    
                
            
            }       
        }
        
        
    }
}
