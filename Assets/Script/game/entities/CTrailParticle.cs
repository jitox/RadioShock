﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class CTrailParticle:CSprite
    {
        private float mScale;

    public CTrailParticle(float aX,float aY)
    {
        setXY(aX+20, aY+9+15);
        //setFrames(Resources.LoadAll<Sprite>("Sprites/trail/trail00"));
        //gotoAndStop(1);
        setImage(Resources.Load<Sprite>("Sprites/trail/trail00"));
        setSortingLayerName("PlayerTrail");
        setState(0);
        setName("TrailParticle");
        CParticleManager.inst().add(this);
        mScale = 1;

    }
    public override void update()
    {
        base.update();
        setAlpha(getAlpha()-0.1f);
        mScale -= 0.1f;
        setScale(mScale);
        if (getAlpha() <= 0)
        {
            setDead(true);
        }
       
    }
    public override void render()
    {
        base.render();
        
    }
}

