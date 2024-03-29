﻿using System;
using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Models;

namespace Pricer.Parser.Abstract;

public interface IParser
{
    Task<ParsedItemServiceResult> Parse(Uri url, ShopKey shopKey);
}