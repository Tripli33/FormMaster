using FormMaster.DAL.DataContext;
using FormMaster.DAL.Entities;

namespace FormMaster.DAL.Repositories;

public class TopicRepository(FormMasterDbContext context) : GenericRepository<Topic>(context), 
    ITopicRepository
{
}
