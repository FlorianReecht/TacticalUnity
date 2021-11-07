using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelUIScript : MonoBehaviour
{
    
    [SerializeField]GiftedUIScript _firstChoice;
    [SerializeField] GiftedUIScript _secondChoice;
    [SerializeField] GiftedUIScript _thirdChoice;
    [SerializeField] CanvasGroup _background;
    Text _chooseOneText;

    // Start is called before the first frame update
    void Awake()
    {
        _chooseOneText=gameObject.GetComponentInChildren<Text>();
        LeanTween.scale(_chooseOneText.gameObject,new Vector3(3,3,gameObject.transform.position.z),0.2f);
        LeanTween.moveLocalX(_chooseOneText.gameObject,140,0.2f);
    }
    void Start()
    {
        GiftedUIScript._clickedOnChoiceButton=false;

    }
    void Update()
    {
        if(GiftedUIScript._clickedOnChoiceButton)
        {
            LeanTween.alphaCanvas(_background,0f,2f).setOnComplete(DestroyMe);
        }
    }
    void DestroyMe()
    {
        Destroy(this.gameObject);
    }
    void OnDestroy()
    {
        Destroy(_firstChoice.gameObject);
        Destroy(_secondChoice.gameObject);
        Destroy(_thirdChoice.gameObject);   
    }

    
   
}
