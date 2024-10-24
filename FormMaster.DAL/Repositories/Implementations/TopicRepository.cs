using FormMaster.DAL.DataContext;
using FormMaster.DAL.Entities;
using FormMaster.DAL.Repositories.Contracts;

namespace FormMaster.DAL.Repositories.Implementations;

public class TopicRepository(FormMasterDbContext context) : GenericRepository<Topic>(context),
    ITopicRepository
{
}
