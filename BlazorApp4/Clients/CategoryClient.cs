using System;
using BlazorApp4.Models;
using BlazorApp4.Services;

namespace BlazorApp4.Clients;

public class CategoryClient(HttpClient httpClient, StorageService storageService)
    : BaseClient<Category>(httpClient, "/api/categories", storageService) { }
