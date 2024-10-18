using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPart : MonoBehaviour
{
    private HelixController helixControllerScript;


    private void Awake() {
        helixControllerScript = FindObjectOfType<HelixController>();
    }
   
    // private void OnEnable() {
    //     GetComponent<Renderer>().material.color = helixControllerScript.allStages[GameManager.singleton.currentStage].stageDeathPartColor;
    // }

    // private void Update() {
    //     GetComponent<Renderer>().material.color = helixControllerScript.allStages[GameManager.singleton.currentStage].stageDeathPartColor;
    // }

    public void HitDeathPart() {
        GameManager.singleton.RestartLevel();
    }

}
