using UnityEngine;
using System.Collections.Generic;

public class GameMenager : MonoBehaviour {

    
    public static GameMenager instance = null;
    public float startY = 5.0f;
    public Object[] blocks;
    public float turnTime= 3f;
    public float turnAcceleration = 3f;
    /*TODO: 
                -punktację
                -wykrywanie linii
                -ściany
    */

    private List<Transform> fallenBlocks=new List<Transform>();
    private FallingElement blockCallingNextTurn = null;
    private bool isGameOver = false;

    void Awake()//Implementacja singletona
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

	void Start ()
    {
        SpawnNewBlock();
	}

    void Update()
    {
        if(blockCallingNextTurn!=null)
        {
            NextTurn();
        }
    }

    public void NextTurn()
    {
        blockCallingNextTurn.transform.DetachChildren();
        Destroy(blockCallingNextTurn.gameObject);
        blockCallingNextTurn = null;

        ChceckLines();
        ChceckIfGameOver();
        if(!isGameOver)
            SpawnNewBlock();
    }

    private void ChceckLines()
    {
        //Y: -4.5 -3.5 -2.5 -1.5 ... startY <- kolejne rzędy do sprawdzenia
        //fallenBlocks.BinarySearch
    }

    private void ChceckIfGameOver()
    {
        //TODO
    }

    public void SpawnNewBlock()
    {
        Vector3 startPoint = new Vector3(0, startY, 0);
        Instantiate(blocks[Random.Range(0, 4)], startPoint, Quaternion.identity);
        decreaseTurnTime();
        
    }

    private void decreaseTurnTime()
    {
        turnTime = turnTime - turnTime * turnAcceleration;
    }

    public void SetNextTurn(FallingElement elementCalling)
    {
        RegisterFallenBlocks(elementCalling);
        blockCallingNextTurn = elementCalling;
    }

    void RegisterFallenBlocks(FallingElement elementCalling)
    {
        Transform[] transforms = elementCalling.GetComponentsInChildren<Transform>();
        fallenBlocks.AddRange(transforms);//dodaje transforms do fallenBlocks
    }
    
}
