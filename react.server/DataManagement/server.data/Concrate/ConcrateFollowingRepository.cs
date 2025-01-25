using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using react.server.ApplicationContext;
using react.server.DataManagement.server.data.Abstract;
using react.server.DataManagement.server.entitys;
using react.server.Models;
using server.data.Concrate;

namespace react.server.DataManagement.server.data.Concrate
{
    public class ConcrateFollowingRepository : ConcrateRepository<FollowerTable, IdentityContext>, IFollowingRepository
    {
        private readonly IdentityContext _context;
        public ConcrateFollowingRepository(IdentityContext context):base(context){
            _context = context;
        }

        public async Task<IActionResult> FollowerAdd(FollowModel model)
        {
            if(model.UserId == null || model.FollowerUserId == null){
                return new BadRequestObjectResult("Model boş olamaz.");
            }else{

                var FollowTableControl = await _context.FollowerTable.FirstOrDefaultAsync(x=>x.UserId== model.UserId && x.FollowerUserId== model.FollowerUserId);

                if(FollowTableControl!=null)
                {   
                    _context.FollowerTable.Remove(FollowTableControl);
                    var dltc = await _context.SaveChangesAsync();

                    if(dltc>0){
                        var deleteF = new DeleteFollowerTable{
                            UserId= FollowTableControl.UserId,
                            FollowerUserId= FollowTableControl.FollowerUserId,
                            Time= DateTime.Now
                        };
                        _context.DeleteFollowerTable.Add(deleteF);
                        var TakipEdenUpdate= await _context.Users.FirstOrDefaultAsync(x=>x.Id== model.UserId);
                        var TakipEdilenUpdate= await _context.Users.FirstOrDefaultAsync(x=>x.Id== model.FollowerUserId);
                        if(TakipEdenUpdate!=null && TakipEdilenUpdate!=null){
                            TakipEdenUpdate.TotalFollower --;
                            TakipEdilenUpdate.TotalFollowed --;
                            _context.Users.Update(TakipEdenUpdate);
                            _context.Users.Update(TakipEdilenUpdate);
                            _context.SaveChanges();
                        }
                        await _context.SaveChangesAsync();
                        return new OkObjectResult("DeleteFollowerTable'a kaydedildi.");
                    }
                    else{
                        return new BadRequestObjectResult("Silinemedi.");
                    }
                }else{
                    
                    var follow= new FollowerTable{
                        UserId=model.UserId,
                        FollowerUserId=model.FollowerUserId,
                        Time= DateTime.Now
                    };
                    _context.FollowerTable.Add(follow);
                    var adds= await _context.SaveChangesAsync();
                    if (adds>0){
                        var TakipEden = await _context.Users.FirstOrDefaultAsync(i=>i.Id==model.UserId);
                        var TakipEdilen = await _context.Users.FirstOrDefaultAsync(i=>i.Id==model.FollowerUserId);
                    
                        if(TakipEden!=null&& TakipEdilen!=null){

                            TakipEden.TotalFollower ++;
                            TakipEdilen.TotalFollowed ++;
                            _context.Users.Update(TakipEden);
                            _context.Users.Update(TakipEdilen);
                            await _context.SaveChangesAsync();
                        }
                        _context.SaveChanges();
                    }
                    return new OkObjectResult(follow);
                }                
            }
        }

        public async Task<IActionResult> FollowingControl(FollowModel model)
        {
            var control=await _context.FollowerTable.FirstOrDefaultAsync(x=>x.UserId== model.UserId && x.FollowerUserId==model.FollowerUserId);
            bool IsFollowing = control!=null;
            var responce= new {Status= IsFollowing};

            return new ObjectResult(responce) {StatusCode=200};
        }

        public async Task<List<FollowUserModel>> Takipciler(string userId)
        {
            var data = await _context.FollowerTable
                .Where(x=>x.FollowerUserId== userId)
                .Join(_context.Users, p=>p.UserId, x=>x.Id, (p,x) => new FollowUserModel{
                    UserId= x.Id,
                    FullName= x.FullName,
                    AtUserName= x.AtUserName,
                    Images= x.Image
                }).ToListAsync();
            return data;
        }

        public async Task<List<FollowingList>> TakipList(string userId)
        {
            
            var TEdata = await _context.FollowerTable
                .Where(i=>i.UserId== userId)
                .Join(_context.Users, x=>x.FollowerUserId, p=>p.Id, (x,p)=> new FollowUserModel{
                    UserId=p.Id,
                    FullName= p.FullName,
                    AtUserName= p.AtUserName,
                    Images= p.Image
                }).ToListAsync();
            
            var Tdata = await _context.FollowerTable
                .Where(x=>x.FollowerUserId== userId)
                .Join(_context.Users, p=>p.UserId, x=>x.Id, (p,x) => new FollowUserModel{
                    UserId= x.Id,
                    FullName= x.FullName,
                    AtUserName= x.AtUserName,
                    Images= x.Image
                }).ToListAsync();

            var list = new List<FollowingList>{
                new FollowingList{
                    TakipEdilenlerList= TEdata,
                    TakipcilerList=Tdata
                }
            };
            return list;
        }
    }
}