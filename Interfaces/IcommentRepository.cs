using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.models;

namespace api.Interfaces
{
    public interface IcommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetById(int id);

        Task<Comment> CreateAsync(Comment commentModel);

        Task<Comment?> DeleteAsync(int id);

        Task<Comment?> UpdateAsync(int id, Comment commentModel);

    }
}