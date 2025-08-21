using System;
using BlazorApp4.Models.UserModels;
using BlazorApp4.Services;

namespace BlazorApp4.Clients;

public class UserClient(HttpClient httpClient, StorageService storageService)
    : BaseClient<User>(httpClient, "/api/users", storageService) { }
