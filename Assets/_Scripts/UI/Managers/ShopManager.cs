﻿using UI.Base;
using UI.Controllers;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : ScreenManager
{
    public static ShopManager instance;

    public Transform itemHolder;

    private List<Item> unlockedItems = new List<Item>();

    protected override void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;

        screenState = MenuState.Shop;
    }

    private void Start()
    {
        foreach (Item item in ItemManager.instance.GetItemArray().Where(item => item.Unlocked == true))
        {
            unlockedItems.Add(item);
        }
    }

    protected override void StartScreen()
    {
        DoStartupAnimation();
    }

    private void DoStartupAnimation()
    {
        int index = 0;

        foreach (Item item in unlockedItems)
        {
            Sequence s = DOTween.Sequence();

            GameObject g = Instantiate(Resources.Load<GameObject>("btn_ItemHolder"));
            Button btn = g.GetComponent<Button>();
            Image img = g.GetComponent<Image>();
            Text txt = g.GetComponentInChildren<Text>();

            RectTransform rect = g.GetComponent<RectTransform>();

            txt.text = item.Key;
            g.transform.SetParent(itemHolder);

            rect.localScale = new Vector3(1, 1, 1);
            rect.anchoredPosition = new Vector2(0, -350);

            btn.onClick.AddListener(() =>
            {
                ItemManager.instance.SetItemSelected(item.Key);
            });

            s.Append(img.DOFade(1, 0.2f));
            s.Join(rect.DOAnchorPos(new Vector2(0, 750 + (index * -325)), 1f / unlockedItems.Count));

            index++;
        }
    }

    protected override void StopScreen()
    {
        foreach (Transform child in itemHolder)
        {
            Destroy(child.gameObject);
        }
    }
}