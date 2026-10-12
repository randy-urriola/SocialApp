using Microsoft.EntityFrameworkCore;
using SocialApp.Data.Helpers;
using SocialApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Data.Services
{
    public class HashtagsService : IHashtagsService
    {
        private readonly AppDbContext _context;
        public HashtagsService(AppDbContext context)
        {
            _context = context;
        }
        public async Task ProcessHashtagsForNewPostAsync(string content)
        {
            // find and store hash tags
            var postHashTags = HashtagHelper.GetHashtags(content);
            foreach (var hashTag in postHashTags)
            {
                var hashTagDb = await _context.Hashtags.FirstOrDefaultAsync(h => h.Name == hashTag);
                if (hashTagDb != null)
                {
                    hashTagDb.Count += 1;
                    hashTagDb.DateUpdated = DateTime.UtcNow;

                    _context.Hashtags.Update(hashTagDb);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newHashTag = new Hashtag()
                    {
                        Name = hashTag,
                        Count = 1,
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow
                    };

                    await _context.Hashtags.AddAsync(newHashTag);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task ProcessHashtagsForRemovePostAsync(string content)
        {
            // Update hashtags count for the removed post
            var postHashTags = HashtagHelper.GetHashtags(content);
            foreach (var hashtag in postHashTags)
            {
                var hashtagDb = await _context.Hashtags.FirstOrDefaultAsync(n => n.Name == hashtag);
                if (hashtagDb != null)
                {
                    hashtagDb.Count -= 1;
                    hashtagDb.DateUpdated = DateTime.UtcNow;

                    _context.Hashtags.Update(hashtagDb);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
