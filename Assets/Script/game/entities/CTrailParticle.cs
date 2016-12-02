using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class CTrailParticle:CSprite
{
    private float mScale;
    //private float  mAuxAngle = 0;
    //private float mAngleVel = 360;
    //private float mMaxHeight = 50;
    //private float mUnaffectedY;

    public CTrailParticle(float aX,float aY)
    {
        setXY(aX+20, aY+35);
        //setFrames(Resources.LoadAll<Sprite>("Sprites/trail/trail00"));
        //gotoAndStop(1);
        //setImage(Resources.Load<Sprite>("Sprites/trail/trail00"));
        setImage(Resources.Load<Sprite>("Sprites/trail/particula003"));
        
        setSortingLayerName("PlayerTrail");
        setState(0);
        setName("TrailParticle");
        CParticleManager.inst().add(this);
        mScale = 0.6f;

        //mUnaffectedY = getY();
    }
    public override void update()
    {
        base.update();
        //mUnaffectedY = getY() - Mathf.Sin(CMath.degToRad(mAuxAngle)) * mMaxHeight;
        setAlpha(getAlpha()-0.1f);
        mScale -= 0.1f;
        setScale(mScale);
        if (getAlpha() <= 0 | mScale<0)
        {
            setDead(true);
        }
        //mAuxAngle += mAngleVel * Time.deltaTime;
        //if (mAuxAngle >= 360)
        //{
        //    mAuxAngle = 0;
        //}
        //setY(mUnaffectedY + Mathf.Sin(CMath.degToRad(mAuxAngle)) * mMaxHeight);
       
    }
    public override void render()
    {
        base.render();
        
    }
}

