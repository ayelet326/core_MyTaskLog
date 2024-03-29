using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Text.Json;
using IUser.Models;
using MyTaskLog.Models;
using SharedModel.Models;


namespace SharedLogicInServices;

public class SharedLogic<T> where T : Shared
{

    private List<T> list;
    private string fileName;
    //constructor to user service or task service
    public SharedLogic(List<T> list, string fileName)
    {
        this.list = list;
        this.fileName = fileName;
    }
    //write to json  the list after changed
    private void saveToFile()
    {
        File.WriteAllText(this.fileName, JsonSerializer.Serialize(this.list));
    }

    public int Add(T newObg)
    {
        if (this.list.Count() == 0)

        {
            newObg.Id = 0;
        }
        else
        {
            newObg.Id = this.list.Max(p => p.Id) + 1;

        }

        this.list.Add(newObg);
        saveToFile();
        return newObg.Id;
    }

    public bool Update(int id, T newObject)
    {
        if (id != newObject.Id)
            return false;

        var existingObj = GetById(id);
        if (existingObj == null)
            return false;


        if (newObject is User)
        {
            if (((User)(object)newObject).TypeUser != ((User)(object)existingObj).TypeUser)
                return false;
        }

        var index = this.list.IndexOf(existingObj);
        if (index == -1)
            return false;

        this.list[index] = newObject;
        saveToFile();
        return true;
    }

    public List<T> GetAll() => this.list;

    public T? GetById(int id)
    {
        return this.list.FirstOrDefault(p => p.Id == id);
    }

    public bool Delete(int id)
    {
        var existingObj = GetById(id);
        if (existingObj == null)
            return false;

        var index = this.list.IndexOf(existingObj);
        if (index == -1)
            return false;

        this.list.RemoveAt(index);
        saveToFile();
        return true;
    }
}