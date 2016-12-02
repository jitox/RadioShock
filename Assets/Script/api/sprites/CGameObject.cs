using UnityEngine;
using System.Collections;

public class CGameObject 
{
	private CVector mPos;
	private CVector mVel;
	private CVector mAccel;
	
	private bool mIsDead = false;
	
	private int mState = 0;
	private float mTimeState = 0.0f;
	
	private string mName;

	private int mRadius = 100;

	private int mType;

	private int mWidth = 100;
	private int mHeight = 100;

	private float mFriction = 1.0f;

	public CGameObject()
	{
		mPos = new CVector ();
		mVel = new CVector ();
		mAccel = new CVector ();
	}
	
	public void setX(float aX)
	{
		mPos.x = aX;
	}
	
	public void setY(float aY)
	{
		mPos.y = aY;
	}
	
	public void setZ(float aZ)
	{
		mPos.z = aZ;
	}

	public void setXY(float aX, float aY)
	{
		mPos.x = aX;
		mPos.y = aY;
	}
    public void setPos(CVector aPos)
    {
        mPos.x = aPos.x;
        mPos.y = aPos.y;
        mPos.z = aPos.z;
    }
	
	public float getX()
	{
		return mPos.x;
	}
	
	public float getY()
	{
		return mPos.y;
	}
	
	public float getZ()
	{
		return mPos.z;
	}
    public CVector getPos()
    {
        return mPos;
    }
	
	public void setVelX(float aVelX)
	{
		mVel.x = aVelX;
	}
	
	public void setVelY(float aVelY)
	{
		mVel.y = aVelY;
	}

	public void setVelXY(float aVelX, float aVelY)
	{
		mVel.x = aVelX;
		mVel.y = aVelY;
	}

	public void setVelZ(float aVelZ)
	{
		mVel.z = aVelZ;
	}
	
	public float getVelX()
	{
		return mVel.x;
	}
	
	public float getVelY()
	{
		return mVel.y;
	}
	
	public float getVelZ()
	{
		return mVel.z;
	}
	
	public void setAccelX(float aAccelX)
	{
		mAccel.x = aAccelX;
	}
	
	public void setAccelY(float aAccelY)
	{
		mAccel.y = aAccelY;
	}
	
	public void setAccelZ(float aAccelZ)
	{
		mAccel.z = aAccelZ;
	}

	public void setAccelXY(float aAccelX, float aAccelY)
	{
		mAccel.x = aAccelX;
		mAccel.y = aAccelY;
	}
	
	public float getAccelX()
	{
		return mAccel.x;
	}
	
	public float getAccelY()
	{
		return mAccel.y;
	}
	
	public float getAccelZ()
	{
		return mAccel.z;
	}
	
	virtual public void update()
	{
		mTimeState = mTimeState + Time.deltaTime;

		mVel = mVel + mAccel * Time.deltaTime;
		mVel = mVel * mFriction;   // mVel.mul(mFriction);
		mPos = mPos + mVel * Time.deltaTime;
	}
	
	virtual public void render()
	{
	}
	
	virtual public void destroy()
	{
		mPos.destroy ();
		mPos = null;
		mVel.destroy ();
		mVel = null;
		mAccel.destroy ();
		mAccel = null;
	}

	virtual public void setState(int aState)
	{
		mState = aState;
		mTimeState = 0.0f;
	}

	public int getState()
	{
		return mState;
	}

	public float getTimeState()
	{
		return mTimeState;
	}

	public void setDead(bool aIsDead)
	{
		mIsDead = aIsDead;
	}

	public bool isDead()
	{
		return mIsDead;
	}

	public void setRadius(int aRadius)
	{
		mRadius = aRadius;
	}

	public int getRadius()
	{
		return mRadius;
	}

	public void setType(int aType)
	{
		mType = aType;
	}

	public int getType()
	{
		return mType;
	}

	virtual public void setName(string aName)
	{
		mName = aName;
	}

	virtual public string getName()
	{
		return mName;
	}

	public void setWidth(int aWidth)
	{
		mWidth = aWidth;
	}

	public int getWidth()
	{
		return mWidth;
	}

	public void setHeight(int aHeight)
	{
		mHeight = aHeight;
	}
	
	public int getHeight()
	{
		return mHeight;
	}

	public virtual void stopMove()
	{
        
		setVelXY(0.0f, 0.0f);
		setAccelXY(0.0f, 0.0f);
	}

	public bool collides(CGameObject aGameObject)
	{
		if (CMath.dist (getX (), getY (), aGameObject.getX (), aGameObject.getY ()) < (getRadius () + aGameObject.getRadius ()))
		{
			return true;
		}
		else 
		{
			return false;
		}
	}

	public void setFriction(float aFriction)
	{
		mFriction = aFriction;
	}

	public float getFriction()
	{
		return mFriction;
	}
}