using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediaBrowser.Model.MediaSegments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JellyfinPluginMediaSegmentEditorEndpoints.Controllers;

/// <summary>
/// 
/// </summary>
[Authorize(Policy = "RequiresElevation")]
[ApiController]
[Route("MediaSegmentsApi")] 
public class MediaSegmentsApiController : ControllerBase
{
    /// <summary>
    /// Create a media segment
    /// </summary>
    /// <param name="itemId">Item ID</param>
    /// <param name="providerId"></param>
    /// <param name="mediaSegmentDto"></param>
    [HttpPost]
    [Route("{itemId:Guid}")] 
    public async Task<IActionResult> CreateSegment([FromRoute, Required] Guid itemId, [FromQuery, Required] String providerId, [FromBody, Required] MediaSegmentDto mediaSegmentDto)
    {
        if (Plugin.Instance is null)
        {
            return StatusCode(500, "Plugin is not initialized");
        }

        var mediaItem = await Plugin.Instance.GetMediaSegmentManager().CreateSegmentAsync(mediaSegmentDto, providerId)
            .ConfigureAwait(false);
        return Ok(mediaItem);
    }

    /// <summary>
    /// Delete a media segment
    /// </summary>
    /// <param name="segmentId">Segment ID</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{segmentId:Guid}")] 
    public async Task<IActionResult> DeleteSegment([FromRoute, Required] Guid segmentId)
    {
     if (Plugin.Instance is null) return StatusCode(500, "Plugin is not initialized");
     await Plugin.Instance.GetMediaSegmentManager().DeleteSegmentAsync(segmentId).ConfigureAwait(false);
     return Ok(segmentId);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Policy = "AnonymousLanAccessPolicy")]
    public async Task<IActionResult> Echo()
    {
        if (Plugin.Instance is null) return StatusCode(500, "Plugin is not initialized");
        await Task.Delay(100).ConfigureAwait(false);
        return Ok("Ok");
    }
}