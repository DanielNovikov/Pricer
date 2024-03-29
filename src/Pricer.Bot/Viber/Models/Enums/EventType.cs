﻿using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Pricer.Bot.Viber.Models.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum EventType
{
    [EnumMember(Value = "delivered")]
    Delivered = 1,
    
    [EnumMember(Value = "seen")]
    Seen = 2,

    [EnumMember(Value = "failed")]
    Failed = 3,

    [EnumMember(Value = "subscribed")]
    Subscribed = 4,

    [EnumMember(Value = "unsubscribed")]
    Unsubscribed = 5,

    [EnumMember(Value = "conversation_started")]
    ConversationStarted = 6,

    [EnumMember(Value = "message")]
    Message = 7,

    [EnumMember(Value = "webhook")]
    Webhook = 8,

    [EnumMember(Value = "action")]
    Action = 9,

    [EnumMember(Value = "client_status")]
    ClientStatus = 10
}