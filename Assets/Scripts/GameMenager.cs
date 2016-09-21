using UnityEngine;
using System.Collections;

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

    private FallingElement blockCallingNextTurn = null;
    private bool isGameOver = true;

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
        ///TODO
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
        turnTime = turnTime - (turnTime / turnAcceleration);
    }

    public void SetNextTurn(FallingElement elementCalling)
    {
        blockCallingNextTurn = elementCalling;
    }

    
}
