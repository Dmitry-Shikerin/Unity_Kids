using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Enums;
using UnityEngine;

namespace Sources.Frameworks.GameServices.DeepWrappers.Localizations
{
    public interface ILocalizationService
    {
        void Translate(LocalizationLanguage language = LocalizationLanguage.Default);
        string GetText(string key);
        Sprite GetSprite(string key);
    }
}