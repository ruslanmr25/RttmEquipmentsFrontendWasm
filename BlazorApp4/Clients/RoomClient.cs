using System;
using BlazorApp4.Models.Rooms;
using BlazorApp4.Services;

namespace BlazorApp4.Clients;

public class RoomClient : BaseClient<Room>
{
    public RoomClient(HttpClient httpClient, StorageService storageService)
        : base(httpClient, "/api/rooms", storageService) { }
}
