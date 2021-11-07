using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class GiftedUIScript : MonoBehaviour
{
    public static bool _clickedOnChoiceButton;
    [SerializeField] Text _textRef;
    [SerializeField] Animator _animRef;
    [SerializeField] Image _imageRef;
    private CanvasGroup _allUI;
    CharacterCharacteristics giftedCharacterStats;


    [SerializeField]private Button _selectButtonRef;
    // Start is called before the first frame update
    void Awake()
    {
        //Spawn Animation
        gameObject.transform.localScale=Vector3.zero;
        LeanTween.scale(this.gameObject,Vector3.one,1f);
        
        _allUI=gameObject.GetComponent<CanvasGroup>();
        _animRef=gameObject.GetComponentInChildren<Animator>();
        _imageRef=gameObject.GetComponentInChildren<Image>();
        _textRef=gameObject.GetComponentsInChildren<Text>()[1];
        _selectButtonRef=gameObject.GetComponentInChildren<Button>();
    }
    void Start()
    {
        int x=168;
        //this.gameObject.transform.position= new Vector3(168,276,gameObject.transform.position.z);
        _animRef.runtimeAnimatorController=getRandomAnimation();
        _textRef.text=getRandomName();
        _imageRef=null;
        x+=20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    string getRandomName()
    {
        return CharacterCharacteristics.SetRandomName();
    }
    RuntimeAnimatorController getRandomAnimation()
    {
        int r=Random.Range(1,5);
        Debug.Log("Valeur random" + r);
        switch(r)
        {
            case 1:
            this.giftedCharacterStats = new ArcherChar();
            return BanqueDeSprites.Instance._ArcherUIAnimation;//Pas d'anim encore donc image static
            case 2:
            this.giftedCharacterStats = new WarriorChar();
            return BanqueDeSprites.Instance._WarriorUIAnimation;
            case 3:
            this.giftedCharacterStats= new WizardChar();
            return BanqueDeSprites.Instance._WizardUIAnimation;
            case 4:
            this.giftedCharacterStats= new SlimeChar();
            return BanqueDeSprites.Instance._SlimeUIAnimation;
            default:
            return null;
        }
    }
    public void OnSelectButtonClick()
    {
        CharacterInWorld.remaningCharacterInPlayerTeamAtTheEndOfTheBattle.Add(this.giftedCharacterStats);
        GiftedUIScript._clickedOnChoiceButton=true;//Lance la destruction de L'ui Parent
    }
}
