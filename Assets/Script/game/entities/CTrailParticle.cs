using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class CTrailParticle:CSprite
    {
        private float mScale;

    public CTrailParticle(float aX,float aY)
    {
        //setXY(aX+20, aY+9+15);
        setXY(aX+50, aY+50);
        //setFrames(Resources.LoadAll<Sprite>("Sprites/trail/trail00"));
        //gotoAndStop(1);
        setImage(Resources.Load<Sprite>("Sprites/trail/particula001"));
        setSortingLayerName("PlayerTrail");
        setState(0);
        setName("TrailParticle");
        CParticleManager.inst().add(this);
        setScale(0.5f);
        mScale = 0.5f;
        setAlpha(1.5f);
        setVelX(-50);

    }
    public override void update()
    {
        base.update();
        //setAlpha(getAlpha()-0.1f);
        mScale -= 0.05f;
        setScale(mScale);
        if (mScale <= 0)
        {
            setDead(true);
        }
       
    }
    public override void render()
    {
        base.render();
        
    }
}

