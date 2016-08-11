using UnityEngine;
using System.Collections;

public class CCannon : CGenericEnemy
{
	private CGameObject mGameObjectToFollow = CGame.inst().getShip(); //CGame.inst().getPlayer();

	public const int SPEED = 240;

	public CCannon(int aTileX, int aTileY)
	{
		setFrames (Resources.LoadAll<Sprite>("Sprites/enemies/cannon"));
		setName ("Cannon");
		setSortingLayerName ("Enemies");
		setRadius(32);
		gotoAndStop (1);

		setX (aTileX * CTileMap.TILE_WIDTH + CTileMap.TILE_WIDTH / 2);
		setY (aTileY * CTileMap.TILE_HEIGHT + CTileMap.TILE_HEIGHT / 2);

		render ();
	}
	
	override public void update()
	{
		base.update ();

		lookAt (mGameObjectToFollow);

		int r = CMath.randomIntBetween (0, 1000);
		if (r > 990)
		{
			fire ();
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

	private void lookAt(CGameObject aGameObjectToFollow)
	{
		float diffX = aGameObjectToFollow.getX () - getX ();
		float diffY = aGameObjectToFollow.getY () - getY ();
		diffY *= -1;
		float angle = Mathf.Atan2 (diffY, diffX);

		setRotation (CMath.radToDeg(angle));
	}

	// TODO: ESTA COMENTADA LA COLISION.
	// TODO: Usar CEnemyBullet.
	public void fire()
	{
		CPlayerBullet b = new CPlayerBullet();

		float angle = CMath.degToRad(getRotation ());
		angle *= -1;

		float dx = getRadius () * Mathf.Cos (angle);
		float dy = getRadius () * Mathf.Sin (angle);

		b.setX (getX () + dx);
		b.setY (getY () + dy);

		b.setVelX (SPEED * Mathf.Cos(angle));
		b.setVelY (SPEED * Mathf.Sin(angle));

		CBulletManager.inst().add(b);
	}
}