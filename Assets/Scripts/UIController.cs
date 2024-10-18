using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestText;



    // Update is called once per frame
    void Update()
    {
    scoreText.text = "" + GameManager.singleton.score;
    bestText.text = "Best: " + GameManager.singleton.best;    
    }
}
