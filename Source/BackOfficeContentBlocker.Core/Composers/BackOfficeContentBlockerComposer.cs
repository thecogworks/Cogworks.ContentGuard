using BackOfficeContentBlocker.Core.Components;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace BackOfficeContentBlocker.Core.Composers
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class BackOfficeContentBlockerComposer : ComponentComposer<BackOfficeContentBlockerComponent>
    {
    }
}