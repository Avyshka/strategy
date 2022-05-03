﻿using Aivagames.Strategy.Abstractions;

namespace Aivagames.Strategy.UserControlSystem.UI.Model.CommandCreators
{
    public class AttackCommandCreator : CancellableCommandCreatorBase<IAttackCommand, IAttackable>
    {
        protected override IAttackCommand CreateCommand(IAttackable argument) => new AttackCommand(argument);
    }
}