using UnityEngine;

public class SliderOpeningController : MonoBehaviour,IUnSelect
{
    private IHasHealthbarSlider[] _healthBarSliders;

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
    }

    private void HideHealthBarSliders(bool state)
    {
        if(!state) return;

        foreach (var slider in _healthBarSliders)
        {
            slider.GetHealthBar().SetActive(false);
        }
    }

    public void OnSelected()
    {
        foreach (var slider in _healthBarSliders)
        {
            slider.GetHealthBar().SetActive(true);
        }
    }

    public void UnSelected()
    {
        foreach (var slider in _healthBarSliders)
        {
            slider.GetHealthBar().SetActive(false);
        }
    }
}