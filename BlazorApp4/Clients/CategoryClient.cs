using System;
using BlazorApp4.Models;
using BlazorApp4.Services;

namespace BlazorApp4.Clients;

public class CategoryClient(IHttpClientFactory factory, StorageService storageService)
    : BaseClient<Category>(factory, "/api/categories", storageService) { }
