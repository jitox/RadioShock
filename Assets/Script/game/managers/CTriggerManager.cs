using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CTriggerManager : CManager
{
    private static CTriggerManager mInst = null;
    private List<CTile> mArray;

    public CTriggerManager()
    {
        mArray = new List<CTile>();
        registerSingleton();
    }

    public static CTriggerManager inst()
    {
        return mInst;
    }

    public void add(CTile aTile)
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
        //for (int i = mArray.Count - 1; i >= 0; i--)
        //{
        //    removeObjectWithIndex(i);
        //}
        mArray = null;
    }

    private void removeObjectWithIndex(int aIndex)
    {
        if (aIndex < mArray.Count)
        {
           // mArray[aIndex].destroy();
            mArray[aIndex] = null;
            mArray.RemoveAt(aIndex);
        }
    }

    public void resetActive()
    {
        for (int i = mArray.Count - 1; i >= 0; i--)
        {
            mArray[i].setActive(true);
        }
    }

    
}
