using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;


//regarder comment générer une UI lorsque le joueur pourra ajouter un nouveau perso à sa team-> UI faite :D


//Ce script sera le script GLOBAL il assemble tout

public class GlobalScript : MonoBehaviour
{

    public enum GameState
    {
        preBattlePhase,//La phase ou on pose les personnages
        classic, 
        attackPhase,
        mooving,
        
    }
    /**
    public class level
    {
        List<Tile> _tiletab;
        string _nameLevel;
        List<Team> _allTeams;
        TileMap map
        void UpdateToCellSize()
        A l'entrée d'un niveau on place des enemis aléatoires création d'une team
        On les place tous sur le terrain et on Update currentTile sur les enemis et après il faut faire le drag and drop
    }
    **/
    public static bool clickOnCharacter=false;//a true si on click sur un caractere
    bool gameIsOver=false; //sera mi à true si une des conditions de fin de partie est remplie
    public static GameState _gameState;//Correspond à l'état du jeu global en fonction des actions du joueur
    int currentWave=1;



    private int nombreDeTours=1; //Correspond au nombre de tour depuis le début de la bataille
    public Text tours;
    public Text _gameStateText;

    
    //references
    [SerializeField] public GridManager _gridManager;
    [SerializeField] public CharacterMovement _movementManager;
    
    float currentTime=0;//sert d'horloge pour l'instant
    public static Team _playerTeam=new Team(UnityEngine.Color.blue,"Player Team");//référence unique à l'équipe du joueur (meme entre les niveaux)
    public Team _enemyTeam;// = new Team(UnityEngine.Color.red,"enemyTeam"); Ici la team est créée deux fois
    public static Team playingTeam;//La team dont c'est le tour actuellement



    void Start()
    {
        GlobalScript._gameState=GlobalScript.GameState.preBattlePhase;
        _enemyTeam= new Team(UnityEngine.Color.red,"enemy Team");//Ici la team est créée une seule fois
        currentWave=1;
        //GlobalScript._gameState=GameState.classic;
        //Ajout des enemies pour test (le premier enemy)
        CharacterInWorld firstWarrior= CharacterInWorld.createWarrior(0,0,_playerTeam);
        CharacterInWorld firstArcher = CharacterInWorld.createArcher(0,1,_playerTeam);
        CharacterInWorld.fillTeamWithRandomCharacters(this.currentWave,_enemyTeam,_gridManager);
        //CharacterInWorld.createArcher(2,2,_enemyTeam);

        
        foreach(Team t in Team._everyTeams)
        {
            foreach(CharacterInWorld c in t._team)
            {
                _movementManager.UpdateCurrentTile(c);
                c.associatedTile._tileType=Tile.TileType.classic;
                c.associatedTile.isWalkable=true;
                c.associatedTile.SetSprite(c.associatedTile.GetSprite());
                c._stats._currentLifePoints=1;

            }
        }
        //InitialisationTeam
        getActualTurnTeamPlaying();
        UpdateTextTurn();
      
    }
    
    

    // Update is called once per frame    
    void Update()//Gestion des clicks sur les tiles et tout
    {
        if(gameIsOver==false)
        {
            gameIsOver=GameIsOver();
        
            //TeamGestion
            if(TurnEnd())
            {
                NextTurn();
                UpdateTextTurn();
            }
            UpdateGameStateText();
        
            if(CharacterInWorld.clickOnCharacter)//si on a cliquer sur une personnage 
            {
                _gridManager.ResetAllColor();
                if((CharacterInWorld._clickedCharacter!=null)&&(GlobalScript._gameState==GlobalScript.GameState.classic))
                {
                    _gridManager.DrawPath(_movementManager.OnRangeMooving(CharacterInWorld._clickedCharacter),CharacterInWorld._clickedCharacter.team._transparantTeamColor);
                    CharacterInWorld.clickOnCharacter=false;
                }
                else
                {
                    if(CharacterInWorld._clickedCharacter!=null&&CharacterInWorld._targetCharacter!=null)
                    {
                        if(GlobalScript._gameState==GlobalScript.GameState.attackPhase&&CharacterInWorld._clickedCharacter._stats.getOnAttackingRangeTile(_gridManager,CharacterInWorld._clickedCharacter.associatedTile).Contains(CharacterInWorld._targetCharacter.associatedTile))
                        {
                            CharacterInWorld._clickedCharacter.Attack(CharacterInWorld._targetCharacter);//L'attaque est éffectuée
                            //Lancement de l'interface de combat
                            CharacterInWorld._clickedCharacter.Wait();
                            CharacterInWorld._clickedCharacter=null;
                            CharacterInWorld._targetCharacter=null;
                        }
                    }
                }
       
            }      
            if(Tile.clickOnTile&&CharacterInWorld._clickedCharacter!=null&&CharacterInWorld._clickedCharacter.hasMooved==false&&_movementManager.OnRangeMooving(CharacterInWorld._clickedCharacter).Contains(Tile._selectedTile)&&GlobalScript.playingTeam._team.Contains(CharacterInWorld._clickedCharacter)&&Tile._selectedTile._currentCharacterInWorld==null)
            {
                _movementManager.MooveCharacter((int)Tile._selectedTile.x,(int)Tile._selectedTile.y,CharacterInWorld._clickedCharacter);
                Tile.clickOnTile=false;
                _gridManager.ResetAllColor();
            }
            if(Tile.clickOnTile&&GlobalScript._gameState==GameState.attackPhase)//si on clique sur une case vide alors que l'on est en phase d'attaque.
            {
                _gridManager.ResetAllColor();
                CharacterInWorld._clickedCharacter=null;
                GlobalScript._gameState=GlobalScript.GameState.classic;
                Tile.clickOnTile=false;
            }
            CharacterInWorld.clickOnCharacter=false;
            Tile.clickOnTile=false;
            if(DragAndDropCharacter.RealeaseCharacter)
            {
                DragAndDropCharacter.RealeaseCharacter=false;
                if(_gridManager.getTileFromWorldPosition()!=null)
                {
                    CharacterInWorld._clickedCharacter.transform.position=_gridManager.getTileFromWorldPosition().transform.position;
                    CharacterInWorld._clickedCharacter.associatedTile=_gridManager.getTileFromWorldPosition();
                }
                else
                {
                    //on est hors limite
                    CharacterInWorld._clickedCharacter.transform.position=CharacterInWorld._clickedCharacter.associatedTile.transform.position;

                }
                CharacterInWorld._clickedCharacter=null;

            }
 
        }
        else
        {
            if(Team._everyTeams[0]!=_playerTeam)
            {
                returnToTitleScreen();

            }
            else
            {
                goToNextWave();
            }

        }
    } 
    public void UpdateSprite(CharacterInWorld updatingChar)
    {
        updatingChar.transform.position=new Vector3(updatingChar.transform.position.x*_gridManager.cellSize,updatingChar.transform.position.y*_gridManager.cellSize,updatingChar.transform.position.z);
        updatingChar.transform.localScale=new Vector3(_gridManager.cellSize,_gridManager.cellSize,0);
    }


    

    public void MooveAllPlayersRandom(Team moovingTeam)
    {
        foreach(CharacterInWorld chara in moovingTeam._team)
        {
            _movementManager.MooveAtRandomPosition(chara);
            chara.hasMooved=false;
            CharacterInWorld._clickedCharacter=null;
            Tile._selectedTile=null;
        }
    }
    public void MooveAllCharPlayerTeam()
    {
        MooveAllPlayersRandom(GlobalScript._playerTeam);
    }

 
    //Pour le nextTurn toutes les equipes dans une liste et current Team = liste[longueur%NbTours]
    
   
    void UpdateTextTurn()
    {
        
        tours.text="Tour de" +playingTeam._teamName;
        tours.color=playingTeam._teamColor;
    }
    public void UpdateGameStateText()
    {
        switch(GlobalScript._gameState)
        {
            case GameState.classic:  _gameStateText.text="classic";
            break;
            case GameState.attackPhase: _gameStateText.text="BattlePhase";
            break;
            case GameState.preBattlePhase: _gameStateText.text="PreBattlePhase";
            break;
        }
    }
    public void getActualTurnTeamPlaying()
    {
        playingTeam= Team._everyTeams[nombreDeTours%Team._everyTeams.Count];

    }
    public void NextTurn()
    {
        nombreDeTours++;
        getActualTurnTeamPlaying();
        playingTeam.rebootTeamCharacter();

    }
    public bool TurnEnd()
    {
        foreach(CharacterInWorld chara in playingTeam._team)
        {
            if(chara.hasMooved==false)
            {
                return false;
            }
        }
        return true;
    }
    public bool GameIsOver()
    {
        return Team._everyTeams.Count==1?true:false;
    }
    public void returnToTitleScreen()//Devra être remplacé par goToNextLevel
    {
        if(GameIsOver())
        {
            
            SceneManager.LoadScene("TitleScreenScene");

        }
    }
    public void goToNextWave()
    {
        currentWave++;
        CharacterInWorld.fillTeamWithRandomCharacters(currentWave,_enemyTeam,_gridManager);
        //CharacterInWorld wave2Char= CharacterInWorld.createSlime(5,5,_enemyTeam);
        Team._everyTeams.Add(_enemyTeam);
        //Faire apparaitre un texte Wave 1 ,Wave 2 , Wave 3, Wave 4 ...
        UnityEngine.Debug.Log("On passe à la wave " + currentWave);
        UnityEngine.Debug.Log("Longueur de enemy Team"+_enemyTeam._team.Count);
        foreach(CharacterInWorld c in _enemyTeam._team)
        {
                _movementManager.UpdateCurrentTile(c);
                c.associatedTile._tileType=Tile.TileType.classic;
                c.associatedTile.isWalkable=true;
                c.associatedTile.SetSprite(c.associatedTile.GetSprite());
                c._stats._currentLifePoints=1;
        }
        gameIsOver=false;
     
    }
    
    
    


}
