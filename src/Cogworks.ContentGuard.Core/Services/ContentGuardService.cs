using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace Cogworks.ContentGuard.Core.Services
{
    public interface IContentGuardService
    {
        bool IsLocked(int pageId);
        void Lock(int pageId, string ownerUsername);
        void Unlock(int pageId);
    }

    public class ContentGuardService : IContentGuardService
    {
        private readonly string _contentGuardRelationTypeAlias = "contentguard";

        private readonly IRelationService _relationService;
        private readonly IRelationType _contentGuardRelationType;
        public ContentGuardService(IRelationService relationService)
        {
            _relationService = relationService;
            _contentGuardRelationType = _relationService.GetRelationTypeByAlias(_contentGuardRelationTypeAlias);
        }

        public bool IsLocked(int pageId)
        {
            return _relationService.AreRelated(pageId, pageId, _contentGuardRelationTypeAlias);
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

        public void Unlock(int pageId)
        {
            var existingLocks =
                _relationService.GetByParentOrChildId(pageId, _contentGuardRelationTypeAlias);

            foreach (var pageLock in existingLocks)
            {
                _relationService.Delete(pageLock);
            }
        }
    }
}