using System;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterUI : MonoBehaviour
{
    public static bool clickOnCharacter=false;
    [SerializeField]Text _uiName;
    [SerializeField]Text _uiAtq;
    [SerializeField]Text _uiMovement;
    [SerializeField]Text _uiClass;
    [SerializeField]Image _image;//Ui générale
    [SerializeField] Text _uiLifeRate;
    [SerializeField] Slider _uiLifeSlider;
    [SerializeField] Image _sliderRenderer;
    [SerializeField] Button _attackButton;
    [SerializeField] Button _waitButton;
    [SerializeField] GridManager _grille;
    [SerializeField] Button _endTurnButton;
    [SerializeField] Button _showHideButton;
    [SerializeField] CanvasGroup _characterUI;
    bool showHide= false; //Hide ->false Show->true
    
    
    // Start is called before the first frame update
    void Start()
    {
        //Ajouter une ligne de commande pour masquer le curseur du slider (la c'est fait dans l'inspector);        
        this._image.gameObject.SetActive(false);
        _uiLifeSlider.minValue=0;
        _uiLifeSlider.onValueChanged.AddListener((v)=>//renvoi la valeur du slider qui va entre 1 et maxLifePoints
        {
            _uiLifeRate.text=v.ToString()+" / "+CharacterInWorld._clickedCharacter._stats._maxLifePoints;
        });
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CharacterUI.clickOnCharacter&&CharacterInWorld._clickedCharacter!=null)
        {
            this._image.gameObject.SetActive(true);//On affiche l'UI qui correspond au personnage
            
            _uiName.text=CharacterInWorld._clickedCharacter._stats._name;
            _uiMovement.text=CharacterInWorld._clickedCharacter._stats._movementPoints.ToString();
            _uiAtq.text=CharacterInWorld._clickedCharacter._stats._force.ToString();
            _uiClass.text=CharacterInWorld._clickedCharacter._stats.GetType().ToString();
            CharacterUI.clickOnCharacter=false;

            //On s'occupe du slider et du text permettant d'update la barre de vie
            _uiLifeSlider.maxValue=CharacterInWorld._clickedCharacter._stats._maxLifePoints;
            
            _uiLifeSlider.value=CharacterInWorld._clickedCharacter._stats._currentLifePoints;
            _sliderRenderer.color=CharacterInWorld._clickedCharacter.team._teamColor;
            
        }
        if(CharacterInWorld._clickedCharacter==null)
        {
            this._image.gameObject.SetActive(false);
        }
    }
    public void OnAttackButtonClick()
    {
        GlobalScript._gameState=GlobalScript.GameState.attackPhase;
        _grille.ResetAllColor();
        _grille.DrawPath(CharacterInWorld._clickedCharacter._stats.getOnAttackingRangeTile(_grille,CharacterInWorld._clickedCharacter.associatedTile),new Color(1,0,0,0.5f));
        //on passe l'état du jeu en mode "selection de cible ou attaque"
        Debug.Log("On affiche la range d'attaque de "+CharacterInWorld._clickedCharacter.name);
        if(CharacterInWorld._clickedCharacter._stats.GetType().ToString()=="WizardChar")
        {
            Debug.Log("La l'animation devrait changer enft");//On passe en animation d'attaque
            CharacterInWorld._clickedCharacter.LaunchAttackAnimation();
        }
    }
    public void OnWaitButtonClick()
    {
        CharacterInWorld._clickedCharacter.Wait();

    }
    public void OnEndTurnButton()
    {
        foreach(CharacterInWorld c in GlobalScript.playingTeam._team)
        {
            c.Wait();
        }
    }
    public void OnShowHideButtonBehavior(bool showHide)
    {
        if(showHide)//on affiche l'UI
        {
            //this._image.gameObject.SetActive(true);
            LeanTween.moveLocalX(this._image.gameObject,374,1f);
            LeanTween.rotate(_showHideButton.gameObject,new Vector3(0,0,-270),0.1f);
        }
        else
        {
            LeanTween.moveLocalX(this._image.gameObject,618,1f);
            LeanTween.rotate(_showHideButton.gameObject,new Vector3(0,0,-90),0.1f);
            //this._image.gameObject.SetActive(false);
        }   
    }
    public void OnShowHideButtonClick()
    {
        OnShowHideButtonBehavior(showHide);
        showHide=!showHide;
    }

}
