using ElevenNote.Data;
using EverNote.Data;
using EverNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Service
{
    public class CatagoryService
    {
        private readonly Guid _userId;

        public CatagoryService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateCatagory(CatagoryCreate model)
        {
            var entity =
                new Catagory()
                {
                    OwnerId = _userId,
                    Title = model.Title,
                    Content = model.Content,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Catagories.Add(entity);
                return ctx.SaveChanges() == 1;
            }




        }
        public IEnumerable<CatagoryListItem> GetCatagory()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Catagories
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new CatagoryListItem
                                {
                                    CatagoryId = e.CatagoryId,
                                    Title = e.Title,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }

        }
        public CatagoryDetail GetCatagoryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Catagories
                        .Single(e => e.CatagoryId == id && e.OwnerId == _userId);
                return
                    new CatagoryDetail
                    {
                        CatagoryId = entity.CatagoryId,
                        Title = entity.Title,
                        Content = entity.Content,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }


        }

        public bool UpdateCatagory(CatagoryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Catagories
                        .Single(e => e.CatagoryId == model.CatagoryId && e.OwnerId == _userId);

                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }


        public bool DeleteCatagory(int CatagoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Catagories
                        .Single(e => e.CatagoryId == CatagoryId && e.OwnerId == _userId);

                ctx.Catagories.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        


    }
}
