using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiWifiRouter
{
	public class ImageHelper
	{
		public void AddImageToImageList(ImageList iml, Bitmap bm, string key)
		{
			// Make the bitmap.
			Bitmap iml_bm = new Bitmap(
			iml.ImageSize.Width,
			iml.ImageSize.Height);
			using (Graphics gr = Graphics.FromImage(iml_bm))
			{
				gr.Clear(Color.Transparent);
				gr.InterpolationMode = InterpolationMode.High;
				// See where we need to draw the image to scale it properly.
				RectangleF source_rect = new RectangleF(
				0, 0, bm.Width, bm.Height);
				RectangleF dest_rect = new RectangleF(
				0, 0, iml_bm.Width, iml_bm.Height);
				dest_rect = ScaleRect(source_rect, dest_rect);
				// Draw the image.
				gr.DrawImage(bm, dest_rect, source_rect,
				GraphicsUnit.Pixel);
			}
			// Add the image to the ImageList.
			iml.Images.Add(key, iml_bm);
		}

		// Scale an image without disorting it.
		// Return a centered rectangle in the destination area.
		private RectangleF ScaleRect(
		RectangleF source_rect, RectangleF dest_rect)
		{
			float source_aspect =
			source_rect.Width / source_rect.Height;
			float wid = dest_rect.Width;
			float hgt = dest_rect.Height;
			float dest_aspect = wid / hgt;
			if (source_aspect > dest_aspect)
			{
				// The source is relatively short and wide.
				// Use all of the available width.
				hgt = wid / source_aspect;
			}
			else
			{
				// The source is relatively tall and thin.
				// Use all of the available height.
				wid = hgt * source_aspect;
			}
			// Center it.
			float x = dest_rect.Left + (dest_rect.Width - wid) / 2;
			float y = dest_rect.Top + (dest_rect.Height - hgt) / 2;
			return new RectangleF(x, y, wid, hgt);
		}
	}
}
