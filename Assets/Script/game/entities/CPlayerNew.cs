using UnityEngine;
using System.Collections;

public class CPlayerNew : CAnimatedSprite
{
    public const int STATE_NORMAL = 0;
    public const int STATE_JUMPING = 2;
    public const int STATE_FALLING = 3;
    public const int STATE_DIE = 4;
    private bool midJump = false;

    private bool mWin = false;

    private const int SPEED = 700;
    private int JUMP_SPEED = 900;
    //private int GRAVITY = 900;
    
    private const int PLAYER_WIDTH = 70;
    private const int PLAYER_HEIGHT = 68;

    // Variables para chequeo de colisiones con los tiles.
    private bool tileTopLeft;
    private bool tileTopRight;
    private bool tileDownLeft;
    private bool tileDownRight;

    // Variables for trigger collision.
    private int triggerTopLeft;
    private int triggerTopRight;
    private int triggerDownLeft;
    private int triggerDownRight;

    private bool triggerTopLeftActive;
    private bool triggerTopRightActive;
    private bool triggerDownLeftActive;
    private bool triggerDownRightActive;

    // Columnas y filas usadas en checkPoints().
    int leftTileX;
    int upTileY;
    int rightTileX;
    int downTileY;

    // Variables para solucionar el problema de las colisiones en diagonal.
    int mOldX;
    int mOldY;
    private bool markToClearMap = false;
    private bool firstPass;
    CTrailParticle auxTrail;

    public CPlayerNew()
    {
        //setFrames(Resources.LoadAll<Sprite>("Sprites/player"));
        firstPass = true;
        setFrames(Resources.LoadAll<Sprite>("Sprites/player/Chispita70"));
        //gotoAndStop(1);
        initAnimation(1, 8, 10, true);
        setSortingLayerName("Player");
        setName("Player");
        setVelXY(SPEED, JUMP_SPEED);
        setXY(80, 400);

        setRegistration(REG_TOP_LEFT);

        setWidth(PLAYER_WIDTH);
        setHeight(PLAYER_HEIGHT);

        setState(STATE_NORMAL);

        setOldXYPosition();
        //crateTrail(CGameConstants.COLOR_RED);
        //ChangeTrailTime(.05f);


        render();
    }

    override public void update()
    {
        if (markToClearMap)
        {
            CGame.inst().getState().passAllMap();
            markToClearMap = false;
        }
       
        if (getY() + PLAYER_HEIGHT < 0 || getY() > CTileMap.inst().MAP_HEIGHT * CTileMap.TILE_HEIGHT)
        {
            // I copied this fron the STATE_DIE so it does not wait for the animation. too lazy for a variable.
            CTriggerManager.inst().resetActive();
            JUMP_SPEED = 650;
            setVelXY(SPEED, JUMP_SPEED);
            setXY(80, 400);
            //GRAVITY = 900;
            setFlip(false);
            midJump = false;
            markToClearMap = true;
            CParticleManager.inst().killEmAll();
            setState(STATE_NORMAL);
            (CGame.inst().getState() as CLevelState).mBackground.resetPos();
        }

        if (getState() != STATE_DIE)
        {

           // auxTrail = new CTrailParticle(getX(), getY());

            checkTriggers((int)getX(), (int)getY());
            if ((triggerDownLeft == 1 && triggerDownLeftActive) || (triggerDownRight == 1 && triggerDownRightActive) || (triggerTopLeft == 1 && triggerTopLeftActive) || (triggerTopRight == 1 && triggerTopRightActive))
            {
                //GRAVITY *= -1;
                setFlip(!getFlip());
                JUMP_SPEED *= -1;
                setVelY(JUMP_SPEED);
                //setAccelY(GRAVITY);
                if (triggerDownLeft == 1)
                {
                    CTileMap.inst().setActive(leftTileX, downTileY, false);
                }
                if (triggerDownRight == 1)
                {
                    CTileMap.inst().setActive(rightTileX, downTileY, false);
                }
                if (triggerTopRight == 1)
                {
                    CTileMap.inst().setActive(rightTileX, upTileY, false);
                }
                if (triggerTopLeft == 1)
                {
                    CTileMap.inst().setActive(leftTileX, upTileY, false);
                }
            }
            else if (triggerDownLeft == 2 || triggerDownRight == 2 || triggerTopLeft == 2 || triggerTopRight == 2)
            {
                setWin(true);
            }

            // if (CTileMap.inst().getTriggerType(((int)getX()) / CTileMap.TILE_WIDTH, ((int)getY()) / CTileMap.TILE_HEIGHT) == 1)
            //{
            //    if (CTileMap.inst().isActive(((int)getX()) / CTileMap.TILE_WIDTH, ((int)getY()) / CTileMap.TILE_HEIGHT))
            //    {
                    
            //        GRAVITY = -GRAVITY;
            //        CTileMap.inst().setActive(((int)getX()) / CTileMap.TILE_WIDTH, ((int)getY()) / CTileMap.TILE_HEIGHT, false);
            //    }
            //}
        }

        switch (getState())
        {
            case STATE_NORMAL:
                //chequeo si tiene que caer
                if (!checkFloor())
                {
                    setState(STATE_FALLING);
                    
                }
                else
                {
                    if (checkFloorSpikes())
                    {
                        return;
                    }
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
                    if (checkFloorSpikes())
                    {
                        return;
                    }
                    setState(STATE_NORMAL);
                }
                jumpControl();           
                break;
            case STATE_JUMPING:
                if (checkFloor())
                {
                    if (checkFloorSpikes())
                    {
                        return;
                    }
                    setState(STATE_NORMAL);
                }
                if (checkRoof())
                {
                    if (checkRoofSpikes())
                    {
                        return;
                    }
                }

                if (checkRightWall())
                {
                    setState(STATE_DIE);
                }
                jumpControl();
                break;
            case STATE_DIE:
                if (isAnimationEnded())
                {
                    CTriggerManager.inst().resetActive();
                    JUMP_SPEED = 650;
                    setVelXY(SPEED, JUMP_SPEED);
                    setXY(80, 400);
                    //GRAVITY = 900;
                    setFlip(false);
                    midJump = false;
                    markToClearMap = true;
                    CParticleManager.inst().killEmAll();
                    setState(STATE_NORMAL);
                    (CGame.inst().getState() as CLevelState).mBackground.resetPos();
                }
                break;
            
        }
        base.update();
    }

    private bool checkFloorSpikes()
    {

        if (JUMP_SPEED > 0)
        {
            checkSpikes((int)getX(), (int)getY() + 1);
            if (tileDownLeft && tileDownRight)
            {
                setState(STATE_DIE);
                setY(getY() + CTileMap.TILE_HEIGHT/2);
                return true;
            }
        }
        else
        {
            checkSpikes((int)getX(), (int)getY() - 1);
            if (tileTopLeft && tileTopRight)
            {
                setState(STATE_DIE);
                setY(getY() - CTileMap.TILE_HEIGHT/2 );
                return true;
            }
        }
        return false;
    }

    private bool checkRoofSpikes()
    {
        if (JUMP_SPEED < 0)
        {
            checkSpikes((int)getX(), (int)getY() + 1);
            if (tileDownLeft && tileDownRight)
            {
                setState(STATE_DIE);
                return true;
            }
        }
        else
        {
            checkSpikes((int)getX(), (int)getY() - 1);
            if (tileTopLeft && tileTopRight)
            {
                setState(STATE_DIE);
                return true;
            }
        }
        return false;
    }

    private bool checkFloor()
    {
       
        if (JUMP_SPEED > 0)
        {
            checkPoints((int)getX(), (int)getY() + 1);
            if (!tileDownLeft || !tileDownRight)
            {
                setY((downTileY * CTileMap.TILE_HEIGHT) - PLAYER_HEIGHT);
                setVelY(0);
                return true;
            }
        }
        else
        {
            checkPoints((int)getX(), (int)getY() - 1);
            if (!tileTopLeft || !tileTopRight)
            {
                setY(((upTileY + 1) * CTileMap.TILE_HEIGHT));
                setVelY(0);
                return true;
            }
        }
        return false;
    }

    private bool checkRoof()
    {
        if (JUMP_SPEED < 0)
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
            if ((CKeyboard.firstPress(CKeyboard.SPACE) || CKeyboard.firstPress(CKeyboard.UP)|| CMouse.firstPress()))
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

            //initAnimation(2, 9, 10, true);
            initAnimation(1, 8, 10, true);
            stopMove();
            midJump = false;
            if (firstPass) {
                firstPass = false;
            } //else
                //CGame.inst().getCamera().initShake(true, 1f, 1f);
        }

        else if (getState() == STATE_JUMPING)
        {
            // TODO: Arreglar la animacion.
            //initAnimation(10, 17, 10, false);
            //setVelY (JUMP_SPEED);
            //GRAVITY *= -1;
            JUMP_SPEED *= -1;
            setVelY(JUMP_SPEED);
            setFlip(!getFlip());
            CTileMap.inst().ChangeColor();
            //setAccelY(GRAVITY);
        }
        else if (getState() == STATE_FALLING)
        {
           // gotoAndStop(17);
           // stopMove();
            setVelY(JUMP_SPEED);
            //setAccelY(GRAVITY);
        }

        else if (getState() == STATE_DIE)
        {
            //CGame.inst().getCamera().initShake(true, 2.3f, 2.8f);

            initAnimation(9, 18, 30, false);
            setVelXY(0, 0);
            setAccelY(0);
            (CGame.inst().getState() as CLevelState).mBackground.stopMove();
            //CTriggerManager.inst().resetActive();
            //JUMP_SPEED = 650;
            //setVelXY(SPEED, JUMP_SPEED);
            //setXY(0, 400);
            ////GRAVITY = 900;
            //setFlip(false);
            //midJump = false;
            //markToClearMap = true;
            //CParticleManager.inst().killEmAll();
            //setState(STATE_NORMAL);
            //(CGame.inst().getState() as CLevelState).mBackground.resetPos();


        }
       
    }

    private void checkTriggers(int aX, int aY)
    {
        leftTileX = aX / CTileMap.TILE_WIDTH;
        upTileY = aY / CTileMap.TILE_HEIGHT;
        rightTileX = (aX + getWidth() - 1) / CTileMap.TILE_WIDTH;
        downTileY = (aY + getHeight() - 1) / CTileMap.TILE_HEIGHT;

        triggerTopLeft = CTileMap.inst().getTriggerType(leftTileX, upTileY);
        triggerTopLeftActive = CTileMap.inst().isActive(leftTileX, upTileY);
        triggerTopRight = CTileMap.inst().getTriggerType(rightTileX, upTileY);
        triggerTopRightActive = CTileMap.inst().isActive(rightTileX, upTileY);
        triggerDownLeft = CTileMap.inst().getTriggerType(leftTileX, downTileY);
        triggerDownLeftActive = CTileMap.inst().isActive(leftTileX, downTileY);
        triggerDownRight = CTileMap.inst().getTriggerType(rightTileX, downTileY);
        triggerDownRightActive = CTileMap.inst().isActive(rightTileX, downTileY);
    }

    private void checkSpikes(int aX, int aY)
    {
        leftTileX = aX / CTileMap.TILE_WIDTH;
        upTileY = aY / CTileMap.TILE_HEIGHT;
        rightTileX = (aX + getWidth() - 1) / CTileMap.TILE_WIDTH;
        downTileY = (aY + getHeight() - 1) / CTileMap.TILE_HEIGHT;

        tileTopLeft = CTileMap.inst().isSpike(leftTileX, upTileY);
        tileTopRight = CTileMap.inst().isSpike(rightTileX, upTileY);
        tileDownLeft = CTileMap.inst().isSpike(leftTileX, downTileY);
        tileDownRight = CTileMap.inst().isSpike(rightTileX, downTileY);
        
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

    public void setWin (bool aWin)
    {
        mWin = aWin;
    }

    public bool getWin()
    {
        return mWin;
    }

}
