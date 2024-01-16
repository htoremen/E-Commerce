﻿using System;

namespace Core.MessageBrokers
{
    public interface IMessageReceiver<T>
    {
        void Receive(Action<T, MetaData> action);
    }
}
