using System;
using BlazorApp4.Models;
using BlazorApp4.Services;

namespace BlazorApp4.Clients;

public class EquipmentTypeClient(HttpClient httpClient, StorageService storageService) : BaseClient<EquipmentType>(httpClient, "/api/types", storageService)
{
}
