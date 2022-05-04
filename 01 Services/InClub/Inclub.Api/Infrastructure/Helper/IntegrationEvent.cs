﻿using Newtonsoft.Json;
using System;

namespace Inclub.BuildingBlocks.EventBusBase.Events
{
    public class IntegrationEvent
    {
        [JsonProperty]
        public Guid Id
        {
            get;
            private set;
        }

        [JsonProperty]
        public DateTime CreationDate
        {
            get;
            private set;
        }

        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }
    }
}