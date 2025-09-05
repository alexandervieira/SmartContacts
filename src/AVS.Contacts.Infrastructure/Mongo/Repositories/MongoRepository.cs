using AVS.Contacts.Domain.Common;
using AVS.Contacts.Domain.Repositories;
using AVS.Contacts.Infrastructure.Mongo.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace AVS.Contacts.Infrastructure.Mongo.Repositories;

public abstract class MongoRepository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>
{
    protected readonly IMongoCollection<TEntity> Collection;

    protected MongoRepository(IOptions<MongoSettings> settings, string collectionName)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        Collection = database.GetCollection<TEntity>(collectionName);
    }

    public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken ct = default)
        => await Collection.Find(x => x.Id!.Equals(id)).FirstOrDefaultAsync(ct);

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default)
    {
        await Collection.InsertOneAsync(entity, cancellationToken: ct);
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default)
    {
        await Collection.ReplaceOneAsync(x => x.Id!.Equals(entity.Id), entity, cancellationToken: ct);
        return entity;
    }

    public virtual async Task DeleteAsync(TId id, CancellationToken ct = default)
        => await Collection.DeleteOneAsync(x => x.Id!.Equals(id), ct);

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct = default)
        => await Collection.Find(_ => true).ToListAsync(ct);

    public virtual async Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
        => await Collection.Find(predicate).ToListAsync(ct);
}