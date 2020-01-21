﻿using System.Text.Json.Serialization;

namespace StudentsAPIClient.Services
{
    public interface IClientCredentials
    {

        string ClientId { get; }
        string ClientSecret { get; }
        string Scope { get; }
    }
}