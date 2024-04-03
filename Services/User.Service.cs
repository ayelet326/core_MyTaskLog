using IUser.Models;
using User_.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using SharedLogicInServices;
using MyTaskLog.Services;

namespace UserService.Services;

public class Useres : IUserService
{
    private List<User> userList;
    private string fileName = "Users.json";
    private SharedLogic<User> shardServices;
    private TaskLogcs TaskLog;
    public Useres(IWebHostEnvironment webHost)
    {
        this.fileName = Path.Combine(webHost.ContentRootPath, "wwwroot", "Data", "Users.json");
        TaskLog=new TaskLogcs(webHost);
        using (var jsonFile = File.OpenText(fileName))
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            this.userList = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
#pragma warning restore CS8601 // Possible null reference assignment.
        }

#pragma warning disable CS8604 // Possible null reference argument.
        this.shardServices = new SharedLogic<User>(userList, fileName);
#pragma warning restore CS8604 // Possible null reference argument.

    }
    public int Add(User newUser)
    {
        return shardServices.Add(newUser);
    }

    public bool Delete(int userId)
    {
        TaskLog.DeleteTasksBelongedUser(userId);
        return shardServices.Delete(userId);
    }

    public List<User> GetAll()
    {
        return shardServices.GetAll();
    }

    public User? GetUserById(int id)
    {
        return shardServices.GetById(id);
    }

    public bool Update(int id, User newUser)
    {
        bool ifUpdate = shardServices.Update(id, newUser);
        return ifUpdate;
    }

   
}

