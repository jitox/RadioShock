using UnityEngine;
using System.Collections;

public class CShip : CAnimatedSprite
{
	private const int STATE_NORMAL = 0;

	public CShip()
	{
		setFrames (Resources.LoadAll<Sprite> ("Sprites/ship"));

		setSortingLayerName ("Player");

		setXY (400, 200);

		setRegistration(REG_CENTER);

		setRadius (32);

		setState (STATE_NORMAL);

		render ();
	}

	override public void update()
	{
		base.update ();

		if (getState () == STATE_NORMAL) 
		{
			if (CKeyboard.pressed (CKeyboard.SPACE)) 
			{
			}

			if (CKeyboard.pressed (CKeyboard.LEFT)) 
			{
			}

			if (CKeyboard.pressed (CKeyboard.RIGHT)) 
			{
				
			}
		} 
	}

	override public void render()
	{
		base.render ();
	}

	override public void destroy()
	{
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

		if (getState () == STATE_NORMAL) 
		{
			initAnimation (1, 1, 0, false);
			gotoAndStop (1);
			stopMove();
		} 
	}
}
