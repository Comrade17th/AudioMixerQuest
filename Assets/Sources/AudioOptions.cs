using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioOptions : MonoBehaviour
{
    private readonly float _scaleModifier = 20f;
    private readonly float _minVolume = 0.0001f;
    private readonly float _maxVolume = 1f;
    
    private readonly string _masterVolume = "MasterVolume";
    private readonly string _musicVolume = "MusicVolume";
    private readonly string _effectsVolume = "SoundEffectsVolume";
    
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _effectsSlider;
    
    [SerializeField] private Button _musicButton;
    [SerializeField] private AudioMixer _mixer;
    
    private bool _soundEnabled;

    private void OnEnable()
    {
        _masterSlider.onValueChanged.AddListener(ChangeMasterVolume);
        _musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        _effectsSlider.onValueChanged.AddListener(ChangeEffectsVolume);
        
        _musicButton.onClick.AddListener(ToggleMusic);
    }

    private void OnDisable()
    {
        _masterSlider.onValueChanged.RemoveListener(ChangeMasterVolume);
        _musicSlider.onValueChanged.RemoveListener(ChangeMusicVolume);
        _effectsSlider.onValueChanged.RemoveListener(ChangeEffectsVolume);
        
        _musicButton.onClick.AddListener(ToggleMusic);
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _masterSlider.minValue = _minVolume;
        _musicSlider.minValue = _minVolume;
        _effectsSlider.minValue = _minVolume;
    }

    private void ToggleMusic()
    {
        if (_soundEnabled)
        {
            _soundEnabled = false;
            
            ChangeMasterVolume(_minVolume);
        }
        else
        {
            _soundEnabled = true;
            ChangeMasterVolume(_maxVolume);
        }
    }

    private void ChangeMasterVolume(float volume)
    {
        _mixer.SetFloat(_masterVolume,  Scale(volume));
    }
    
    private void ChangeMusicVolume(float volume)
    {
        _mixer.SetFloat(_musicVolume,  Scale(volume));
    }
    
    private void ChangeEffectsVolume(float volume)
    {
        _mixer.SetFloat(_effectsVolume,  Scale(volume));
    }

    private float Scale(float volume)
    {
        return Mathf.Log10(volume) * _scaleModifier;
    }
}
