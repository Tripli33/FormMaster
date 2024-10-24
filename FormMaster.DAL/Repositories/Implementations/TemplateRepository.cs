using FormMaster.DAL.DataContext;
using FormMaster.DAL.Entities;
using FormMaster.DAL.Repositories.Implementations;

namespace FormMaster.DAL.Repositories;

public class TemplateRepository(FormMasterDbContext context) : GenericRepository<Template>(context), 
    ITemplateRepository
{
}
