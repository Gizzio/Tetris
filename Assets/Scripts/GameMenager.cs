using UnityEngine;
using System.Collections.Generic;

public class GameMenager : MonoBehaviour {

    
    public static GameMenager instance = null;
    public float bottomY = -5f;
    public float topY = 5f;
    public float width = 14f;
    public Object[] blocks;
    public float turnTime= 3f;
    public float turnAcceleration = 3f;
    public float speededTurn = 0.02f;
    public Object explosion;
    /*TODO: 
                -punktacja
                -sprawdzanie kolizji przed obrotem

                
    */

    private FallingElement blockCallingNextTurn = null;
    private bool isGameOver = false;
    

    public void SetNextTurn(FallingElement elementCalling)
    {
        blockCallingNextTurn = elementCalling;
    }

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

    private void NextTurn()
    {
        blockCallingNextTurn.transform.DetachChildren();
        Destroy(blockCallingNextTurn.gameObject);
        blockCallingNextTurn = null;

        RefreshLines();
        ChceckIfGameOver();
        if(!isGameOver)
            SpawnNewBlock();
    }

    private void SpawnNewBlock()
    {
        Vector3 startPoint = new Vector3(0, topY);
        if (Random.Range(0, 2) == 0)
        {
            Instantiate(blocks[Random.Range(0, 4)], startPoint, Quaternion.identity);
        }
        else
        {
            Quaternion rot = new Quaternion();
            rot.eulerAngles = new Vector3(0, 180, 0);
            Instantiate(blocks[Random.Range(0, 4)], startPoint, rot);
        }
        decreaseTurnTime();
    }

    private void RefreshLines()
    {
        for(float i = bottomY + 0.5f; i < topY - 0.5f; i++)//leci po kolejnych liniach od dołu do góry
        {
            CheckLine(i);
        }
    }

    private void CheckLine(float y)
    {
        Vector2 start = new Vector2( (width / 2) - 0.5f, y);
        Vector2 end = new Vector2( (-width / 2) + 0.5f, y);
        RaycastHit2D[] hits = Physics2D.LinecastAll(start, end);
        if (hits.Length == (int) width)
        {
            DestroyLine(hits);
            FallBlocksAbove(y);
        }
    }

    private void DestroyLine (RaycastHit2D[] hits)
    {
        foreach(RaycastHit2D hit in hits)
        {
            Instantiate(explosion, hit.transform.position, Quaternion.identity);
            Destroy(hit.transform.gameObject);
        }
    }

    private void FallBlocksAbove(float y)
    {
        GameObject[] blocks=GameObject.FindGameObjectsWithTag("SmallBlock");

        foreach(GameObject obj in blocks)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            Transform trans = obj.transform;
            if (obj.transform.position.y > y)
            {
                rb.MovePosition((Vector2)trans.position + Vector2.down);
            }
        }
    }

    private void ChceckIfGameOver()
    {
        //TODO
    }

    private void decreaseTurnTime()
    {
        turnTime = turnTime - turnTime * turnAcceleration;
    }

}
