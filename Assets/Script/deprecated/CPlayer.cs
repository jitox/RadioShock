using UnityEngine;
using System.Collections;

public class CPlayer : CAnimatedSprite
{
	private const int STATE_STAND = 0;
	private const int STATE_WALKING = 1;
	private const int STATE_JUMPING = 2;
	private const int STATE_FALLING = 3;
    private const int STATE_DIE = 4;
    private bool midJump = false;


	private const int SPEED = 240;
	private const int JUMP_SPEED = -500;
	private  int GRAVITY = 900;

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

	public CPlayer()
	{
		setFrames (Resources.LoadAll<Sprite> ("Sprites/player"));

		setSortingLayerName ("Player");
        setVelX(250);
		setXY (0, 600);

		setRegistration(REG_TOP_LEFT);

		setWidth (PLAYER_WIDTH);
		setHeight (PLAYER_HEIGHT);

		setState (STATE_STAND);

		setOldXYPosition();

        /*



		mShipSprite = new CAnimatedSprite ();
		mShipSprite.setFrames (Resources.LoadAll<Sprite>("Sprites/ship"));
		mShipSprite.gotoAndStop (1);
					// mShipSprite.initAnimation (1, 3, 15, true);
		setX (CGameConstants.SCREEN_WIDTH / 2);
		setY (CGameConstants.SCREEN_HEIGHT - 100);

		mShipSprite.setName ("Player");
		mShipSprite.setSortingLayerName ("Player");

		setState(STATE_MOVE);*/
       
        render ();
	}

	override public void update()
	{
		

		if (getState () == STATE_STAND) 
		{
			
            if (GRAVITY > 0)
            {
                checkPoints((int)getX(), (int)getY() + 1);
                if (tileDownLeft && tileDownRight)
                {
                    setState(STATE_FALLING);
                    return;
                }
            }else
            {
                checkPoints((int)getX(), (int)getY() - 1);
                if (tileTopLeft && tileTopRight)
                {
                    
                    setState(STATE_FALLING);
                    return;
                }
            }

            // Si no hay pared a la derecha, caminamos.
            checkPoints((int)getX() + (int)(SPEED * Time.deltaTime), (int)getY());
            if (tileTopRight && tileDownRight)
            {
                
                setVelX(SPEED);
            }
            else
            {
                // Corregir contra el tile con el que choca.
                setX((rightTileX * CTileMap.TILE_WIDTH) - PLAYER_WIDTH);

                setState(STATE_DIE);
                return;
            }

            if (CKeyboard.pressed (CKeyboard.SPACE) || CKeyboard.pressed (CKeyboard.UP)) {
				setState (STATE_JUMPING);
                
                
                return;
			}

			if (CKeyboard.pressed (CKeyboard.LEFT)) {
				// Si no hay pared a la izquierda puedo caminar.
				checkPoints ((int)getX () - 1, (int)getY ());
				if (tileTopLeft && tileDownLeft) {
					setState (STATE_WALKING);
					return;
				}
			}

			if (CKeyboard.pressed (CKeyboard.RIGHT)) {
				// Si no hay pared a la derecha puedo caminar.
				checkPoints ((int)getX () + 1, (int)getY ());
				if (tileTopRight && tileDownRight) {
					setState (STATE_WALKING);
					return;
				}
			}
		} else if (getState () == STATE_WALKING) 
		{
			if (CKeyboard.pressed (CKeyboard.SPACE) || CKeyboard.pressed (CKeyboard.UP)) {
				setState (STATE_JUMPING);
				return;
			}

			checkPoints ((int)getX (), (int)getY () + 1);
			if (tileDownLeft && tileDownRight) 
			{
				setState(STATE_FALLING);
				return;
			}

			if (!CKeyboard.pressed (CKeyboard.LEFT) && !CKeyboard.pressed (CKeyboard.RIGHT)) {
				setState (STATE_STAND);
				return;
			} else {
				if (CKeyboard.pressed (CKeyboard.LEFT)) {
					// Si no hay una pared a la izquierda, caminamos.
					checkPoints ((int)getX () - (int)(SPEED * Time.deltaTime), (int)getY ());
					if (tileTopLeft && tileDownLeft) {
						setFlip (true);
						setVelX (-SPEED);
					} else {
						// Corregir contra el tile con el que choca.
						setX ((leftTileX + 1) * CTileMap.TILE_WIDTH);
 
						setState (STATE_STAND);
						return;
					}
				} else if (CKeyboard.pressed (CKeyboard.RIGHT)) {
					// Si no hay pared a la derecha, caminamos.
					checkPoints ((int)getX () + (int)(SPEED * Time.deltaTime), (int)getY ());
					if (tileTopRight && tileDownRight) {
						setFlip (false);
						setVelX (SPEED);
					} else {
						// Corregir contra el tile con el que choca.
						setX ((rightTileX * CTileMap.TILE_WIDTH) - PLAYER_WIDTH);

						setState (STATE_STAND);
						return;
					}
				}
			}
		} 

		base.update ();

		if (getState () == STATE_JUMPING) 
		{
			controlMoveHorizontal();
            // Si no hay pared a la derecha, caminamos.
            checkPoints((int)getX() + (int)(SPEED * Time.deltaTime), (int)getY());
            if (tileTopRight && tileDownRight)
            {

                setVelX(SPEED);
            }
            else
            {
                // Corregir contra el tile con el que choca.
                setX((rightTileX * CTileMap.TILE_WIDTH) - PLAYER_WIDTH);

                setState(STATE_DIE);
                return;
            }

            if (checkFloor())
				return;


            if (GRAVITY > 0)
            {
                checkPoints((int)getX(), (int)getY() - 1);
                if (!tileTopLeft || !tileTopRight)
                {
                    setY((upTileY + 1) * CTileMap.TILE_HEIGHT +1);
                    setVelY(0);
                    return;
                }
            }
            else
            {
                checkPoints((int)getX(), (int)getY() + 1);
                if (!tileDownLeft || !tileDownRight)
                {
                    setY((downTileY ) * CTileMap.TILE_HEIGHT - PLAYER_HEIGHT -1);
                    setVelY(0);
                    return;
                }
            }


            if (!midJump)
            {
                if (CKeyboard.firstPress(CKeyboard.SPACE) || CKeyboard.firstPress(CKeyboard.UP))
                {
                    setState(STATE_JUMPING);
                    midJump = true;
                    return;
                }
            }
        }
		else if (getState () == STATE_FALLING) 
		{
            if (!midJump)
            {
                if (CKeyboard.pressed(CKeyboard.SPACE) || CKeyboard.pressed(CKeyboard.UP))
                {
                    setState(STATE_JUMPING);
                    midJump = true;
                    return;
                }
            }
          

            controlMoveHorizontal();

			if (checkFloor())
				return;
		}
			
		setOldXYPosition();



     
	}

	private bool checkFloor()
	{
		
        if (GRAVITY > 0)
        {
            checkPoints((int)getX(), (int)getY() + 1);
            if (!tileDownLeft || !tileDownRight)
            {
                setY((downTileY * CTileMap.TILE_HEIGHT) - PLAYER_HEIGHT);
                setState(STATE_STAND);
                return true;
            }
        }else
        {
            checkPoints((int)getX(), (int)getY() - 1);
            if (!tileTopLeft || !tileTopRight)
            {
                setY(((upTileY +1) * CTileMap.TILE_HEIGHT) );
                setState(STATE_STAND);
                return true;
            }
        }
		

		return false;
	}

	// Se invoca cuando estamos saltando y cayendo, para poder movernos en el aire.
	private void controlMoveHorizontal()
	{
		checkPoints ((int)getX (), (int) mOldY);
		if (!tileTopLeft || !tileDownLeft) {
			setX ((leftTileX + 1) * CTileMap.TILE_WIDTH);
			//setVelX (0);
		} else {
			checkPoints ((int)getX (), (int) mOldY );
			if (!tileTopRight || !tileDownRight) {
				setX ((rightTileX * CTileMap.TILE_WIDTH) - PLAYER_WIDTH);
				//setVelX (0);
			}
		}



		if (CKeyboard.pressed (CKeyboard.LEFT)) {
			// Si no hay una pared a la izquierda, caminamos.
			checkPoints ((int)getX () - (int)(SPEED * Time.deltaTime), (int) mOldY/*(int)getY()*/);
			if (tileTopLeft && tileDownLeft) {
				setFlip (true);
				setVelX (-SPEED);
			} else {
				// Corregir contra el tile con el que choca.
				setX ((leftTileX + 1) * CTileMap.TILE_WIDTH);
			}
		} else if (CKeyboard.pressed (CKeyboard.RIGHT)) {
			// Si no hay pared a la derecha, caminamos.
			checkPoints ((int)getX () + (int)(SPEED * Time.deltaTime), (int) mOldY/*(int)getY()*/);
			if (tileTopRight && tileDownRight) {
				setFlip (false);
				setVelX (SPEED);
			} else {
				// Corregir contra el tile con el que choca.
				setX ((rightTileX * CTileMap.TILE_WIDTH) - PLAYER_WIDTH);
			}
		} else 
		{
			//setVelX (0);
		}	
	}

	override public void render()
	{
		base.render ();
	}
	
	override public void destroy()
	{
		/*base.destroy ();	
		mShipSprite.destroy ();
		mShipSprite = null;*/
	}

	public void fire()
	{
		/*CPlayerBullet b = new CPlayerBullet();
		b.setX(getX());
		b.setY (getY () - 50);  // TODO:// - getRadius());
		b.setVelX(0);
		b.setVelY(CPlayerBullet.SPEED);
		b.setAccelX(0);
		b.setAccelY(0);
		CBulletManager.inst().add(b);*/
	}

	override public void setState(int aState)
	{
		base.setState (aState);

		setVisible (true);

		if (getState () == STATE_STAND) 
		{
			//initAnimation (1, 1, 0, false);
            initAnimation(2, 9, 10, true);
            //gotoAndStop (1);
			stopMove();
            midJump = false;
		} 
		else if (getState () == STATE_WALKING) 
		{
			initAnimation (2, 9, 10, true);
		}
		else if (getState () == STATE_JUMPING) 
		{
			// TODO: Arreglar la animacion.
			initAnimation (10, 17, 10, false);
			//setVelY (JUMP_SPEED);
            GRAVITY *= -1;
            setFlip(!getFlip());
			setAccelY (GRAVITY);
           
		} 
		else if (getState () == STATE_FALLING) 
		{
			gotoAndStop(17);
			stopMove();
			setAccelY (GRAVITY);

		} else if (getState() == STATE_DIE)
        {
            setVelXY(0, 0);
            setAccelY(0);
            
            setVelX(250);
            setXY(0, 600);
            GRAVITY = 900;
            setFlip(false);
            midJump = false;
            setState(STATE_STAND);
            

        }
	}

	private void checkPoints(int aX, int aY)
	{
		leftTileX = aX / CTileMap.TILE_WIDTH;
		upTileY = aY / CTileMap.TILE_HEIGHT;
		rightTileX = (aX + getWidth () - 1) / CTileMap.TILE_WIDTH;
		downTileY = (aY + getHeight () - 1) / CTileMap.TILE_HEIGHT;

		//Debug.Log ("Coordenada x (del mapa) a la izquierda: " + leftTileX);
		//Debug.Log ("Coordenada y (del mapa) arriba: " + upTileY);
		//Debug.Log ("Coordenada x (del mapa) a la derecha: " + rightTileX);
		//Debug.Log ("Coordenada y (del mapa) abajo: " + downTileY);

		tileTopLeft = CTileMap.inst().isWalkable(leftTileX, upTileY);
		tileTopRight = CTileMap.inst ().isWalkable (rightTileX, upTileY);
		tileDownLeft = CTileMap.inst ().isWalkable (leftTileX, downTileY);
		tileDownRight = CTileMap.inst ().isWalkable (rightTileX, downTileY);

		//Debug.Log ("En la esquina superior izquierda hay un tile: " + tileTopLeft);
		//Debug.Log ("En la esquina superior derecha hay un tile: " + tileTopRight);
		//Debug.Log ("En la esquina inferior izquierda hay un tile: " + tileDownLeft);
		//Debug.Log ("En la esquina inferior derecha hay un tile: " + tileDownRight);
	}

	private void setOldXYPosition()
	{
		mOldX = (int) getX();
		mOldY = (int) getY();
	}
}
