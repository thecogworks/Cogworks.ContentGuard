using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Cogworks.ContentGuard.Core.Services;


public interface IContentGuardService
{
    IRelation? GetPageLockDetails(int pageId);
    string GetPageEditingUser(IRelation relation);
    bool IsLocked(IRelation relation, string ownerUsername);
    void Lock(int pageId, string ownerUsername);
    void Unlock(int pageId);
}


public class ContentGuardService : IContentGuardService
{
    private const string ContentGuardRelationTypeAlias = "contentguard";

    private readonly IRelationService _relationService;
    private readonly IRelationType _contentGuardRelationType;

    public ContentGuardService(IRelationService relationService)
    {
        _relationService = relationService;
        _contentGuardRelationType = _relationService.GetRelationTypeByAlias(ContentGuardRelationTypeAlias) ?? throw new Exception("No relation type found for ContentGuard");


    }


    public string GetPageEditingUser(IRelation relation)
    {
        return relation != null && !string.IsNullOrWhiteSpace(relation.Comment)
            ? relation.Comment
            : string.Empty;
    }

    public bool IsLocked(IRelation relation, string ownerUsername)
    {
        return relation != null && relation.Comment != null && !relation.Comment.Equals(ownerUsername);
    }

    public void Lock(int pageId, string ownerUsername)
    {
        Unlock(pageId);

        var relation = new Relation(pageId, pageId, _contentGuardRelationType)
        {
            Comment = ownerUsername
        };

        _relationService.Save(relation);
    }

    public IRelation? GetPageLockDetails(int pageId)
    {
        var existingLocks = _relationService.GetByParentOrChildId(pageId, ContentGuardRelationTypeAlias);

        return existingLocks != null && existingLocks.Any()
            ? existingLocks.FirstOrDefault(x => x.ParentId.Equals(pageId))
            : null;
    }

    public void Unlock(int pageId)
    {
        var existingLocks =
            _relationService.GetByParentOrChildId(pageId, ContentGuardRelationTypeAlias);

        foreach (var pageLock in existingLocks)
        {
            _relationService.Delete(pageLock);
        }
    }


}
