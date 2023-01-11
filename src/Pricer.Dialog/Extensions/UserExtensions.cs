﻿using Pricer.Data.Persistent.Models;

namespace Pricer.Dialog.Extensions;

public static class UserExtensions
{
    public static string GetFullName(this User user)
    {
        if (string.IsNullOrEmpty(user.FirstName))
            return user.LastName;

        if (string.IsNullOrEmpty(user.LastName))
            return user.FirstName;

        return $"{user.FirstName} {user.LastName}";
    }
}