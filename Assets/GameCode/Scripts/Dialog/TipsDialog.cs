﻿using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class TipsDialogContext : DialogContext
{
    public string tipsContext;
    public bool withBg;
    public bool isWarning;
    public int offsetY=312;

    public TipsDialogContext(string tipsContext, bool withBg, bool isWarning, int offsetY = 312)
    {
        this.tipsContext = tipsContext;
        this.withBg = withBg;
        this.isWarning = isWarning;
        this.offsetY = offsetY;
    }
}

public class TipsDialog : Dialog<TipsDialogContext>
{
    [SerializeField] private Text tipsTxt;
    [SerializeField] private Text withoutBgTxt;

    public static void ShowDialog(string tips,Action onClose=null, bool withBg = true, bool isWarning = false, int offsetY = 312)
    {
        var dialog = GetShowingDialog(nameof(TipsDialog)) as TipsDialog;
        if (dialog != null)
        {
            var txtComp = dialog.tipsTxt;
            
            if (dialog.tipsTxt.text == tips)
            {
                return;
            }

            
            DialogUtil.ShowDialogWithContext(nameof(TipsDialog),
                new TipsDialogContext(tips, withBg, isWarning, dialog.dialogContext.offsetY-65), null, onClose);
            
        }
        else
        {
            DialogUtil.ShowDialogWithContext(nameof(TipsDialog), new TipsDialogContext(tips, withBg, isWarning, offsetY),null,onClose);
        }

    }

    public override void Show()
    {
        base.Show();
        
        
        frameImage.gameObject.SetActive(dialogContext.withBg);
        withoutBgTxt.gameObject.SetActive(!dialogContext.withBg);
        withoutBgTxt.text = dialogContext.tipsContext;
        withoutBgTxt.color = dialogContext.isWarning ? Color.red : Color.white;
        
        tipsTxt.text = dialogContext.tipsContext;

        frameImage.transform.localPosition = new Vector2(0,dialogContext.offsetY);
        
        //tipsTxt.transform.DOLocalJump(tipsTxt.transform.localPosition + Vector3.up * 100, 5, 2, 2);
        //withoutBgTxt.transform.DOLocalJump(withoutBgTxt.transform.localPosition + Vector3.up * 100, 5, 2, 2);
        frameImage.transform.DOLocalJump(frameImage.transform.localPosition + Vector3.up * 100, 5, 2, 2);
        
        Timer.Register(2f, Close);
    }

   
}