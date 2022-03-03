﻿namespace Aivagames.Strategy.Abstractions
{
    public interface ICommandsQueue
    {
        void EnqueueCommand(object command);
        void Clear();
    }
}