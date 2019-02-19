using Achievements;
using StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ViewStartUI : MonoBehaviour
{
    [SerializeField] public Text score;
    [SerializeField] public Text health;

    public RawImage miniMap;

    public void Start()
    {

    }
    public void SetMiniMap(RenderTexture texture){
        miniMap.texture=texture;
    }

    public void DestroyUI()
    {
        //Debug.Log("InStartView");
        // Destroy(this.gameObject);
        score.gameObject.SetActive(false);
        health.gameObject.SetActive(false);
       
    }
    public void DisplayUI()
    {

        health.gameObject.SetActive(true);
        score.gameObject.SetActive(true);

    }
    public void UpdateScore(string scor)
    {
        score.text = scor;
    }
    public void UpdateHealth(string healt)
    {
        health.text = healt;
    }
   

}
