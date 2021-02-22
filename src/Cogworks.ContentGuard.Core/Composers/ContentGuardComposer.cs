using Cogworks.ContentGuard.Core.Components;
using Cogworks.ContentGuard.Core.Services;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Cogworks.ContentGuard.Core.Composers
{
    internal class ContentGuardComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<IContentGuardService, ContentGuardService>();

            _ = composition.Components().Append<ContentGuardComponent>();
        }
    }
}