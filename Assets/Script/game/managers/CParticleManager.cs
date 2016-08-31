using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CParticleManager:CManager
    {

    private static CParticleManager mInst = null;
    private List<CGameObject> mArray;

    public CParticleManager()
    {
        mArray = new List<CGameObject>();
        registerSingleton();
    }

    public static CParticleManager inst()
    {
        return mInst;
    }

    public void add(CGameObject aTile)
    {
        mArray.Add(aTile);
    }
    private void registerSingleton()
    {
        if (mInst == null)
        {
            mInst = this;
        }
        else
        {
            throw new UnityException("ERROR: Cannot create another instance of singleton class CTriggerManager.");
        }
    }

    public override void update()
    {
        for (int i = mArray.Count - 1; i >= 0; i--)
        {
            mArray[i].update();
        }

        for (int i = mArray.Count - 1; i >= 0; i--)
        {
            if (mArray[i].isDead())
            {
                removeObjectWithIndex(i);
            }
        }
    }

    public override void render()
    {
        for (int i = mArray.Count - 1; i >= 0; i--)
        {
            mArray[i].render();
        }
    }

    public override void destroy()
    {
        for (int i = mArray.Count - 1; i >= 0; i--)
        {
            removeObjectWithIndex(i);
        }
        mArray = null;
    }

    private void removeObjectWithIndex(int aIndex)
    {
        if (aIndex < mArray.Count)
        {
            mArray[aIndex].destroy();
            mArray[aIndex] = null;
            mArray.RemoveAt(aIndex);
        }
    }

    public void killEmAll()
    {
        for (int i = mArray.Count - 1; i >= 0; i--)
        {
            mArray[i].setDead(true);
        }

    }
}

