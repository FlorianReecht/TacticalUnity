using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInWorld : MonoBehaviour
{
    [SerializeField] SpriteRenderer _rendererRef;
    [SerializeField] Transform _transformRef;
    //Reférences aux statistiques du personnage
    public Animator _animatorRef;
    public CharacterCharacteristics _stats;
    public static CharacterInWorld _clickedCharacter;
    public static CharacterInWorld _targetCharacter;
    public static bool clickOnCharacter;
    public int _x,_y;
    public Team team;
    public const float  character_movement_speed=10f;
    
    public bool hasMooved,hasAttacked;
    //Attributs nécéssaires aux repérage sur le terrain
    public Tile associatedTile;
    public List<Tile> moovingRange;
    public static List<CharacterCharacteristics> remaningCharacterInPlayerTeamAtTheEndOfTheBattle= new List<CharacterCharacteristics>();
    // Start is called before the first frame update
    
    public SpriteRenderer GetRenderer()
    {
        return _rendererRef;
    }
    public Transform GetTransform()
    {
        return _transformRef;
    }
    void OnMouseDown() 
    {
        Tile._selectedTile=null;//evite de deplacer le personnage sur la derniere case (si on veut deplacer deux persos enfin jmecomprend koi)
        CharacterUI.clickOnCharacter=true;
        CharacterInWorld.clickOnCharacter=true;
        //En mode classic -> on setup le personnage séléctionné par le joueur -> if faut aussi vérifier qu'il fait partie de la team de l'utilisateur
        //On selectionne un personnage
        if(GlobalScript._gameState==GlobalScript.GameState.classic||GlobalScript._gameState==GlobalScript.GameState.preBattlePhase)
        {
            if(this==CharacterInWorld._clickedCharacter)//si on reclique sur un personnage, on annule la séléction.
            {
                CharacterInWorld._clickedCharacter=null;
            }
            else
            {
                CharacterInWorld._clickedCharacter=this;
            }
        }
        if(GlobalScript._gameState==GlobalScript.GameState.attackPhase)
        {
            if(CharacterInWorld._clickedCharacter!=null)
            {
                if(GlobalScript._gameState==GlobalScript.GameState.attackPhase&&this.team!=CharacterInWorld._clickedCharacter.team&&CharacterInWorld._clickedCharacter.hasAttacked==false&&CharacterInWorld._clickedCharacter!=null)//ça veut dire qu'on click sur la cible
                {
                    Debug.Log("On vérifie si la cible est valide");
                    CharacterInWorld._targetCharacter=this;
                }
                else
                {
                    Debug.Log("la cible est de la même équipe que l'attaquant ");
                    CharacterInWorld._clickedCharacter=null;
                    CharacterInWorld._targetCharacter=null;
                }
            }
         

        }
        //On clique sur la cible
     
       
           
    }
    
    public static CharacterInWorld createCharacter(int x,int y,Team team)
    {
        CharacterInWorld retour= Instantiate(BanqueDeSprites.Instance._baseCharacter,new Vector3(x,y,-8),Quaternion.identity);
        retour._x=x;
        retour._y=y;
        retour.hasMooved=false;
        retour.hasAttacked=false;
        team.AddInTeam(retour);
        retour.team=team;
        return retour;
    }
    public static CharacterInWorld createWarrior(int x,int y,Team team)
    {
        CharacterInWorld retour=CharacterInWorld.createCharacter(x,y,team);
        retour._stats=new WarriorChar();
        retour._rendererRef.sprite=BanqueDeSprites.Instance.WarriorSprite;
        retour._animatorRef.runtimeAnimatorController=BanqueDeSprites.Instance._warriorAnimatorController;
        retour.GetRenderer().flipX=true;
        return retour;
    }
    public static CharacterInWorld createArcher(int x,int y,Team team)
    {
        CharacterInWorld retour=CharacterInWorld.createCharacter(x,y,team);
        retour._stats=new ArcherChar();
        retour._rendererRef.sprite=BanqueDeSprites.Instance.ArcherSprite;
        return retour;
    }
    public static CharacterInWorld createWizard(int x,int y,Team team)
    {
        CharacterInWorld retour=CharacterInWorld.createCharacter(x,y,team);
        retour._stats=new WizardChar();
        retour._rendererRef.sprite=BanqueDeSprites.Instance.WizardSprite;
        retour._rendererRef.transform.localScale=new Vector3(0.60936f,0.6020492f,1);
        retour._animatorRef.runtimeAnimatorController=BanqueDeSprites.Instance._wizardIdleAnimation;
        retour._rendererRef.flipX=true;
        return retour;
    }
    public static CharacterInWorld createSlime(int x,int y,Team team)
    {
        CharacterInWorld retour=CharacterInWorld.createCharacter(x,y,team);
        retour._stats= new SlimeChar();
        retour._rendererRef.sprite=BanqueDeSprites.Instance.SlimeSprite;
        retour._rendererRef.transform.localScale=new Vector3(0.9108751f,0.95886f,1f);
        retour._animatorRef.runtimeAnimatorController=BanqueDeSprites.Instance._slimeAnimatorController;
        return retour;
    }
   
    public void Attack(CharacterInWorld attackedChar)
    {
        //Ouverture de l'interface d'attaque
        attackedChar._stats._currentLifePoints-=this._stats._force;
        this.hasAttacked=true;
        this.hasMooved=true;
        Debug.Log(attackedChar._stats._name +" a perdu "+ this._stats._force+ "Points de vies");
        if(attackedChar._stats._currentLifePoints<=0)
        {
            Destroy(attackedChar);
        }
    }

    public void Die()
    {
        this.team._team.Remove(this);//on le retire de la liste
        if(this.team._team.Count==0)
        {
            Team._everyTeams.Remove(this.team);
        }
        Destroy(this);
        Destroy(this.gameObject);

    }
    public bool CheckIfValidTarget(CharacterInWorld target)
    {
        return target.team==this.team?false:true;
    }
    public void LaunchAttackAnimation()
    {
        this._animatorRef.SetTrigger("Attack");
    }
    public void LaunchWalkAnimation()
    {
        this._animatorRef.SetTrigger("Walk");
    }
    public void Wait()
    {
        this.hasAttacked=true;
        this.hasMooved=true;
        this.GetRenderer().color=new Color(0.3773f,0.3773f,0.3773f,1f);
        GlobalScript._gameState=GlobalScript.GameState.classic;
    }
    public  void OnDestroy()
    {
        this.Die();   
    }
    public static CharacterInWorld createRandomClass(int x,int y,Team team)
    {
        int r=UnityEngine.Random.Range(0,CharacterCharacteristics.nbClass);
        switch(r)
        {
            default:
            case 1:
            return CharacterInWorld.createWarrior(x,y,team);
            case 2:
            return CharacterInWorld.createWizard(x,y,team);
            case 3:
            return CharacterInWorld.createArcher(x,y,team);
            case 4:
            return CharacterInWorld.createSlime(x,y,team);
            
        }

    }
    public static void fillTeamWithRandomCharacters(int len,Team team,GridManager grille)
    {
        for(int i=0;i<len;i++)
        {
            int x=UnityEngine.Random.Range(0,grille.width-1);
            int y=UnityEngine.Random.Range(0,grille.heigh-1);
            while(grille.tileTab[x,y]._currentCharacterInWorld!=null)
            {
                x=UnityEngine.Random.Range(0,grille.width-1);
                y=UnityEngine.Random.Range(0,grille.heigh-1);   
            }
            CharacterInWorld futurCharacter=CharacterInWorld.createRandomClass(x,y,team);
            

        }
    }
    

    

}
