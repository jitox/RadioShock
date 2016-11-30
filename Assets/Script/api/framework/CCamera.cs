using UnityEngine;
using System.Collections;

public class CCamera : CGameObject
{
	public const int WIDTH = CGameConstants.SCREEN_WIDTH;
	public const int HEIGHT = CGameConstants.SCREEN_HEIGHT;

    private float mTimeShake;
    private float mTimer;
    private float mShakeForce;
    private float mShakeDistX;
    private float mShakeDistY;
    private bool mShake = false;
    private Vector2 mObjective;
    private bool onBorder = false;

    public const int SPEED = 60;

	private CGameObject mGameObjectToFollow;

	public CCamera()
	{
	}

	override public void update()
	{
		base.update ();

		if (mGameObjectToFollow != null) 
		{
            followObject();
			//setX (mGameObjectToFollow.getX () - WIDTH / 2);
			//setY (mGameObjectToFollow.getY () - HEIGHT / 2);
		} 
		else 
		{
            // Mover la camara con el teclado.
            if (isShaking())
            {
                mShakeDistX = shake();
                mShakeDistY = shake();
            }
            else
            {
                mShakeDistX = 0;
                mShakeDistY = 0;
            }

            setX(mObjective.x + mShakeDistX);
            setY(mObjective.y + mShakeDistY);
        }

		// Chequear que la camara no se vaya de los bordes.
		checkBorder();
	}

	private void checkBorder()
	{
		if (getX () <= 0) {
			setX (0);
		}

		if (getY () <= 0) {
			setY (0);
		}

		if (getX () >= CTileMap.inst().WORLD_WIDTH - WIDTH) {
			setX (CTileMap.inst().WORLD_WIDTH - WIDTH);
            onBorder = true;
		}

		if (getY () >= CTileMap.inst().WORLD_HEIGHT - HEIGHT) {
			setY (CTileMap.inst().WORLD_HEIGHT - HEIGHT);
		}
	}

	override public void render()
	{
		base.render ();
	}

	override public void destroy()
	{
		base.destroy ();
	}

	public void setGameObjectToFollow(CGameObject aGameObjectToFollow)
	{
		mGameObjectToFollow = aGameObjectToFollow;
	}

	public CGameObject getGameObjectToFollow()
	{
		return mGameObjectToFollow;
	}
    private void SetShake(bool aShake)
    {
        mShake = aShake;
        if (!aShake)
        {
            mTimeShake = 0;
        }
    }

    private bool isShaking()
    {
        return mShake;
    }

    public void initShake(bool aBoolean, float aTimeShake, float aShakeForce)
    {
        SetShake(aBoolean);
        mTimeShake = aTimeShake;
        mShakeForce = aShakeForce;
        mTimer = 0;
    }

    public void stopShake()
    {
        SetShake(false);
    }

    public float shake()
    {
        mTimer = mTimer + Time.deltaTime;

        if (mTimer < mTimeShake)
        {
            return CMath.randomFloatBetween(-mShakeForce, mShakeForce);
            
        }
        else
        {
            SetShake(false);
        }
        return 0;
    }

    public void followObject()
    {
        if (isShaking())
        {
            mShakeDistX = shake();
            mShakeDistY = shake();
        }
        else
        {
            mShakeDistX = 0;
            mShakeDistY = 0;
        }
        
        setX(mGameObjectToFollow.getX()-300 - getWidth() / 2 + mShakeDistX);
        setY( getY() + mShakeDistY);
        //setY(mGameObjectToFollow.getY() - getHeight() / 2 + mShakeDistY);
    }
    public bool getOnborder()
    {
        return onBorder;
    }
}

