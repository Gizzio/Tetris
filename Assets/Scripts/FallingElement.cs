using UnityEngine;
using System.Collections;

public class FallingElement : MonoBehaviour {

    
    public LayerMask blockingLayer;


    private float turnTime;
    private float timer = 0;
    private float speededTurn = 0.1f;
    private bool canTurn;
    
    void Start()
    {
        canTurn = true;
        ChangeColors();
        turnTime = GameMenager.instance.turnTime;
        speededTurn = GameMenager.instance.speededTurn;
       
    }

    void FixedUpdate()
    {
        //canTurn = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.IsChildOf(transform))
        {
            canTurn = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.transform.IsChildOf(transform))
        {
            canTurn = true;
        }
    }

    void Update ()
    {
            UpdateTimer();
            if (Input.GetKeyDown(KeyCode.LeftArrow) && CanMove(Vector2.left))
            {
                Move(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && CanMove(Vector2.right))
            {
                Move(Vector2.right);
            }
            
            if(Input.GetKeyDown(KeyCode.Space) && canTurn )
            {
                TurnAround();
            }
            
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                turnTime = speededTurn;
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                turnTime = GameMenager.instance.turnTime;
            }

            if (timer > turnTime)
            {
                TryToMoveDown();
            }
	}

    void TryToMoveDown()
    {
        if(CanMove(Vector2.down))
        {
            ChangeColors();
            Move(Vector2.down);
            timer = 0;
        }
        else
        {
            GameMenager.instance.SetNextTurn(this);
        }
    }

    void TurnAround()
    {
        GetComponent<Rigidbody2D>().MoveRotation(90f + GetComponent<Rigidbody2D>().rotation);
    }



    void UpdateTimer()
    {
        timer += Time.deltaTime;
    }

    bool CanMove(Vector2 direction)
    {
        TurnCollidersOnOff(false);
        bool isSpace = LineCast(direction);
        TurnCollidersOnOff(true);
        return isSpace;
    }

    void Move(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().MovePosition((Vector2)transform.position + direction);
      
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
        Transform[] transforms=GetComponentsInChildren<Transform>();
        foreach(Transform trans in transforms)
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

    void ChangeColors()
    {
        SpriteRenderer[] renders = GetComponentsInChildren<SpriteRenderer>();

        foreach( SpriteRenderer rend in renders)
        {
            Color col = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            rend.color = col;
        }
    }


}
