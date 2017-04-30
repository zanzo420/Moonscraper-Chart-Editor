﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMPool : SongObjectPool
{
    public BPMPool(GameObject parent, GameObject prefab, int initialSize) : base(parent, prefab, initialSize)
    {
        if (!prefab.GetComponentInChildren<BPMController>())
            throw new System.Exception("No BPMController attached to prefab");
    }

    protected override void Assign(SongObjectController sCon, SongObject songObject)
    {
        BPMController controller = sCon as BPMController;

        // Assign pooled objects
        controller.bpm = (BPM)songObject;
        controller.gameObject.SetActive(true);
    }

    public void Activate(BPM[] range)
    {
        base.Activate(range);
    }
}