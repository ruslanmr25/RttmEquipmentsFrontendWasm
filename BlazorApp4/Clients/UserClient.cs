using System;
using BlazorApp4.Models.UserModels;
using BlazorApp4.Services;

namespace BlazorApp4.Clients;

public class UserClient(IHttpClientFactory factory, StorageService storageService)
    : BaseClient<User>(factory, "/api/users", storageService) { }
