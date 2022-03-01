using System;
using Aivagames.Strategy.Abstractions;
using Aivagames.Strategy.UserControlSystem.UI.Model;
using Aivagames.Strategy.Utils.AssetsInjector;
using Code.Utils;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "AssetsInstaller", menuName = "Installers/AssetsInstaller")]
public class AssetsInstaller : ScriptableObjectInstaller<AssetsInstaller>
{
    [SerializeField] private AssetsContext _legacyContext;
    [SerializeField] private SelectableValue _selectableValue;
    [SerializeField] private Vector3Value _groundClicksRMB;
    [SerializeField] private AttackableValue _attackablesRMB;
    [SerializeField] private Sprite _chomperSprite;

    public override void InstallBindings()
    {
        Container.BindInstances(
            _legacyContext,
            _selectableValue,
            _groundClicksRMB,
            _attackablesRMB
        );

        Container.Bind<IAwaitable<IAttackable>>().FromInstance(_attackablesRMB);
        Container.Bind<IAwaitable<Vector3>>().FromInstance(_groundClicksRMB);
        Container.Bind<IObservable<ISelectable>>().FromInstance(_selectableValue);
        Container.Bind<Sprite>().WithId("Chomper").FromInstance(_chomperSprite);
    }
}