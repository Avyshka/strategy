using Aivagames.Strategy.UserControlSystem.UI.Model;
using Aivagames.Strategy.Utils.AssetsInjector;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "AssetsInstaller", menuName = "Installers/AssetsInstaller")]
public class AssetsInstaller : ScriptableObjectInstaller<AssetsInstaller>
{
    [SerializeField] private AssetsContext _legacyContext;
    [SerializeField] private SelectableValue _selectableValue;
    [SerializeField] private Vector3Value _groundClicksRMB;
    [SerializeField] private AttackableValue _attackablesRMB;

    public override void InstallBindings()
    {
        Container.BindInstances(
            _legacyContext,
            _selectableValue,
            _groundClicksRMB,
            _attackablesRMB
        );
    }
}