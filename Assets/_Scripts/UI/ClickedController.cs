using System;
using UnityEngine;

public class ClickedController : OnClicked, IUnSelect
{
    private IHasHealthbarSlider[] _healthBarSliders;

    private SpriteRenderer _renderer;

    private bool _selected;

    private void OnEnable()
    {
        EventManager.WaveCompleted += HideHealthBarSliders;
    }

    private void OnDisable()
    {
        EventManager.WaveCompleted -= HideHealthBarSliders;
    }


    private void Awake()
    {
        _healthBarSliders = GetComponents<IHasHealthbarSlider>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void HideHealthBarSliders(bool state)
    {
        if (!state) return;

        foreach (var slider in _healthBarSliders)
        {
            slider.GetHealthBar().SetActive(false);
        }
    }

    private void OnMouseEnter()
    {
        if (!_selected)
            OpenOutline();
    }

    private void OnMouseExit()
    {
        if (!_selected)
            CloseOutline();
    }

    private void OpenOutline()
    {
        _renderer.material.SetInt("_Activate", 1);
    }

    private void CloseOutline()
    {
        _renderer.material.SetInt("_Activate", 0);
    }

    public override void OnSelected()
    {
        base.OnSelected();
        foreach (var slider in _healthBarSliders)
        {
            slider.GetHealthBar().SetActive(true);
        }

        _selected = true;
    }

    public void UnSelected()
    {
        foreach (var slider in _healthBarSliders)
        {
            slider.GetHealthBar().SetActive(false);
        }
        CloseOutline();
        _selected = false;
    }
}