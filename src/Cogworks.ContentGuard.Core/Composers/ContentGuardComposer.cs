using Cogworks.ContentGuard.Core.Components;
using Cogworks.ContentGuard.Core.Services;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Cogworks.ContentGuard.Core.Composers
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class ContentGuardComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<IContentGuardService, ContentGuardService>();

            composition.Components().Append<ContentGuardComponent>();
        }
    }
}