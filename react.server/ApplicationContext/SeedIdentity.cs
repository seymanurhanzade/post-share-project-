using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace react.server.ApplicationContext
{ 
    #nullable disable
    public class SeedIdentity
    {
        public static async Task Seed(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IdentityContext context){
            var username= configuration["Data:AdminUser:username"];
            var atusername= configuration["Data:AdminUser:atusername"];
            var fullname= configuration["Data:AdminUser:fullname"];
            var email = configuration["Data:AdminUser:email"];  
            var password = configuration["Data:AdminUser:password"]; 
            var image = configuration["Data:AdminUser:image"];   
            var role = configuration["Data:AdminUser:role"];

            if(await userManager.FindByNameAsync(username)==null)
            {
                await roleManager.CreateAsync(new IdentityRole(role));

                var user = new Users()
                {
                    UserName = username,
                    Email = email,
                    AtUserName = atusername,
                    FullName= fullname,
                    Password = password,
                    Image= image,
                    EmailConfirmed = true
                };
                try{
                    var result = await userManager.CreateAsync(user,password);
                    if(result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user,role);
                        context.SaveChangesAsync();
                    }
                }catch(Exception ex){
                    Console.WriteLine(ex.Message);
                }
                
            }
        }
    }
}