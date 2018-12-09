using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContoller : MonoBehaviour
{
    public List<Vector3> normanDropPoints = new List<Vector3> { new Vector3(300,0,326), new Vector3(0,0,326), new Vector3(-300,0,326),
                                                               new Vector3(300,0,663), new Vector3(0,0,663), new Vector3(-300,0,663),
                                                               new Vector3(300,0,1000), new Vector3(0,0,1000), new Vector3(-300,0,1000)};
    public List<Vector3> saxonDropPoints = new List<Vector3> { new Vector3(300,0,-326), new Vector3(0,0,-326), new Vector3(-300,0,-326),
                                                               new Vector3(300,0,-663), new Vector3(0,0,-663), new Vector3(-300,0,-663),
                                                               new Vector3(300,0,-1000), new Vector3(0,0,-1000), new Vector3(-300,0,-1000)};
    private DrawScript SDS;
    private DrawScript NDS;
    public int saxonResources = 0;
    public int normanResources = 0;


    // Use this for initialization
    void Start()
    {
        SDS = GameObject.Find("SaxonDeck").GetComponent<DrawScript>();
        NDS = GameObject.Find("NormanDeck").GetComponent<DrawScript>();
        StartCoroutine("startDraw");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator startDraw()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.25f);
            SDS.drawFunc(1);
            NDS.drawFunc(1);
        }
    }

    public void EndTurn()
    {
    }

    public void damageObjective()
    {
    }

    public void damageWedge()
    {
    }

    public void damageEnemy()
    {
    }
}
