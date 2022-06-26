using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestApi.Entities;

namespace TestApi.DATA
{
    public class seedData
    {
         public static async Task SeedUser(ApplicationDbContext db_context){
             if(await db_context.users.AnyAsync()) return;
             var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
             var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
             foreach(var user in users){
                   using var hmac = new HMACSHA512();
                   user.UserName = user.UserName.ToLower();
                   user.passwordhash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                    user.passwordSalt = hmac.Key;
                    db_context.users.Add(user);
             }

             await db_context.SaveChangesAsync();
         }
    }
}