using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageMagick;
using ImageMagick.Defines;

namespace SaucyCapstone.Services;

public class ImageService
{
    public async Task ConvertAndResizeImage(Stream stream)
    {
        using var image = new MagickImage(stream);
        image.Quality = 100;
        image.Settings.SetDefine(MagickFormat.WebP, "lossless", true);
        image.Settings.SetDefine(MagickFormat.WebP, "method", "6");
        await image.WriteAsync(stream, MagickFormat.WebP);
    }
}