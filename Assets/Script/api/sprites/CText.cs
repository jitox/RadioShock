using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;


public class CText : CGameObject
{
    private GameObject mSprite;
    private Text mText;

    private Transform mTransform;
    private bool mFlip = false;

    public enum alignment
    {
        TOP_CENTER,
        CENTER,
    }


    public CText(string aText = "noText", alignment aAlign = alignment.CENTER, int aFontSize = 38, string aFontName = "Beast")
    {
        mSprite = new GameObject();
        mSprite.name = aText;

        mText = mSprite.AddComponent<Text>();

        GameObject canvas = GameObject.Find("Canvas");
        mSprite.transform.SetParent(canvas.transform);
        mSprite.transform.localScale = new Vector3(1, 1, 1);

        Font font = Resources.Load("Fonts/" + aFontName, typeof(Font)) as Font;
        mText.text = aText;
        mText.font = font;
        mText.fontSize = aFontSize;

        mText.rectTransform.sizeDelta = new Vector2(400, 400);

        mTransform = mSprite.transform;

        if (aAlign == alignment.CENTER)
        {
            mText.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            mText.alignment = TextAnchor.MiddleCenter;

        }
        else if (aAlign == alignment.TOP_CENTER)
        {
            mText.rectTransform.pivot = new Vector2(0f, 1.0f);
            mText.alignment = TextAnchor.UpperLeft;
        }
        render();
    }

    public override void update()
    {
        base.update();
    }
    public override void render()
    {
        base.render();

        Vector3 pos = new Vector3(getX(), (getY() * -1) + getZ(), 0.0f);
        mTransform.position = pos;

        if (mFlip)
        {

            mTransform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            mTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

    }
    public override void destroy()
    {
        base.destroy();
        UnityEngine.Object.Destroy(mSprite);
    }

    public void scale(float aX, float aY, float aZ = 1.0f)
    {
        mSprite.transform.localScale = new Vector3(aX, aY, aZ);
    }

    public void setVisible(bool aIsVisible)
    {
        mText.enabled = aIsVisible;
    }

    public bool isVisible()
    {
        return mText.enabled;
    }
    public void setName(string aName)
    {
        //base.setName(aName);
        mSprite.name = aName;
    }
    public string getName()
    {
        return mSprite.name;
    }

    public void setScale(float aScale)
    {
        mSprite.transform.localScale = new Vector3(aScale, aScale, 0.0f);
    }

    public void setScale(float aScaleX, float aSclaeY)
    {
        mSprite.transform.localScale = new Vector3(aScaleX, aSclaeY, 0.0f);
    }

    public void setRotation(float aRotation)
    {
        mSprite.transform.rotation = Quaternion.Euler(0, 0, aRotation);
    }
    public void setAlpha(float aAlpha)
    {
        Color color = mText.color;
        mText.color = new Color(color.r, color.g, color.b, aAlpha);
    }
    public void setColor(Color aColor)
    {
        mText.color = aColor;
    }
    public Color getColor()
    {
        return mText.color;
    }

    public void setText(string aText)
    {
        mText.text = aText;
    }

    public void setSize(float aWidth, float aHeight)
    {
        mText.rectTransform.sizeDelta = new Vector2(aWidth, aHeight);
    }
    public void setStyle(FontStyle on)
    {
        mText.fontStyle = on;
    }
    public void setHorizontalOverflow(HorizontalWrapMode aWrap)
    {
        mText.horizontalOverflow = aWrap;
    }

    public string getText()
    {
        return mText.text;
    }

}

