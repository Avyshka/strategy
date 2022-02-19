﻿using UnityEngine;

namespace Aivagames.Strategy.Abstractions
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        public override void ExecuteSpecificCommand(IPatrolCommand command)
        {
            Debug.Log($"{name} patrolling from {command.From} to {command.To}");
        }
    }
}