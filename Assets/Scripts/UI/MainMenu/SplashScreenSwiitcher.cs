using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenSwiitcher : MonoBehaviour
{
    [SerializeField] private Image bgImage;
    [SerializeField] private Sprite[] bgArtCollection;

    void Start(){
        bgImage = GameObject.Find("BG-Image").GetComponent<Image>();
        if(bgImage == null){
            Debug.LogWarning("No Image Component Found");
        }
    }
}
