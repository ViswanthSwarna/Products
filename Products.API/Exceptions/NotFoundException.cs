﻿using System;

namespace Products.API.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" with key \"{key}\" was not found.") { }
    }

}