using System.Collections.Generic;
using Infrastructure.Services;
using Plugins.HabObject.DIContainer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.ScenesServices.MainMenuPart.Mono
{
    public class ViewProfile : MonoBehaviour
    {
        [SerializeField] private Transform _parentText;
        [SerializeField] private SelectebelText _textTemplate;
        [SerializeField] private Color _colorUnselected;
        [SerializeField] private Color _colorSelected;
        
        [Header("UI parts")]
        [SerializeField] private Button _buttonCreate;
        [SerializeField] private Button _buttonDelete;
        [SerializeField] private TextMeshProUGUI _label;
        
        [DI] private ProfileProvider _profileProvider;

        private List<SelectebelText> _texts = new List<SelectebelText>();

        private void Awake() => _profileProvider.NewProfileSelected += OnNewProfileSelected;

        private void OnDestroy() => _profileProvider.NewProfileSelected -= OnNewProfileSelected;

        private void OnEnable()
        {
            UpdateProfileList();
            _profileProvider.ProfileCreated += OnProfileSelectedDeleted;
            _profileProvider.ProfileDeleted += OnProfileSelectedDeleted;
            _buttonCreate.onClick.AddListener(CreateProfile);
            _buttonDelete.onClick.AddListener(DeleteCurrentProfile);
        }

        private void OnDisable()
        {
            _profileProvider.ProfileCreated -= OnProfileSelectedDeleted;
            _profileProvider.ProfileDeleted -= OnProfileSelectedDeleted;
            _buttonCreate.onClick.RemoveListener(CreateProfile);
            _buttonDelete.onClick.RemoveListener(DeleteCurrentProfile);
        }

        private void OnProfileSelectedDeleted(string obj) => UpdateProfileList();

        private void DeleteCurrentProfile() => _profileProvider.Remove(_profileProvider.CurrentProfile);

        private void CreateProfile()
        {
            _profileProvider.Create(_label.text);
            _label.text = string.Empty;
        }

        private void OnNewProfileSelected() => UpdateProfileList();

        private void UpdateProfileList()
        {
            DestroyTexts();
            List<string> profiles = _profileProvider.GetAllProfiles();
            foreach (var profile in profiles)
            {
                Color targetcolor = _colorUnselected;
                if (profile == _profileProvider.CurrentProfile)
                    targetcolor = _colorSelected;
                _texts.Add(Instantiate(_textTemplate, _parentText));
                _texts[_texts.Count-1].Init(targetcolor, profile);
                _texts[_texts.Count - 1].Selected += OnSelectedText;
            }
        }

        private void DestroyTexts()
        {
            foreach (var text in _texts)
            {
                text.Selected -= OnSelectedText;
                Destroy(text.gameObject);
            }

            _texts = new List<SelectebelText>();
        }

        private void OnSelectedText(string nameProfile) => _profileProvider.Choise(nameProfile);
    }
}