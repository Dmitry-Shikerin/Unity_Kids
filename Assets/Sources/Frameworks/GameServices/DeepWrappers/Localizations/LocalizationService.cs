using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Constant;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Data;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Infrastructure.Services;
using UnityEngine;

namespace Sources.Frameworks.GameServices.DeepWrappers.Localizations
{
    public class LocalizationService : ILocalizationService
    {
        public void Translate(LocalizationLanguage language = LocalizationLanguage.Default)
        {
            string key = language != LocalizationLanguage.Default
                ? GetLanguage(language)
                : GetLanguage(LocalizationDataBase.Instance.Language);

            DeepLocalizationBrain.Translate(key);
        }

        public string GetText(string key) =>
            DeepLocalizationBrain.GetText(key);

        public Sprite GetSprite(string key) =>
            DeepLocalizationBrain.GetSprite(key);

        private string GetLanguage(LocalizationLanguage language)
        {
            return language switch
            {
                LocalizationLanguage.English => LocalizationConst.English,
                LocalizationLanguage.Turkish => LocalizationConst.Turkish,
                LocalizationLanguage.Russian => LocalizationConst.Russian,
                LocalizationLanguage.Default => LocalizationConst.Russian,
            };
        }
    }
}