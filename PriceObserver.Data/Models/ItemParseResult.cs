﻿using System;

namespace PriceObserver.Data.Models;

public class ItemParseResult
{
    public int Id { get; set; }
    
    public int ItemId { get; set; }
    
    public bool IsSuccess { get; set; }
    
    public DateTime Created { get; set; }
}