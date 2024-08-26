using System;
using _Assets.Scripts.Configs;
using _Assets.Scripts.Services.UIs.StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.UIs
{
    public class UIFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly ConfigProvider _configProvider;

        public UIFactory(IObjectResolver objectResolver, ConfigProvider configProvider)
        {
            _objectResolver = objectResolver;
            _configProvider = configProvider;
        }

        public GameObject CreateUI(UIStateType uiStateType)
        {
            switch (uiStateType)
            {
                case UIStateType.None:
                    break;
                case UIStateType.Loading:
                    _objectResolver.Instantiate(_configProvider.UIConfig.LoadingUI);
                    break;
                case UIStateType.Game:
                    _objectResolver.Instantiate(_configProvider.UIConfig.GameUI);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(uiStateType), uiStateType, null);
            }

            return null;
        }
    }
}