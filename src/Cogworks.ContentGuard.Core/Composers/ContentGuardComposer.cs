using Cogworks.ContentGuard.Core.Components;
using Cogworks.ContentGuard.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using System.Text.Json;

namespace Cogworks.ContentGuard.Core.Composers;

internal class ContentGuardComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        RegisterServices(builder);
        AddComponents(builder);

    }


    public void AddComponents(IUmbracoBuilder builder)
    {
        builder.Components().Append<ContentGuardComponent>();
    }


    public void RegisterServices(IUmbracoBuilder builder)
    {
        
        builder.Services.AddSingleton<IContentGuardService, ContentGuardService>();
    }
}
