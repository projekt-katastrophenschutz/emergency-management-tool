// <copyright file="HistoryItemMessage.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Messages
{
    using System;
    using BSA.Core.Models;

    public class HistoryItemMessage
    {
        public HistoryItemMessage(HistoryItem historyItem, Guid entityId, HistoryItemType entityType)
        {
            this.HistoryItem = historyItem;
            this.EntityId = entityId;
            this.EntityType = entityType;
        }

        public HistoryItem HistoryItem { get; set; }

        public Guid EntityId { get; set; }

        public HistoryItemType EntityType { get; set; }
    }
}