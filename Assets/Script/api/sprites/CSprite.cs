using UnityEngine;
using System.Collections;

public class CSprite : CGameObject 
{
	private GameObject mSprite;
	private SpriteRenderer mSpriteRenderer;
    public TrailRenderer mTrailRenderer;

    // Caching of mSprite.transform.
    private Transform mTransform;

	private bool mFlip = false;
	private float mRotation = 0.0f;
	private bool mIsRotatingSprite = false;

	public const int REG_CENTER = 0;
	public const int REG_TOP_LEFT = 1;
	private int mRegistration = REG_CENTER;

	public CSprite()
	{
		mSprite = new GameObject ();
		mSpriteRenderer = mSprite.AddComponent<SpriteRenderer> ();

		mTransform = mSprite.transform;
	}

	override public void update()
	{
		base.update ();


	}

	override public void render()
	{
		base.render ();

		//int offsetX = 0;
        int offsetY = 0;
		if (mRegistration == REG_TOP_LEFT) 
		{
			if (mFlip) 
			{
                //offsetX = getWidth ();
                offsetY = getHeight();
			}
		}

		float x = getX();
        float y = getY();

        CCamera cam = CGame.inst().getCamera();
        if (cam != null)
        {
            x -= cam.getX();
            y -= cam.getY();
            //y = getY() - CGame.inst().getCamera().getY();
        }

        Vector3 pos = new Vector3 (x , (y +offsetY)* -1, 0.0f);
		mTransform.position = pos;

		if (!mIsRotatingSprite) 
		{
			if (mFlip) 
			{
				mTransform.rotation = Quaternion.Euler (new Vector3 (180, 0, mRotation));
			} 
			else 
			{
				mTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, mRotation));
			}
		} 
		else 
		{
			mTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, mRotation));
		}
	}

	override public void destroy()
	{
		base.destroy ();
		Object.Destroy (mSprite);
	}

	public void setImage(Sprite aSprite)
	{
		mSpriteRenderer.sprite = aSprite;
	}

	public void setFlip(bool aFlip)
	{
		mFlip = aFlip;
	}

	public bool getFlip()
	{
		return mFlip;
	}

	public void setRotation(float aRotation)
	{
		mIsRotatingSprite = true;
		mRotation = aRotation;
		mRotation = CMath.clampDeg (mRotation);
	}

	public float getRotation()
	{
		return mRotation;
	}

	override public void setName(string aName)
	{
		base.setName (aName);
		mSprite.name = aName;
	}

	public void setSortingLayerName(string aSortingLayerName)
	{
		mSpriteRenderer.sortingLayerName = aSortingLayerName;
	}

	public string getSortingLayerName()
	{
		return mSpriteRenderer.sortingLayerName;
	}

	public void setSortingOrder(int aSortingOrder)
	{
		mSpriteRenderer.sortingOrder = aSortingOrder;
	}

	public int getSortingOrder()
	{
		return mSpriteRenderer.sortingOrder;
	}

	public void setColor(Color aColor)
	{
		mSpriteRenderer.material.color = aColor;
	}

	public Color getColor()
	{
		return mSpriteRenderer.material.color;
	}

	public void setAlpha(float aAlpha)
	{
		Color color = mSpriteRenderer.material.color;
		mSpriteRenderer.material.color = new Color (color.r, color.g, color.b, aAlpha);
	}

	public float getAlpha()
	{
		Color color = mSpriteRenderer.material.color;
		return color.a;
	}

	public void setVisible(bool aIsVisible)
	{
		mSpriteRenderer.enabled = aIsVisible;
	}

	public bool isVisible()
	{
		return mSpriteRenderer.enabled;
	}

	public void setScale(float aScale)
	{
		mSprite.transform.localScale = new Vector3 (aScale, aScale, 0.0f);
	}

	public void setRegistration(int aRegistration)
	{
		mRegistration = aRegistration;
	}

	public int getRegistration()
	{
		return mRegistration;
	}

    public void crateTrail(int aColor)
    {
        GameObject trailObj = new GameObject("Trail");
        trailObj.transform.SetParent(mSprite.transform);
        trailObj.transform.position = Vector3.zero;
        mTrailRenderer = trailObj.AddComponent<TrailRenderer>();
        //------------------------------------------------
        Color color = Color.white;
        //		if (aColor == CGameConstants.COLOR_BLACK)
        //		{
        //			mTrailRenderer.material = Resources.Load<Material> ("Materials/TrailMaterials/BlackMaterial");
        //		}
        if (aColor == CGameConstants.COLOR_BLUE)
        {
            mTrailRenderer.material = Resources.Load<Material>("Materials/TrailMaterials/BlueMaterial");
        }
        //		else if (aColor == CGameConstants.COLOR_GREEN) 
        //		{
        //			mTrailRenderer.material = Resources.Load<Material> ("Materials/TrailMaterials/GreenMaterial");
        //		}
        //		else if (aColor == CGameConstants.COLOR_ORANGE) 
        //		{
        //			mTrailRenderer.material = Resources.Load<Material> ("Materials/TrailMaterials/OrangeMaterial");
        //		}
        else if (aColor == CGameConstants.COLOR_RED)
        {
            mTrailRenderer.material = Resources.Load<Material>("Materials/TrailMaterials/RedMaterial");
        }
        //		else if (aColor == CGameConstants.COLOR_PINK) 
        //		{
        //			mTrailRenderer.material = Resources.Load<Material> ("Materials/TrailMaterials/PinkMaterial");
        //		}
        //		else if (aColor == CGameConstants.COLOR_WHITE) 
        //		{
        //			mTrailRenderer.material = Resources.Load<Material> ("Materials/TrailMaterials/WhiteMaterial");
        //		}
        ////		else if (aColor == CGameConstants.COLOR_PURPLE) 
        ////		{
        ////			mTrailRenderer.material = Resources.Load<Material> ("Materials/TrailMaterials/PurpleMaterial");
        ////		}
        //		else if (aColor == CGameConstants.COLOR_YELLOW) 
        //		{
        //			mTrailRenderer.material = Resources.Load<Material> ("Materials/TrailMaterials/YellowMaterial");
        //		}
        //_--------------------------------------------------


        //			mTrailRenderer.material.SetColor("_TintColor",Color.blue);
        //			SerializedObject so = new SerializedObject (mTrailRenderer);
        //			so.FindProperty("m_Colors.m_Color[0]").colorValue = color;
        //			so.FindProperty("m_Colors.m_Color[1]").colorValue = color;
        //			so.FindProperty("m_Colors.m_Color[2]").colorValue = color;
        //			so.FindProperty("m_Colors.m_Color[3]").colorValue = color;
        //			so.FindProperty("m_Colors.m_Color[4]").colorValue = color;
        //			so.ApplyModifiedProperties();

        mTrailRenderer.startWidth = 40;
        mTrailRenderer.endWidth = 10;
        mTrailRenderer.time = 0.5f;
        mTrailRenderer.sortingLayerName = "Player";
    }

    public void ChangeTrailMat(Material aMaterial)
    {
        mTrailRenderer.material = aMaterial;
    }

    public void ChangeTrailStarWidth(float aValue)
    {
        mTrailRenderer.startWidth = aValue;
    }

    public void ChangeTrailEndWidth(float aValue)
    {
        mTrailRenderer.endWidth = aValue;
    }

    public void ChangeTrailTime(float aValue)
    {
        mTrailRenderer.time = aValue;
    }

    public void HideTrailRenderer(bool aValue)
    {
        mTrailRenderer.enabled = aValue;
    }
}
