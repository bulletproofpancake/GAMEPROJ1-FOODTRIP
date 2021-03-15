using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShiftLoader : SceneLoader
{
    [SerializeField] private ShiftData data;
    [SerializeField] private ShiftSchedule schedule;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        SetBackgroundImage();
    }

    public override void LoadNextScene()
    {
        ShiftManager.Instance.shift = data;
        base.LoadNextScene($"Scenes/Game Scenes/{ShiftManager.Instance.cart.Type}/Arcade");
    }

    private void SetBackgroundImage()
    {
        switch (ShiftManager.Instance.cart.Type)
        {
            case CartType.Paresan:
                SetSprites();
                break;
            case CartType.Ihawan:
                SetSprites();
                break;
            case CartType.Tusoktusok:
                SetSprites();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetSprites()
    {
        switch (schedule)
        {
            case ShiftSchedule.Morning:
                _image.sprite = ShiftManager.Instance.cart.LocSprites.Morning;
                break;
            case ShiftSchedule.Afternoon:
                _image.sprite = ShiftManager.Instance.cart.LocSprites.Afternoon;
                break;
            case ShiftSchedule.Night:
                _image.sprite = ShiftManager.Instance.cart.LocSprites.Night;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
