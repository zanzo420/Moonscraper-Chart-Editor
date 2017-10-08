﻿// Copyright (c) 2016-2017 Alexander Ong
// See LICENSE in project root for license information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note2D3DSelector : MonoBehaviour {
    public NoteController nCon;
    public NoteVisuals2DManager note2D;
    public NoteVisuals3DManager note3D;
    public Skin customSkin;

    public NoteVisualsManager currentVisualsManager
    {
        get
        {
            return note2D.gameObject.activeSelf ? note2D : (NoteVisualsManager)note3D;
        }
    }
    
    void Start()
    {
        if (AssignCustomResources.noteSpritesAvaliable != null)
        {
            switch (AssignCustomResources.noteSpritesAvaliable)
            {
                case (Skin.AssestsAvaliable.All):
                    note2D.gameObject.SetActive(true);
                    note3D.gameObject.SetActive(false);
                    enabled = false;
                    break;
                case (Skin.AssestsAvaliable.None):
                    note2D.gameObject.SetActive(false);
                    note3D.gameObject.SetActive(true);
                    enabled = false;
                    break;
                default:
                    break;
            }
        }
    }

    public void UpdateSelectedGameObject()
    {
        if (Globals.viewMode == Globals.ViewMode.Chart)
        {
            Texture2D textureInSkin = null;

            Note note = nCon.note;
            Note.Note_Type noteType = note.type;
            Note.Special_Type specialType = NoteVisualsManager.IsStarpower(note);

            int arrayPos = note.rawNote;
            if (!Globals.ghLiveMode)
            {
                if (note != null)
                {
                    if (noteType == Note.Note_Type.Strum)
                    {
                        if (specialType == Note.Special_Type.STAR_POW)
                            textureInSkin = customSkin.sp_strum[arrayPos];
                        else
                            textureInSkin = customSkin.reg_strum[arrayPos];
                    }
                    else if (noteType == Note.Note_Type.Hopo)
                    {
                        if (specialType == Note.Special_Type.STAR_POW)
                            textureInSkin = customSkin.sp_hopo[arrayPos];
                        else
                            textureInSkin = customSkin.reg_hopo[arrayPos];
                    }
                    // Tap notes
                    else
                    {
                        if (note.fret_type != Note.Fret_Type.OPEN)
                        {
                            if (specialType == Note.Special_Type.STAR_POW)
                                textureInSkin = customSkin.sp_tap[arrayPos];
                            else
                                textureInSkin = customSkin.reg_tap[arrayPos];
                        }
                    }
                }
            }

            if (textureInSkin)
            {
                note2D.gameObject.SetActive(true);
                note3D.gameObject.SetActive(false);
            }
            else
            {
                note2D.gameObject.SetActive(false);
                note3D.gameObject.SetActive(true);
            }
        }
        /*else
        {
            note2D.gameObject.SetActive(false);
            note3D.gameObject.SetActive(false);
        }*/
    }
}
