using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropCharacter : MonoBehaviour
{
    float startX,startY;
    bool isMooving=false;
    CharacterInWorld charRef;
    public static bool RealeaseCharacter=false;
    // Start is called before the first frame update

    void Awake()
    {
        charRef = GetComponentInParent<CharacterInWorld>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GlobalScript._gameState==GlobalScript.GameState.preBattlePhase&&CharacterInWorld._clickedCharacter==charRef)
        {
            Vector3 mousePos;
            mousePos=Input.mousePosition;
            mousePos=Camera.main.ScreenToWorldPoint(mousePos);
            startX=mousePos.x-this.transform.localPosition.x;
            startY=mousePos.y-this.transform.localPosition.y;
            this.transform.position=new Vector3(mousePos.x,mousePos.y,this.transform.localPosition.z);
            Debug.Log("U should Follow The Mouse Dumb");
            Debug.Log("Mouse Pos :"+mousePos);

        }

        
    }
    void OnMouseDown()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 mousePos;
            mousePos=Input.mousePosition;
            mousePos=Camera.main.ScreenToWorldPoint(mousePos);
            startX=mousePos.x-this.transform.localPosition.x;
            startY=mousePos.y-this.transform.localPosition.y;   
            Debug.Log("click");
            Debug.Log("Name : " + charRef._stats._name);
            
        }
    }
    private void OnMouseUp() 
    {
        if(GlobalScript._gameState==GlobalScript.GameState.preBattlePhase)
        {
            isMooving=false;
            DragAndDropCharacter.RealeaseCharacter=true;//On envoie aux autres scripts que l'on relache un personnage.
        }
       
    }
 
}
