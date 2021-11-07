using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//SceneManager.GetActiveScene().buildIndex

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField]Image _titleScreenImage;
    [SerializeField] CanvasGroup _background;    
    void Start()
    {
        //LeanTween.scale(titleScreenImage.gameObject,new Vector3(0,0,0),0.5f).setOnComplete(LaunchFirstLevel);
        //Fait scale l'image jusque a Vector 3 en 0.5 secondes puis lance la fonction LaunchFirstLevel
    }
    // Start is called before the first frame update
 
    public void LaunchFirstLevel()
    {
        //LeanTween.rotateAround(_titleScreenImage,Vector3.left,180,0.1f);
        //LeanTween.alphaCanvas(_titleScreenImage.gameObject,0,2f);//This part DontWork
        //LeanTween.alpha(_titleScreenImage.gameObject,0f,2f);
        LeanTween.alphaCanvas(_background,0f,2f).setOnComplete(LaunchLevel);
        //LeanTween.scale(_titleScreenImage.gameObject,Vector3.zero,2f).setOnComplete(LaunchLevel);
    }
    public void LaunchLevel()
    {
        SceneManager.LoadScene("BattleFieldScene");
    }
 
    
}
