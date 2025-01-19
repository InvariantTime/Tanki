﻿using Tanki.Domain;

namespace Tanki.Infrastructure.Intefaces
{
    public interface IAccountService
    {
        Task<Result<string>> SignIn(string name, string password);

        Task<Result<string>> Register(string name, string password);
    }
}