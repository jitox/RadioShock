using UnityEngine;
using System.Collections;

// TODO: Sacar el mAsteroidSprite.
public class CAsteroid : CGenericEnemy
{
	private CSprite mAsteroidSprite;

	public const int ASTEROID_BIG = 0;
	public const int ASTEROID_MEDIUM = 1;
	public const int ASTEROID_SMALL = 2;

	private float mRotVel = CMath.randomFloatBetween(-10.0f, 10.0f);

	public CAsteroid(int aType)
	{
		mAsteroidSprite = new CSprite ();
		string name = "";

		setType (aType);

		if (getType () == ASTEROID_BIG) 
		{
			name = "Sprites/asteroid1_big";
			setRadius(32);
		}
		else if (getType () == ASTEROID_MEDIUM) 
		{
			name = "Sprites/asteroid1_medium";
			setRadius(16);
		}
		else if (getType () == ASTEROID_SMALL) 
		{
			name = "Sprites/asteroid1_small";
			setRadius(8);
		}

		mAsteroidSprite.setImage (Resources.Load<Sprite>(name));

		mAsteroidSprite.setName ("Asteroid");
		mAsteroidSprite.setSortingLayerName ("Enemies");

		render ();
	}
	
	override public void update()
	{
		base.update ();
		mAsteroidSprite.update ();

		mAsteroidSprite.setRotation (mAsteroidSprite.getRotation () + mRotVel);

		if (getX () > CTileMap.WORLD_WIDTH - getRadius ()) 
		{
			setX (CTileMap.WORLD_WIDTH - getRadius ());
			setVelX(getVelX() * -1);
		}

		if (getY () > CTileMap.WORLD_HEIGHT - getRadius ()) 
		{
			setY (CTileMap.WORLD_HEIGHT - getRadius ());
			setVelY(getVelY() * -1);
		}

		if (getX () < getRadius ()) 
		{
			setX (getRadius ());
			setVelX(getVelX() * -1);
		}

		if (getY () < getRadius ()) 
		{
			setY (getRadius ());
			setVelY(getVelY() * -1);
		}
	}
	
	override public void render()
	{
		base.render ();
		
		mAsteroidSprite.setX (getX ());
		mAsteroidSprite.setY (getY ());
		
		mAsteroidSprite.render ();
	}
	
	override public void destroy()
	{
		base.destroy ();	
		mAsteroidSprite.destroy ();
		mAsteroidSprite = null;
	}
}