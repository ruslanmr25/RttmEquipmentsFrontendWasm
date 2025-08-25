using System;
using BlazorApp4.Models.Departments;
using BlazorApp4.Services;

namespace BlazorApp4.Clients;

public class DepartmentClient : BaseClient<Department>
{
    public DepartmentClient(IHttpClientFactory factory, StorageService storageService)
        : base(factory, "/api/departments", storageService) { }
}
