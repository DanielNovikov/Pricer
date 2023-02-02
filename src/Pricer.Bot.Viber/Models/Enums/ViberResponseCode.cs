namespace Pricer.Viber.Models.Enums;

public enum ViberResponseCode
{
    Ok = 0,
    InvalidUrl = 1,
    InvalidAuthToken = 2,
    BadData = 3,
    MissingData = 4,
    ReceiverNotRegistered = 5,
    ReceiverNotSubscribed = 6,
    PublicAccountBlocked = 7,
    PublicAccountNotFound = 8,
    PublicAccountSuspended = 9,
    WebhookNotSet = 10,
    ReceiverNoSuitableDevice = 11,
    TooManyRequests = 12,
    ApiVersionNotSupported = 13,
    IncompatibleWithVersion = 14,
    PublicAccountNotAuthorized = 15,
    InChatReplyMessageNotAllowed = 16,
    PublicAccountIsNotInline = 17,
    NoPublicChat = 18,
    CannotSendBroadcast = 19,
    GeneralError = -1
}