using Microsoft.EntityFrameworkCore;
using Products.Constants.Enums;
using Products.Domain.Entities;
using Products.Infrastructure.Data;
using Products.Repositories.Classes;
using Products.Repository.Interfaces;
using Serilog;
using System.Data;

namespace Products.Repository.Classes
{
    public class CodeTrackerRepository : GenericRepository<CodeTracker>, ICodeTrackerRepository
    {

        public CodeTrackerRepository(ZeissAppDbContext context) : base(context)
        {
        }

        public async Task<int> GetNextCodeAsync(CodeTrackerKey key)
        {
            Log.Information("CodeTracker creating next code");

            using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

            var entry = await _context.CodeTrackers
                                      .FirstOrDefaultAsync(x => x.Id == (int)key);

            if (entry == null)
            {
                Log.Error($"Code tracker not found for key: {key}");
                throw new InvalidOperationException($"Code tracker not found for key: {key}");
            }

            entry.LastCode += 1;
            if (entry.LastCode > 999999) 
            {
                Log.Error("ID limit reached for key {key}",key);
                throw new InvalidOperationException($"ID limit reached for key: {key}");
            }
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return entry.LastCode;
        }
    }
}
