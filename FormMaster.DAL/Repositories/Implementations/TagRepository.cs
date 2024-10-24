using FormMaster.DAL.DataContext;
using FormMaster.DAL.Entities;
using FormMaster.DAL.Repositories.Contracts;

namespace FormMaster.DAL.Repositories.Implementations;

public class TagRepository(FormMasterDbContext context) : GenericRepository<Tag>(context), ITagRepository
{
}
