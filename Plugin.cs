using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using JellyfinPluginMediaSegmentEditorEndpoints.Configuration;
using JellyfinPluginMediaSegmentEditorEndpoints.Controllers;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Controller;
using MediaBrowser.Controller.Plugins;
using MediaBrowser.Model.MediaSegments;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;

namespace JellyfinPluginMediaSegmentEditorEndpoints;

/// <summary>
/// The main plugin.
/// </summary>
public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
{
    private IMediaSegmentManager _mediaSegmentsManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="Plugin"/> class.
    /// </summary>
    /// <param name="applicationPaths">Instance of the <see cref="IApplicationPaths"/> interface.</param>
    /// <param name="xmlSerializer">Instance of the <see cref="IXmlSerializer"/> interface.</param>//
    /// <param name="mediaSegmentsManager">Instance of the <see cref="IMediaSegmentManager"/> interface.</param>//
    public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer,
        IMediaSegmentManager mediaSegmentsManager
    ) : base(applicationPaths,
        xmlSerializer)
    {
        Instance = this;
        _mediaSegmentsManager = mediaSegmentsManager ?? throw new ArgumentNullException(nameof(mediaSegmentsManager));
    }

    /// <summary>
    ///  Gets the instance's <see cref="IMediaSegmentManager"/>
    /// </summary>
    /// <returns></returns>
    public IMediaSegmentManager GetMediaSegmentManager()
    {
        return _mediaSegmentsManager;
    }

    /// <inheritdoc />
    public override string Name => "Media Segment Editor Endpoints";

    /// <inheritdoc />
    public override Guid Id => Guid.Parse("71aa771c-eafd-4b77-a1d2-184f946d630c");

    /// <summary>
    /// Gets the current plugin instance.
    /// </summary>
    public static Plugin? Instance { get; private set; }

    /// <inheritdoc />
    public IEnumerable<PluginPageInfo> GetPages()
    {
        return
        [
            new PluginPageInfo
            {
                Name = Name,
                EmbeddedResourcePath = string.Format(CultureInfo.InvariantCulture, "{0}.Configuration.configPage.html",
                    GetType().Namespace)
            }
        ];
    }
}