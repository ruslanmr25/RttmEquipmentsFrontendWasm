using System;
using BlazorApp4.Models;
using BlazorApp4.Services;

namespace BlazorApp4.Clients;

public class EquipmentTypeClient(IHttpClientFactory factory, StorageService storageService)
    : BaseClient<EquipmentType>(factory, "/api/types", storageService) { }
