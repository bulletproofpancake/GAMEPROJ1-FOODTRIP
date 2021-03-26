using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JK_Cart_Loader:SceneLoader
{
    [SerializeField] private ShiftSchedule schedule;
    [SerializeField] private ShiftData shiftPares, shiftTusok;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        SetBackgroundImage();
    }

    public void SelectShift()
    {
        SetShift();
        base.LoadNextScene($"Scenes/Game Scenes/{ShiftManager.Instance.cart.Type}/Arcade");

        FindObjectOfType<AudioManager>().Play("ArcadeBGM");
        LeanTween.scale(gameObject, new Vector3(.7f, .7f, .7f), 1f).setEase(LeanTweenType.punch);
    }

    private void SetBackgroundImage()
    {
        switch (ShiftManager.Instance.cart.Type)
        {
            case CartType.Paresan:
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

    private void SetShift()
    {
        switch (ShiftManager.Instance.cart.Type)
        {
            case CartType.Paresan:
                ShiftManager.Instance.shift = shiftPares;
                break;
            case CartType.Tusoktusok:
                ShiftManager.Instance.shift = shiftTusok;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
