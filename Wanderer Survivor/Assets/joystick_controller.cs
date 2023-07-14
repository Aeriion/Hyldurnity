using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class joystick_controller : MonoBehaviour {
    
    public Transform player;
    public obj_player player_stats;
    private bool touchStart = false;
    private Vector2 pointA;     // correspond a la position de base du joystick 
    private Vector2 pointB;     // correspond a la position de deplacement du joystick
    private Vector2 pointC;     // correspond a la position de base du joueur 
    private Vector2 pointD;     // correspond a la position suivante du joueur
    private Vector2 direction;
    private Vector2 offSet;

    
    public Transform circle;
    public Transform outerCircle;

    void Update () {
        if(Input.GetMouseButtonDown(0) && Time.timeScale!=0f){
            pointA = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            pointC = Camera.main.ScreenToWorldPoint(new Vector2(player.position.x, player.position.y));
            
            circle.transform.position = pointA * 1;
            outerCircle.transform.position = pointA * 1;
            circle.GetComponent<SpriteRenderer>().enabled = true;
            outerCircle.GetComponent<SpriteRenderer>().enabled = true;
        }
    
        if(Input.GetMouseButton(0) && Time.timeScale!=0f){
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            pointD = Camera.main.ScreenToWorldPoint(new Vector2(player.position.x, player.position.y));
        }

        else{
            touchStart = false;
        }
                // détecte la direction de déplacement du joueur
        float moveInput = Input.GetAxis("Horizontal");

        // ajuste le point de focus en fonction de la direction de déplacement
        // mettre à jour la position du point de focus en fonction de la direction de déplacement
// ajuste le point de focus en fonction de la direction de déplacement
    }
    private void FixedUpdate(){
        if(touchStart){
            Vector2 offset = pointB - pointA;       //offset du joystick
            
            Vector2 offSet =pointD -pointC;         //offset du player
            Vector2 offSet3 =pointC - pointA;    
            direction = Vector2.ClampMagnitude(offset, 1.0f);
            moveCharacter(direction * 1);
           
            movePointA(direction * 1,offSet3*1);
            moveCircle(direction * 1,offSet,offSet*1);
        } else{
            circle.GetComponent<SpriteRenderer>().enabled = false;
            outerCircle.GetComponent<SpriteRenderer>().enabled = false;
            player_stats.speed=0f;
        }

        float percent_x=direction.x;
        float percent_y=direction.y;
        float diffx=Mathf.Abs(pointA.x - circle.transform.position.x);
        float diffy=Mathf.Abs(pointA.y - circle.transform.position.y);
        Debug.Log("x: " + percent_x);
        Debug.Log("y: " + percent_y);
        Debug.Log("diffx: " + diffx);
        Debug.Log("diffy: " + diffy);

        if((diffx<0.4 && diffy<0.4) && circle.GetComponent<SpriteRenderer>().enabled == true)
        {
            player_stats.speed=1.99f;
        }
        else if((diffx>=0.4 || diffy>=0.4) && circle.GetComponent<SpriteRenderer>().enabled == true)
        {
            player_stats.speed=3f;
        }

    }
void moveCharacter(Vector2 direction){
    if (direction.x < 0) {
        player.GetComponent<SpriteRenderer>().flipX = true;
        //player.GetChild(1).GetComponent<SpriteRenderer>().flipX = true;
    } else if (direction.x > 0) {
        player.GetComponent<SpriteRenderer>().flipX = false;
        //player.GetChild(1).GetComponent<SpriteRenderer>().flipX = false;
    }
    player.Translate(direction * player_stats.speed * Time.deltaTime);
}






    void movePointA(Vector2 direction,Vector2 offSet3){
        pointA = new Vector2 (outerCircle.transform.position.x , outerCircle.transform.position.y) ;
        
    }

    void moveCircle (Vector2 direction,Vector2 offSet,Vector2 offset){



        
        circle.transform.position = new Vector2(pointA.x + direction.x*0.6f, pointA.y + direction.y*0.6f) * 1;
    }

    public Vector2 GetDirection()
{
    return direction;
}
 }