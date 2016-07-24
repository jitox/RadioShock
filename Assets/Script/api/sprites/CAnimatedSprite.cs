using UnityEngine;

public class CAnimatedSprite : CSprite
{
	private Sprite[] mFrame;

	private CAnim mAnim;

	public CAnimatedSprite()
	{
		mAnim = new CAnim ();
	}

	public void setFrames(Sprite[] aFramesArray)
	{
		mFrame = aFramesArray;
	}

	override public void update()
	{
		base.update ();
		mAnim.update ();
	}

	override public void render()
	{
		base.render ();

		int frame = mAnim.getCurrentFrame () - 1;

		if (frame < 0 || frame >= mFrame.Length) 
		{
			Debug.Log ("ERROR: Animation out of range: " + frame); 
		}
		else 
		{
			setImage(mFrame[frame]);			    
		}
	}

	public void gotoAndStop(int aFrame)
	{
		mAnim.gotoAndStop (aFrame);
	}

	public void gotoAndPlay(int aFrame)
	{
		mAnim.gotoAndPlay (aFrame);
	}

	public void initAnimation(int aStartFrame, int aEndFrame, int aFPS, bool aIsLoop)
	{
		mAnim.init (aStartFrame, aEndFrame, aFPS, aIsLoop);
	}
}