using UnityEngine;
using System.Collections;

public class FallingElement : MonoBehaviour {

    
    public LayerMask blockingLayer;


    private float turnTime;
    private float timer = 0;
    private bool fallen = false;
    
    void Start()
    {
        turnTime = GameMenager.instance.turnTime;
    }

	void Update ()
    {
        if (!fallen)
        {
            UpdateTimer();
            if (Input.GetKeyDown(KeyCode.LeftArrow) && CanMove(Vector2.left))
            {
                transform.Translate(-1f, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && CanMove(Vector2.right))
            {
                transform.Translate(1f, 0, 0);
            }

            if (timer > turnTime)
            {
                TryToMoveDown();
            }
        }
	}

    void TryToMoveDown()
    {
        if(CanMove(Vector2.down))
        {
            MoveDown();
            timer = 0;
        }
        else
        {
            fallen = true;

           
            GameMenager.instance.SetNextTurn(this);
        }
    }

    void UpdateTimer()
    {
        timer += Time.deltaTime;
    }

    bool CanMove(Vector2 direction)//TODO: naprawić wykrywanie kolizji(czasem blokuje się na rogach)
    {
        TurnCollidersOnOff(false);
        bool isSpace = LineCast(direction);
        TurnCollidersOnOff(true);
        return isSpace;
    }

    void MoveDown()
    {
        transform.Translate(0, -1f, 0);
    }

    void TurnCollidersOnOff(bool state)
    {
        BoxCollider2D[] colliders = GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D boxCollider in colliders)
        {
            boxCollider.enabled = state;
        }
    }
    

    bool LineCast(Vector2 direction)
    {
        Transform[] positions=GetComponentsInChildren<Transform>();
        foreach(Transform trans in positions)
        {
            Vector2 start = trans.position;
            Vector2 end = start + direction;
            RaycastHit2D hit = Physics2D.Linecast(start, end, blockingLayer);
            if(hit.transform!=null)
            {
                return false;
            }
        }

        return true;

    }

    void SetTurnTime(float time)
    {
        turnTime = time;
    }

}
