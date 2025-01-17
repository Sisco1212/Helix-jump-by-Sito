using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    private Vector2 lastTapPos;
    private Vector3 startRotation;
    
    public Transform topTransform;
    public Transform goalTransform;
    public GameObject helixLevelPrefab;

    public List<Stage> allStages = new List<Stage>();
    private float helixDistance;
    private List<GameObject> spawnedLevels = new List<GameObject>();
    public Renderer helixRenderer;
    
    // private DeathPart deathPartColor;

    // Start is called before the first frame update
    void Awake()
    {
        // deathPartColor = FindObjectOfType<DeathPart>().
        startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + 0.1f);
        LoadStage(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)) {
            Vector2 curTapPos = Input.mousePosition;

            if(curTapPos == Vector2.zero) {
                lastTapPos = curTapPos;
            }
            float delta = lastTapPos.x - curTapPos.x;
            lastTapPos = curTapPos;
            transform.Rotate(Vector3.up * delta);
        }
        if(Input.GetMouseButtonUp(0)) {
        lastTapPos = Vector2.zero;
    }
    }

public void LoadStage(int stageNumber) {

    Stage stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count -1)];
    if(stage == null) {
        Debug.LogError("No stage " + stageNumber + " found in allStages list. Are all stages assigned?");
        return;
    }

    // stage.stageDeathPartColor = allStages[stageNumber].stageDeathPartColor;
    // Change background color of the stage
    Camera.main.backgroundColor = allStages[stageNumber].stageBackgroundColor;
    // Change color of the ball
    FindObjectOfType<BallController>().GetComponent<Renderer>().material.color = allStages[stageNumber].stageBallColor;
    // Change color of the helix
    helixRenderer.material.color = allStages[stageNumber].stageHelixColor;
    // reset helix rotation
    transform.localEulerAngles = startRotation;

      //delete old levels if there are any
    foreach(GameObject go in spawnedLevels) 
        Destroy(go);

    float levelDistance = helixDistance / stage.levels.Count;
    float spawnPosY = topTransform.localPosition.y;

    for(int i = 0; i < stage.levels.Count; i++) {
    spawnPosY -= levelDistance;
    // instantiate level
    GameObject level = Instantiate(helixLevelPrefab, transform);
    Debug.Log("Level created");
    level.transform.localPosition = new Vector3(0, spawnPosY, 0);
    spawnedLevels.Add(level);

    // disabling parts of the level prefab
    int partsToDisable = 12 - stage.levels[i].partCount;
    List<GameObject> disabledParts = new List<GameObject>();

    while(disabledParts.Count < partsToDisable) {
        GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;
        if(!disabledParts.Contains(randomPart)) {
            randomPart.SetActive(false);
            disabledParts.Add(randomPart);
        }
    }

    List<GameObject> leftParts = new List<GameObject>();

    //changing colors of the left parts
    foreach(Transform t in level.transform) {
        t.GetComponent<Renderer>().material.color = allStages[stageNumber].stageLevelPartColor;
        if(t.gameObject.activeInHierarchy) 
            leftParts.Add(t.gameObject);
        }

    foreach(Transform g in goalTransform) {
        g.GetComponent<Renderer>().material.color = allStages[stageNumber].stageLevelPartColor;
        }
           //creating the deathParts
        List<GameObject> deathParts = new List <GameObject>();

        while(deathParts.Count < stage.levels[i].deathPartCount) {
        GameObject randomPart = leftParts[(Random.Range(0, leftParts.Count))];
        if(!deathParts.Contains(randomPart)) {
            randomPart.gameObject.AddComponent<DeathPart>();
            randomPart.GetComponent<Renderer>().material.color = allStages[stageNumber].stageDeathPartColor;
            deathParts.Add(randomPart);            
        }
        }
    }

    
}


}
