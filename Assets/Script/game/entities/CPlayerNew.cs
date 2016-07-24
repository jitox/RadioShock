﻿using UnityEngine;
using System.Collections;

public class CPlayerNew : CAnimatedSprite
{
    private const int STATE_NORMAL = 0;
    private const int STATE_JUMPING = 2;
    private const int STATE_FALLING = 3;
    private const int STATE_DIE = 4;
    private bool midJump = false;


    private const int SPEED = 240;
    private const int JUMP_SPEED = -500;
    private int GRAVITY = 900;

    private const int PLAYER_WIDTH = 64;
    private const int PLAYER_HEIGHT = 74;

    // Variables para chequeo de colisiones con los tiles.
    private bool tileTopLeft;
    private bool tileTopRight;
    private bool tileDownLeft;
    private bool tileDownRight;

    // Columnas y filas usadas en checkPoints().
    int leftTileX;
    int upTileY;
    int rightTileX;
    int downTileY;

    // Variables para solucionar el problema de las colisiones en diagonal.
    int mOldX;
    int mOldY;

    public CPlayerNew()
    {
        setFrames(Resources.LoadAll<Sprite>("Sprites/player"));

        setSortingLayerName("Player");
        setVelX(250);
        setXY(0, 600);

        setRegistration(REG_TOP_LEFT);

        setWidth(PLAYER_WIDTH);
        setHeight(PLAYER_HEIGHT);

        setState(STATE_NORMAL);

        setOldXYPosition();      
          render();
    }

    override public void update()
    {

        switch (getState())
        {
            case STATE_NORMAL:
                //chequeo si tiene que caer
                if (!checkFloor())
                {
                    setState(STATE_FALLING);
                }
                if (checkRightWall())
                {
                    setState(STATE_DIE);
                }
                jumpControl();
               
                break;

            case STATE_FALLING:
                if (checkFloor())
                {
                    setState(STATE_NORMAL);
                }
                               
                break;
            case STATE_JUMPING:
                if (checkFloor())
                {
                    setState(STATE_NORMAL);
                }
                checkRoof();

                if (checkRightWall())
                {
                    setState(STATE_DIE);
                }
                jumpControl();
                break;
            
        }
        base.update();


    }

    private bool checkFloor()
    {

        if (GRAVITY > 0)
        {
            checkPoints((int)getX(), (int)getY() + 1);
            if (!tileDownLeft || !tileDownRight)
            {
                setY((downTileY * CTileMap.TILE_HEIGHT) - PLAYER_HEIGHT);
                               
                return true;
            }
        }
        else
        {
            checkPoints((int)getX(), (int)getY() - 1);
            if (!tileTopLeft || !tileTopRight)
            {
                setY(((upTileY + 1) * CTileMap.TILE_HEIGHT));
                
                return true;
            }
        }


        return false;
    }

    private bool checkRoof()
    {
        if (GRAVITY < 0)
        {
            checkPoints((int)getX(), (int)getY() + 1);
            if (!tileDownLeft || !tileDownRight)
            {
                setY((downTileY * CTileMap.TILE_HEIGHT) - PLAYER_HEIGHT -1 );
                setVelY(0);
                return true;
            }
        }
        else
        {
            checkPoints((int)getX(), (int)getY() - 1);
            if (!tileTopLeft || !tileTopRight)
            {
                setY(((upTileY + 1) * CTileMap.TILE_HEIGHT+1));
                setVelY(0);
                return true;
            }
        }


        return false;
    }

    private bool checkRightWall()
    {
        checkPoints((int)getX() + (int)(SPEED * Time.deltaTime), (int)getY());
        if (tileTopRight && tileDownRight)
        {
            return false;
           
        }
        else
        {
            // Corregir contra el tile con el que choca.
            setX((rightTileX * CTileMap.TILE_WIDTH) - PLAYER_WIDTH);

            
            return true;
        }
    }
  

    override public void render()
    {
        base.render();
    }

    private void jumpControl()
    {
        if (!midJump)
        {
            if ((CKeyboard.firstPress(CKeyboard.SPACE) || CKeyboard.firstPress(CKeyboard.UP)))
            {
                if (getState() == STATE_JUMPING)
                {
                    midJump = true;
                }
                setState(STATE_JUMPING);
                return;
            }
        }
       
    }
    

    override public void setState(int aState)
    {
        base.setState(aState);

        setVisible(true);

        if (getState() == STATE_NORMAL)
        {

            initAnimation(2, 9, 10, true);
            stopMove();
            midJump = false;
        }

        else if (getState() == STATE_JUMPING)
        {
            // TODO: Arreglar la animacion.
            initAnimation(10, 17, 10, false);
            //setVelY (JUMP_SPEED);
            GRAVITY *= -1;
            setFlip(!getFlip());
            setAccelY(GRAVITY);
        }
        else if (getState() == STATE_FALLING)
        {
            gotoAndStop(17);
            stopMove();
            setAccelY(GRAVITY);
        }

        else if (getState() == STATE_DIE)
        {
            setVelXY(0, 0);
            setAccelY(0);

            setVelX(250);
            setXY(0, 600);
            GRAVITY = 900;
            setFlip(false);
            midJump = false;
            setState(STATE_NORMAL);

        }
       
    }

    private void checkPoints(int aX, int aY)
    {
        leftTileX = aX / CTileMap.TILE_WIDTH;
        upTileY = aY / CTileMap.TILE_HEIGHT;
        rightTileX = (aX + getWidth() - 1) / CTileMap.TILE_WIDTH;
        downTileY = (aY + getHeight() - 1) / CTileMap.TILE_HEIGHT;

        //Debug.Log ("Coordenada x (del mapa) a la izquierda: " + leftTileX);
        //Debug.Log ("Coordenada y (del mapa) arriba: " + upTileY);
        //Debug.Log ("Coordenada x (del mapa) a la derecha: " + rightTileX);
        //Debug.Log ("Coordenada y (del mapa) abajo: " + downTileY);

        tileTopLeft = CTileMap.inst().isWalkable(leftTileX, upTileY);
        tileTopRight = CTileMap.inst().isWalkable(rightTileX, upTileY);
        tileDownLeft = CTileMap.inst().isWalkable(leftTileX, downTileY);
        tileDownRight = CTileMap.inst().isWalkable(rightTileX, downTileY);

        //Debug.Log ("En la esquina superior izquierda hay un tile: " + tileTopLeft);
        //Debug.Log ("En la esquina superior derecha hay un tile: " + tileTopRight);
        //Debug.Log ("En la esquina inferior izquierda hay un tile: " + tileDownLeft);
        //Debug.Log ("En la esquina inferior derecha hay un tile: " + tileDownRight);
    }

    private void setOldXYPosition()
    {
        mOldX = (int)getX();
        mOldY = (int)getY();
    }
    public override void stopMove()
    {
        setVelY(0.0f);        
        setAccelXY(0.0f, 0.0f);
    }

}
