using FormMaster.DAL.DataContext;
using FormMaster.DAL.Entities;

namespace FormMaster.DAL.Repositories;

public class TagRepository(FormMasterDbContext context) : GenericRepository<Tag>(context), ITagRepository
{
}
