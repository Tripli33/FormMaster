﻿using Elastic.Clients.Elasticsearch;
using FormMaster.BLL.Services.Contracts;
using FormMaster.DAL.Entities;

namespace FormMaster.BLL.Services.Implementations;

public class TemplateSearchService(ElasticsearchClient client) : ITemplateSearchService
{
    private const string Index = "template";

    public async Task DeleteIndexAsync()
    {
        await client.Indices.DeleteAsync(Index);
    }

    public async Task AddIndexAsync(Template template)
    {
        await client.IndexAsync(template);
    }

    public async Task<IEnumerable<Template>> SearchTemplatesByTagAsync(string tagName)
    {
        var response = await client.SearchAsync<Template>(t => t
            .Index(Index)
            .Query(q => q
                .Match(m => m
                    .Field(f => f.Tags.First().Name)
                    .Query(tagName)
                )
            )
        );

        return response.Documents.AsEnumerable();
    }

    public async Task<IEnumerable<Template>> SearchTemplatesAsync(string searchTerm)
    {
        var response = await client.SearchAsync<Template>(t => t
            .Index(Index)
            .Query(q => q
                .Bool(b => b
                    .Should(
                        m => m.Match(mf => mf
                            .Field(f => f.Tags.First().Name)
                            .Query(searchTerm)
                        ),
                        m => m.Match(mf => mf
                            .Field(f => f.Description)
                            .Query(searchTerm)
                        ),
                        m => m.Match(mf => mf
                            .Field(f => f.Topic.Name)
                            .Query(searchTerm)
                        ),
                        m => m.Match(mf => mf
                            .Field(f => f.Name)
                            .Query(searchTerm)
                        )
                    )
                )
            )
        );

        return response.Documents.AsEnumerable();
    }

}