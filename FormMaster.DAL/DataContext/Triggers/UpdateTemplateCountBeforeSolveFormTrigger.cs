using EntityFrameworkCore.Triggered;
using FormMaster.DAL.Entities;
using FormMaster.DAL.Repositories;

namespace FormMaster.DAL.DataContext.Triggers;

public class UpdateTemplateCountBeforeSolveFormTrigger(ITemplateRepository templateRepository) : IBeforeSaveTrigger<Form>
{
    public Task BeforeSave(ITriggerContext<Form> context, CancellationToken cancellationToken)
    {
        if (context.ChangeType == ChangeType.Added)
        {
            var template = templateRepository.GetById(context.Entity.FormId);
            if (template is not null)
            {
                template.Count += 1;
            }
        }

        return Task.CompletedTask;
    }
}
